using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    private static List<PlayerStation> GamePlayers = new List<PlayerStation>();
    private static bool paused;

public class PlayerStation : IEquatable<PlayerStation>
    {
        private int Score;
        private Vector3 Position;
        private string Name;

        public void SetScore(int Score)
        {
            this.Score = Score;
        }

        public void SetPosition(Vector3 Position)
        {
            this.Position = Position;
        }
        public void SetName(string Name) 
        {
            this.Name = Name;
        }

        public int GetScore()
        {
            return this.Score;
        }

        public Vector3 GetPosition()
        {
            return this.Position;
        }

        public string GetName()
        {
            return this.Name;
        }

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

}
