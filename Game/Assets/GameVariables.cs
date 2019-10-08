using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    private static int playerCount, score; //Keeps data between scenes


    /// <summary>
    /// PlayerCount handles requests to change the number of players to be created
    /// for the next game and also can be used to get the number of players at any point
    /// in the game for calculating score
    /// </summary>
    public static int PlayerCount
    {
        get
        {
            return playerCount;
        }
        set
        {
            playerCount = value;
            Debug.Log("Player count: "+ playerCount);
        }
    }

    /// <summary>
    /// Score is a variable that can be modified at any point in the game 
    /// Created for holding score between scenes
    /// TODO: Need to build this out and use it, maybe replace with list of sccores for each player?
    /// </summary>
    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

}
