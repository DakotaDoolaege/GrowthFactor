using System.Collections.Generic;
using System.Timers;
using Assets.Resources.Classes.Blobs;
using UnityEngine;
using Assets.Resources.Classes.Instantiator;

namespace Assets.Resources.Classes.Driver
{
    public class Driver : MonoBehaviour
    {
        public Instantiator.Instantiator PlayerInstantiator;
        public Instantiator.Instantiator ConsumableInstantiator;
        private int Level { get; set; } = 1;
        public IList<Blob> Players => this.PlayerInstantiator.CurrentBlobs;

        public const int MillisecondsPerSecond = 1000;
        public Timer CountDown { get;} = new Timer(MillisecondsPerSecond);
        public int TimerCount { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            this.StartLevel();
        }

        public void StartLevel()
        {
            Level++;
            this.TimerCount = GetLevelTime();
            Debug.Log("Start time: " + TimerCount);

            this.PlayerInstantiator = this.gameObject.AddComponent<PlayerInstantiator>();
            this.ConsumableInstantiator = this.gameObject.AddComponent<ConsumableInstantiator>();

            this.CountDown.Elapsed += this.IncrementTimerCount;
            this.CountDown.Start();
        }

        // Update is called once per frame
        void Update()
        {
            if (this.CheckWin())
            {
                this.OnEndLevel();
            }
        }

        public int GetLevelTime()
        {
            int extraSecondsPerLevel = 5;
            int baseSecondsPerLevel = 0;
            return baseSecondsPerLevel + (this.Level * extraSecondsPerLevel);
        }

        public void IncrementTimerCount(object source, ElapsedEventArgs e)
        {
            this.TimerCount--;
            Debug.Log(this.TimerCount);

            if (this.TimerCount == 0)
            {
                this.OnEndLevel();
            }
        }

        public bool CheckWin()
        {
            foreach (Blob blob in this.Players)
            {
                if (((Player) blob).MaxRadius >= this.GetWinningRadius())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates each of the current player's scores
        /// </summary>
        private void UpdateScores()
        {
            foreach (Blob blob in this.Players)
            {
                Player player = blob as Player;
                if (player != null)
                {
                    player.Score += player.FoodValue + this.TimerCount;
                }
            }
        }

        public void OnEndLevel()
        {
            // Do whatever needs to be done when the level ends
            // here

            Debug.Log("LEVEL ENDS");

            this.UpdateScores();

            // Pause Game Here
            // Display information from level
            // Go to next level
        }

        /// <summary>
        /// Determines the radius a player must get to win
        /// </summary>
        /// <returns>
        /// The radius that determines when a player wins
        /// </returns>
        private int GetWinningRadius()
        {
            return 100 + this.Level * 5;
        }
    }
}
