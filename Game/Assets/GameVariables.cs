using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/// <summary>
/// Contains values and objects that need to exist for entire games or passed between unrelated scripts
/// </summary>
public static class GameVariables
{
    private static List<PlayerStation> GamePlayers = new List<PlayerStation>();
    private static bool paused;
	private static int PlayersReady; //used to track that all players are ready to move to next scene
	private static Database DB;

	/// <summary>
	/// PlayerStation objects are active players that can be passed between scenes, they contain the location, name and score and related methods
	/// </summary>
public class PlayerStation : IEquatable<PlayerStation>
    {
        private int Score;
        private Vector3 Position;
        private string Name;

		/// <summary>
		/// Sets the score for the player
		/// </summary>
		/// <param name="Score"></param>
        public void SetScore(int Score)
        {
            this.Score = Score;
        }

		/// <summary>
		/// Sets the position of the player
		/// </summary>
		/// <param name="Position"></param>
        public void SetPosition(Vector3 Position)
        {
            this.Position = Position;
        }

		/// <summary>
		/// Sets the name for the player
		/// </summary>
		/// <param name="Name"></param>
        public void SetName(string Name) 
        {
            this.Name = Name;
        }

		/// <summary>
		/// Gets the score for the player
		/// </summary>
		/// <returns></returns>
        public int GetScore()
        {
            return this.Score;
        }

		/// <summary>
		/// Gets the position
		/// </summary>
		/// <returns></returns>
        public Vector3 GetPosition()
        {
            return this.Position;
        }

		/// <summary>
		/// Gets the name of the player
		/// </summary>
		/// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

		/// <summary>
		/// Saves the score of the player and their name to the database
		/// </summary>
        public void SaveScore()
        {
            if (this.Name != null && this.Score > 0)
            {
				DB = GameObject.Find("DBScript").GetComponent<Database>();
				DB.InsertScore(this.Name, this.Score.ToString(), "1");
            }
        }

		/// <summary>
		/// Tests if one player equals another by comparing their location 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
        public bool Equals(PlayerStation other)
        {
            if (other == null)
                return false;
            if (this.Position == other.Position)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// List of players 
    /// </summary>
    public static List<PlayerStation> PlayerStations
    {
        get
        {
            return GamePlayers;
        }
        set
        {
            GamePlayers = value;
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

    /// <summary>
    /// Tracks if all players in the game are ready to move to the next scene
    /// </summary>
    public static int Ready
    {
        get
        {
            return PlayersReady;
        }
        set
        {
            PlayersReady = value;
        }
    }


}
