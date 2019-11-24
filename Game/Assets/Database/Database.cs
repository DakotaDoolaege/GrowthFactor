using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;

public class Database : MonoBehaviour
{
    private string dbFile;
    private string conStr;
    private IDbConnection dbCon;
    private bool isOpen = false;

    // Start is called before the first frame update
    public void Awake()
    {
        // Preserve connection across scene loads
        DontDestroyOnLoad(this.gameObject);

        //Debug.Log("DB Connection open? " + this.isOpen);
        if (this.dbCon == null || this.dbCon.State != ConnectionState.Open)
            MakeConnection();
    }

    private void MakeConnection()
    {
        this.dbFile = Application.dataPath + "/Database/PlayerScores.db";
        this.conStr = "URI=file:" + this.dbFile;
        bool needCreate = false;
        bool test = false;          // Set this to true to load test data

        //Debug.Log(this.conStr);         // Check
        // If DB doesn't exist, make it
        if (! File.Exists(this.dbFile))
        {
            needCreate = true;
            SqliteConnection.CreateFile(dbFile);
            //Debug.Log("DB doesn't exist. Creating...");
        }

        // Open a connection to the database
        this.dbCon = new SqliteConnection(this.conStr);
        if (this.dbCon.State == ConnectionState.Open)
            return;
        //Debug.Log("Opening connection...");
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
            //Debug.Log("Creating tables...");

            // This next block populates with test data. Delete before handover
            if (test)
            {
                IDbCommand testData = this.dbCon.CreateCommand();
                testData.CommandText = testDataScript;
                testData.ExecuteNonQuery();
                testData.Dispose();
                //Debug.Log("Populating with test data...");
            }

            needCreate = false;
        }

        // Test InsertScores method
        //InsertScore("TEST_NAME", "9999", "99");

        // Test RemoveScore method
        //RemoveScore("TEST_NAME");

        // Test purging of old scores
        //PurgeOld();

        // Test hashing
        //Debug.Log(Sha256Hash("password"));
        //Debug.Log(GetHash("Admin"));
        //Debug.Log(Authenticate("Admin", "password"));

        // Test Admin table methods
        //RegisterAdmin("TestAdmin", "password2");
        //Debug.Log(RemoveAdmin("Admin", "password", "TestAdmin"));
        //GetAllAdmins();
        //Debug.Log(AdminCount());
        //Debug.Log(GetAdmin("TestAdmin"));
        //Debug.Log(ChangePassword("TestAdmin", "password2", "password3"));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    private IDataReader Query(string queryText)
    {
        IDbCommand query = this.dbCon.CreateCommand();
        query.CommandText = queryText;
        IDataReader reader = query.ExecuteReader();
        query.Dispose();
        return reader;
    }

    // Can't find native C# function, so use sqlite function instead
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

    public void CloseConnection()
    {
        //Debug.Log("Closing connection...");
        this.dbCon.Close();
        this.isOpen = false;
    }

    public bool Authenticate(string username, string password)
    {
        string hash = Sha256Hash(password);
        string storedHash = GetHash(username);

        return (hash == storedHash);
    }

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
