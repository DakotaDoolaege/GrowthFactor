using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;

/// <summary>
/// This is the class that's responsible for connecting to the game's database 
/// and handling queries to manage game scores and admin account credentials.
/// Within the game, this script is bound to a game object that's instantiated
/// in the very first scene and lives persistently through scene changes until
/// the game is closed.
/// </summary>
public class Database : MonoBehaviour
{
    private string dbFile;          // The filename of the SQLite database file
    private string conStr;          // Connection string
    private IDbConnection dbCon;    // The connection to the database, using conStr
    private bool isOpen = false;    // A flag indicating whether DB connection is open

    // Awake is called before the first frame update
    public void Awake()
    {
        // Preserve connection across scene loads
        DontDestroyOnLoad(this.gameObject);

        //Debug.Log("DB Connection open? " + this.isOpen);
        if (this.dbCon == null || this.dbCon.State != ConnectionState.Open)
            MakeConnection();
    }

    /// <summary>
    /// This method connects to the SQLite database that stores the game's 
    /// scores and admin account credentials. It's called from the Awake()
    /// method above (which itself is called when the main menu scene is loaded
    /// and runs before the first frame runs). If the database doesn't exist, 
    /// then it creates the database, sets up the tables, and registers an Admin
    /// account with the username "Admin" and password "guest".
    /// </summary>
    private void MakeConnection()
    {
        this.dbFile = Application.dataPath + "/Database/PlayerScores.db";
        this.conStr = "URI=file:" + this.dbFile;
        bool needCreate = false;
        bool test = false;          // Set this to true to load test data

        // If DB doesn't exist, make it
        if (! File.Exists(this.dbFile))
        {
            needCreate = true;
            SqliteConnection.CreateFile(dbFile);
        }

        // Open a connection to the database
        this.dbCon = new SqliteConnection(this.conStr);
        if (this.dbCon.State == ConnectionState.Open)
            return;
        this.dbCon.Open();
        this.isOpen = true;

        // If DB didn't exst and was just created, make tables
        if (needCreate)
        {
            string createScript = File.ReadAllText(Application.dataPath + "/Database/create.sql");
            string testDataScript = File.ReadAllText(Application.dataPath + "/Database/loadtestdata.sql");

            IDbCommand makeTables = this.dbCon.CreateCommand();
            makeTables.CommandText = createScript;
            makeTables.ExecuteNonQuery();
            makeTables.Dispose();
            RegisterAdmin("Admin", "guest");    // This is a reference to the TV show Archer lol

            // This next block populates with test data, if the test variable is set to true
            // (modify it manually; it doesn't actually serve a purpose for the game itsel.f)
            if (test)
            {
                IDbCommand testData = this.dbCon.CreateCommand();
                testData.CommandText = testDataScript;
                testData.ExecuteNonQuery();
                testData.Dispose();
            }

            needCreate = false;
        }
    }

    /// <summary>
    /// This function queries the scores table for the top 10 scores.
    /// 
    /// The nae is a holdover from when we originally planned to have separate
    /// single-player and multiplayer scores, but then decided to combine them
    /// all into a single table. One of the other devs made calls to this method
    /// before I could change the name, and now I'm too scared I'll accidentally
    /// break the game if I try to rename it (I am a coward) so I left it.
    /// code and 
    /// </summary>
    /// 
    /// <returns>
    /// A matrix/list of list of strings containing the results from the query
    /// (see below). Format is as follows:
    /// [ ["score", "level", "name"], 
    ///   ["score", "level", "name"], 
    ///   ..., 
    ///   ["score", "level", "name"] ]
    /// </returns>
    public IList TopSingle()
    {
        /* Query:
         * SELECT *
         * FROM Scores
         * ORDER BY -Score
         * LIMIT 10
         */ 
        string query = "SELECT * FROM Scores ORDER BY - Score LIMIT 10";
        IDataReader results = Query(query);

        IList records = new ArrayList();
        while (results.Read())
        {
            IList<string> entry = new List<string>();
            string name = results.GetValue(0).ToString();
            string score = results.GetValue(2).ToString();
            string level = results.GetValue(3).ToString();
            entry.Add(score);
            entry.Add(level);
            entry.Add(name);
            records.Add(entry);
            
            //string output = name + ": " + score;
            //double diff = JulianDay() - double.Parse(results.GetValue(1).ToString());
            //output += ", " + diff.ToString() + " days ago";
            //Debug.Log(output);
        }
        results.Close();
        return records;
    }

    /// <summary>
    /// Does what it sounds like: inserts a player score into a database at the 
    /// end of a game. See below for the query.
    /// </summary>
    /// 
    /// <param name="player"> The player's name </param>
    /// <param name="score"> The player's scre </param>
    /// <param name="level"> The highest level reached in this session </param>
    public void InsertScore(string player, string score, string level)
    {
        /* Query:
         * INSERT INTO Scores
         * VALUES (player, julianday('now'), score, level)
         */ 
        string cmdText = "INSERT INTO Scores VALUES ('" + player + "', ";
        cmdText += "julianday('now'), " + score + ", " + level + ");";

        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();
    }

    /// <summary>
    /// Removes all scores associated with a particular player name
    /// (ex. if they enter something naughty that the profanity filter doesn't 
    /// catch)
    /// </summary>
    /// 
    /// <param name="player"> The name whose scores are to be removed </param>
    public void RemoveScore(string player)
    {
        /* Query:
         * DELETE FROM Scores
         * WHERE Name LIKE player
         */ 
        string cmdText = "DELETE FROM Scores WHERE Name LIKE '" + player + "'";

        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();
    }

    /// <summary>
    /// Removes all stored player scores that are older than 180 days.
    /// The main purpose here is to prevent the database file from endlessly 
    /// growing and reaching some absurd size like several gigabytes.
    /// (Library patrons would have to really LOVE our game for that to happen,
    /// and play it way too much for that to happen, I guess, but that's beside 
    /// the point!)
    /// </summary>
    public void PurgeOld()
    {
        /* Query:
         * DELETE FROM Scores
         * WHERE julianday('now') - Datetime > 180
         */
        string cmdText = "DELETE FROM Scores WHERE julianday('now') - Datetime > 180";

        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();
    }

    /// <summary>
    /// Private method to separately handle the boilerplate code involved in 
    /// execting queries that return a result. 
    /// </summary>
    /// 
    /// <param name="queryText"> SQL query to execute </param>
    /// 
    /// <returns> The query results </returns>
    private IDataReader Query(string queryText)
    {
        IDbCommand query = this.dbCon.CreateCommand();
        query.CommandText = queryText;
        IDataReader reader = query.ExecuteReader();
        query.Dispose();
        return reader;
    }

    /// <summary>
    /// Function to store the current date in the Julianday format.
    /// I couldn't/was too lazy to figure out how to do this natively in C#, so
    /// I came up with this janky-ass workaround using SQLite's julianday 
    /// function.
    /// 
    /// (Note: The julianday format of the date and tie a score is registered is 
    /// part of the primary key in the scores table in the DB. This is what lets
    /// users have multiple scores attached to their names, and lets old scores
    /// get cleared out of the DB after 180 days)
    /// 
    /// I think I originally used this for some debugging output, but not 
    /// actually in the final game, so you can go ahead and delete this if you
    /// want. Oops, haha.
    /// </summary>
    /// 
    /// <returns> juliandow format of current date and time </returns>
    private double JulianDay()
    {
        string queryStr = "SELECT julianday('now')";
        IDbCommand query = this.dbCon.CreateCommand();
        query.CommandText = queryStr;
        IDataReader result = query.ExecuteReader();
        query.Dispose();
        double julianDay = 0.0;
        while (result.Read())
        {
            julianDay = double.Parse(result.GetValue(0).ToString());
        }
        return julianDay;
    }

    /// <summary>
    /// Does what it says it does. Called when you exit from the game.
    /// </summary>
    public void CloseConnection()
    {
        //Debug.Log("Closing connection...");
        this.dbCon.Close();
        this.isOpen = false;
    }

    /// <summary>
    /// Used to authenticate an admin account
    /// </summary>
    /// 
    /// <param name="username"> Admin account username </param>
    /// <param name="password"> Admin account password </param>
    /// 
    /// <returns>
    /// true, if authentication is successful, otherwise false
    /// </returns>
    public bool Authenticate(string username, string password)
    {
        string hash = Sha256Hash(password);
        string storedHash = GetHash(username);

        return (hash == storedHash);
    }

    /// <summary>
    /// Compute the SHA256 of a password to store for an admin account
    /// </summary>
    /// 
    /// <param name="input"> The plaintext password </param>
    /// 
    /// <returns>
    /// The corresponding SHA256 hash (hexadecimal) in string form
    /// </returns>
    private string Sha256Hash(string input)
    {
        using (SHA256 digest = SHA256.Create() )
        {
            // Compute hash as byte array
            byte[] bytes = digest.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert byte array to string
            StringBuilder hash = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }

    /// <summary>
    /// Retrieve the password hash associated with an admin account username
    /// </summary>
    /// 
    /// <param name="user"> The admin account's username </param>
    /// 
    /// <returns> The associated password hash </returns>
    private string GetHash(string user)
    {
        /* Query:
         * SELECT Hash
         * FROM Admin
         * WHERE Username = user
         */

        string cmdText = "SELECT Hash FROM Admin WHERE Username = '" + user + "'";
        IDataReader result = Query(cmdText);
        while (result.Read())
        {
            // Note: Username is primary key; should only be one result
            return result.GetString(0);
        }
        return "NULL";  // Returned if user doesn't exist in Admin table
    }

    /// <summary>
    /// Change the password associated with an admin account. Fails and returns
    /// false if the wrong current password is supplied
    /// </summary>
    /// 
    /// <param name="user"> The account username </param>
    /// <param name="oldPassword"> The current password </param>
    /// <param name="newPassword">
    /// The new password to replace the current one 
    /// </param>
    /// 
    /// <returns> true if success, false otherwise </returns>
    public bool ChangePassword(string user, string oldPassword, string newPassword)
    {
        bool auth = Authenticate(user, oldPassword);
        if (!auth)
            return false;

        /* Query:
         * UPDATE Admin
         * SET Hash = [newPasswordHash]
         * WHERE Username = user
         */
        string hash = Sha256Hash(newPassword);
        string cmdText = "UPDATE Admin SET Hash = '" + hash + "' WHERE Username = '" + user + "'";
        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();

        return true;
    }

    /// <summary>
    /// Register a new admin account into the database
    /// </summary>
    /// 
    /// <param name="username"> The account's username </param>
    /// <param name="password"> The plaintext password </param>
    public void RegisterAdmin(string username, string password)
    {
        string hash = Sha256Hash(password);

        /* Query:
         * INSERT INTO Admin
         * VALUES (username, hash)
         */

        string cmdText = "INSERT INTO Admin VALUES ('" + username + "', '" + hash + "')";
        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();
    }

    /// <summary>
    /// Returns a count of the number of currently registered admin accounts 
    /// </summary>
    /// 
    /// <returns> The number of entries in the Admin table </returns>
    public int AdminCount()
    {
        /* Query:
         * SELECT count(*)
         * FROM Admin
         */

        int count = 0;
        string queryText = "SELECT count(*) FROM Admin";

        IDataReader result = Query(queryText);
        while (result.Read())
        {
            string resultStr = result.GetValue(0).ToString(); // Should return only a single value
            Int32.TryParse(resultStr, out count);
        }

        return count;
    }

    /// <summary>
    /// Get a list of all the current admin usernames
    /// </summary>
    /// 
    /// <returns> See above </returns>
    public IList GetAllAdmins()
    {
        /* Query
         * SELECT Username
         * FROM Admin
         */

        IList admins = new List<string>();
        string query = "SELECT Username FROM Admin";
        IDataReader results = Query(query);

        while (results.Read())
        {
            string username = results.GetValue(0).ToString();
            admins.Add(username);

            Debug.Log(username);
        }

        return admins;
    }

    /// <summary>
    /// Query the database to see if a specific admin account username is 
    /// registered
    /// </summary>
    /// 
    /// <param name="target"> the username to search </param>
    /// 
    /// <returns>
    /// The username if it exists, or an empty string if it doesn't
    /// </returns>
    public string GetAdmin(string target)
    {
        /* Query:
         * SELECT Username
         * FROM Admin
         * WHERE Username = target
         */

        string queryText = "SELECT Username FROM Admin WHERE Username = '" + target + "'";
        string username = "";
        IDataReader result = Query(queryText);
        while (result.Read())
        {
            username = result.GetString(0); // SHould return a single result
        }

        return username;
    }

    /// <summary>
    /// Delete a currently registered admin account (remove the entry from the
    /// Admin table). This will fail if the wrong credentials are given, or if 
    /// the account to be deleted does not currently exist in the database
    /// </summary>
    /// 
    /// <param name="user">
    /// The username of any currently-registered admin account
    /// </param>
    /// <param name="password"> That account's password </param>
    /// <param name="target"> The userame of the account you wish to delete </param>
    /// 
    /// <returns> true if successful, otherwise false </returns>
    public bool RemoveAdmin(string user, string password, string target)
    {
        if (! Authenticate(user, password))
            return false;

        if (GetAdmin(target) == "")
            return false;

        /* Query:
         * DELETE FROM Admin
         * WHERE Username = target
         */

        string cmdText = "DELETE FROM Admin WHERE Username = '" + target + "'";
        IDbCommand command = this.dbCon.CreateCommand();
        command.CommandText = cmdText;
        command.ExecuteNonQuery();
        command.Dispose();

        return (GetAdmin(target) == "");
    }
}
