using System.Collections.Generic;
using System.Timers;
using Assets.Resources.Classes.Blobs;
using UnityEngine;
using Assets.Resources.Classes.Instantiator;
using Assets.Resources.Classes.Theme;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.XR.WSA.Input;

namespace Assets.Resources.Classes.Driver
{
    public class Driver : MonoBehaviour
    {
        public GameTheme GameTheme { get; set; }
        private GameObject Background { get; set; }

        public Instantiator.Instantiator PlayerInstantiator;
        public Instantiator.Instantiator ConsumableInstantiator;

        public TextMeshProUGUI ScoreDisplay;
        private int Level { get; set; } = 0;
        public IList<Blob> Players => this.PlayerInstantiator.CurrentBlobs;
        public int NumPlayers;
        public const int MillisecondsPerSecond = 1000;

        /// <summary>
        /// The actual timer that counts down in real time
        /// </summary>
        public Timer CountDown { get;} = new Timer(MillisecondsPerSecond);

        /// <summary>
        /// The variable that keeps track of the game seconds. It is an integer
        /// that we decrement every time the CountDown variable decreases by a
        /// second. Once this reaches 0, it's game over.
        /// </summary>
        public int TimerCount { get; set; }

        /// <summary>
        /// Flag that determines when the level has ended or not
        /// </summary>
        public bool LevelEnded { get; set; } = false;

        /// <summary>
        /// Array of objects to show when the level ended screen is show
        /// </summary>
        private GameObject[] _pauseObjects;

        // Start is called before the first frame update
        void Start()
        {

            GameTheme = new DefaultGameTheme();
            //GameTheme = new KnightTheme();
            //this.GameTheme = this.gameObject.AddComponent<DefaultGameTheme>();
            SetBackground();

            GetPlayerCount();
            this._pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");
            this.HidePaused();

            this.PlayerInstantiator = this.gameObject.GetComponent<PlayerInstantiator>();
            this.ConsumableInstantiator = this.gameObject.GetComponent<ConsumableInstantiator>();

            // Generate the players
            for (int i = 0; i < this.NumPlayers; i++)
            {
                this.PlayerInstantiator.GenerateBlob();
            }

            this.StartLevel();
        }

        /// <summary>
        /// Sets the game background from the GameTheme
        /// </summary>
        private void SetBackground()
        {
           this.Background = GameObject.FindGameObjectWithTag("Background");
            
           SpriteRenderer render = this.Background.GetComponent<SpriteRenderer>();

            render.sprite = this.GameTheme.Background;
        }


        /// <summary>
        /// Hides the level ended pause screen
        /// </summary>
        public void HidePaused()
        {
            Time.timeScale = 1;
            foreach (GameObject obj in this._pauseObjects)
            {
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// Shows the level ended pause screen
        /// </summary>
        public void ShowPaused()
        {
            Time.timeScale = 0;

            foreach (GameObject obj in this._pauseObjects)
            {
                obj.SetActive(true);
            }
        }

        /// <summary>
        /// Performs setup routine when starting a new level
        /// </summary>
        public void StartLevel()
        {
            Level++;
            ((ConsumableInstantiator) this.ConsumableInstantiator).Level++;

            this.TimerCount = GetLevelTime();

            // Calls this.IncrementTimerCount every time 1000 milliseconds
            // has ellapsed
            this.CountDown.Elapsed += this.IncrementTimerCount;

            this.CountDown.Start();
        }

        // Update is called once per frame
        void Update()
        {
            this.CheckWin();

            if (this.LevelEnded)
            {
                this.ShowPaused();
            }

        }

        /// <summary>
        /// Gets the allowable time per level
        /// </summary>
        /// <returns></returns>
        public int GetLevelTime()
        {
            int extraSecondsPerLevel = 5;
            int baseSecondsPerLevel = 60;
            return baseSecondsPerLevel + (this.Level * extraSecondsPerLevel);
        }

        /// <summary>
        /// Counts down the timer, ending the level when the timer reaches 0
        /// </summary>
        /// <param name="source">The timer calling the function</param>
        /// <param name="e">The arguments passed when the timer calls the function</param>
        public void IncrementTimerCount(object source, ElapsedEventArgs e)
        {
            this.TimerCount--;
            Debug.Log(this.TimerCount);

            if (this.TimerCount == 0)
            {
                this.OnEndLevel();
            }
        }

        /// <summary>
        /// Checks if a player has won by reaching the max radius
        /// </summary>
        public void CheckWin()
        {
            foreach (Blob blob in this.Players)
            {
                if (blob.Renderer != null && ((Player) blob).MaxRadius >= this.GetWinningRadius())
                {
                    this.OnEndLevel();
                }
            }
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
                    ScoreDisplay.text = "Score: " + player.Score.ToString();
                }
            }
        }

        /// <summary>
        /// Setup routine for when a level has ended. Scores are updated here
        /// and the remainder of the score screen should be constructed.
        /// </summary>
        public void OnEndLevel()
        {
            // Do whatever needs to be done when the level ends
            // here

            this.UpdateScores();

            this.LevelEnded = true;

            // Basically here we need to create the score screen I believe
        }
        
        /// <summary>
        /// Gets the number of players from the GameVariables if not preset in Inspector
        /// </summary>
        private void GetPlayerCount()
        {
            if(NumPlayers == 0)
                NumPlayers =  GameVariables.Players.Count;
        }

        /// <summary>
        /// Determines the radius a player must get to win
        /// </summary>
        /// <returns>
        /// The radius that determines when a player wins
        /// </returns>
        private int GetWinningRadius()
        {
            return 1 + this.Level * 3;
        }
    }
}
