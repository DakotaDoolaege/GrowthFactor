using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Scores</c> holds the top 10 current scores in the database.
/// </summary>
public static class Scores
{
    static Database Connection { get; set; } = GameObject.FindGameObjectWithTag("Database").GetComponent<Database>();
    // static IList AllScores { get; set; } = Connection.TopSingle();
    static IList AllScores { get => Connection.TopSingle(); }

    public static IList<string> GetScore(int place)
    {
        return (List<string>) AllScores[place];
    }
}
