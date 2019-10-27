using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    private static int score; //Keeps data between scenes
    private static List<Vector3> playerPositions = new List<Vector3>();
    private static bool paused;

    /// <summary>
    /// PlayerPositions handles requests to change the number of players to be created
    /// for the next game and also can be used to get the number of players and location at any point
    /// in the game for calculating score
    /// </summary>
    public static List<Vector3> Players
    {
        get
        {
            return playerPositions;
        }
        set
        {
            playerPositions = value;
            //Debug.Log("Player count: " + playerPositions.Count);
            for (int i = 0; i < playerPositions.Count; i++)
            {
                //Debug.Log("Player Location: " + playerPositions[i]);
            }
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


    /// <summary>
    /// Tracks if the game is active or not
    /// </summary>
    public static bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }

}
