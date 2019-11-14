using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.IO;
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
            IDbCommand testData = this.dbCon.CreateCommand();
            testData.CommandText = testDataScript;
            testData.ExecuteNonQuery();
            testData.Dispose();
            //Debug.Log("Populating with test data...");

            needCreate = false;
        }

        // Test InsertScores method
        //InsertScore("TEST_NAME", "9999", "99");

        // Test RemoveScore method
        //RemoveScore("TEST_NAME");

        // Test purging of old scores
        //PurgeOld();
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
            IList entry = new ArrayList();
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
}
