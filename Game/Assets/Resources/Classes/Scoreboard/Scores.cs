using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scores
{
    static Database Connection { get; set; } = GameObject.FindGameObjectWithTag("Database").GetComponent<Database>();
    static IList AllScores { get; set; } = Connection.TopSingle();

    public static IList<string> GetScore(int place)
    {
        return (List<string>) AllScores[place];
    }
}
