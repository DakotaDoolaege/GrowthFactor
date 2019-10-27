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
        this.dbFile = Application.dataPath + "/Database/test_db.db";
        this.conStr = "URI=file:" + this.dbFile;
        bool needCreate = false;

        Debug.Log(this.conStr);         // Check
        // If DB doesn't exist, make it
        if (! File.Exists(this.dbFile))
        {
            needCreate = true;
            SqliteConnection.CreateFile(dbFile);
            Debug.Log("DB doesn't exist. Creating...");
        }

        // Open a connection to the database
        this.dbCon = new SqliteConnection(this.conStr);
        if (this.dbCon.State == ConnectionState.Open)
            return;
        Debug.Log("Opening connection...");
        this.dbCon.Open();
        this.isOpen = true;

        // If DB didn't exst and was just created, make tables
        if (needCreate)
        {
            string createScript = File.ReadAllText(Application.dataPath + "/Database/create.sql");
            string testDataScript = File.ReadAllText(Application.dataPath + "/Database/loadtestdata.sql");

            IDbCommand makeTables = this.dbCon.CreateCommand();
            //makeTables.Connection = this.dbCon;
            makeTables.CommandText = createScript;
            makeTables.ExecuteNonQuery();
            Debug.Log("Creating tables...");

            IDbCommand testData = this.dbCon.CreateCommand();
            testData.CommandText = testDataScript;
            testData.ExecuteNonQuery();
            Debug.Log("Populating with test data...");

            needCreate = false;
        }
        //TopSingle();
        //CloseConn();

        //Debug.Log();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IList TopSingle()
    {
        string query = "SELECT * FROM SinglePlayer ORDER BY - Score LIMIT 10";
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

            string output = name + ": " + score;
            double diff = JulianDay() - double.Parse(results.GetValue(1).ToString());
            output += ", " + diff.ToString() + " days ago";
            Debug.Log(output);
        }
        results.Close();
        return records;
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
        double julianDay = 0.0;
        while (result.Read())
        {
            julianDay = double.Parse(result.GetValue(0).ToString());
        }
        return julianDay;
    }

    public void CloseConn()
    {
        Debug.Log("Closing connection...");
        this.dbCon.Close();
    }
}
