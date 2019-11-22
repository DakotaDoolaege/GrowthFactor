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
		public int Level = 0;
		public IList<Blob> Players => this.PlayerInstantiator.CurrentBlobs;
		public int NumPlayers;
		public const int MillisecondsPerSecond = 1000;
		public bool isTutorial = false;

		/// <summary>
		/// The actual timer that counts down in real time
		/// </summary>
		public Timer CountDown { get; } = new Timer(MillisecondsPerSecond);

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
		/// Array of objects to show when the pause screen is shown
		/// </summary>
		private GameObject _pauseMenu;

		/// <summary>
		/// Array of objects to show when the level ended screen is show
		/// </summary>
		private GameObject[] _endObjects;

		// Start is called before the first frame update
		void Start()
		{
			this.GameTheme = ApplicationTheme.GetTheme();
			//GameTheme = new DefaultGameTheme();
			//GameTheme = new KnightTheme();
			//this.GameTheme = this.gameObject.AddComponent<DefaultGameTheme>();
			this.SetBackground();

			GetPlayerCount();
			this._pauseMenu = GameObject.FindGameObjectWithTag("ShowOnPause");
			this._endObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");
			this.HidePaused();
			this.HideEnded();

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

			render.sprite = this.GameTheme.GetBackground();

			ApplicationTheme.ScaleBackground(this.Background, this.GameTheme.GetBackground());
		}


		/// <summary>
		/// Hides the pause screen
		/// </summary>
		public void HidePaused()
		{
			Time.timeScale = 1;
			this._pauseMenu.SetActive(false);

			// foreach (GameObject obj in this._pauseObjects)
			// {
			//     obj.SetActive(false);
			// }
		}

		/// <summary>
		/// Hides the level ended pause screen
		/// </summary>
		public void HideEnded()
		{
			Time.timeScale = 1;
			foreach (GameObject obj in this._endObjects)
			{
				obj.SetActive(false);
			}
		}

		/// <summary>
		/// Shows the pause screen
		/// </summary>
		public void ShowPaused()
		{

			// foreach (GameObject obj in this._pauseObjects)
			// {
			//     Debug.Log("here");
			//     obj.SetActive(true);
			// }
			this._pauseMenu.SetActive(true);
			Time.timeScale = 0;
		}

		/// <summary>
		/// Shows the level ended pause screen
		/// </summary>
		public void ShowEnded()
		{
			Time.timeScale = 0;

			foreach (GameObject obj in this._endObjects)
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
			((ConsumableInstantiator)this.ConsumableInstantiator).Level++;

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
				this.ShowEnded();
				this.UpdateScores();

			}

			if (GameVariables.Paused)
			{
				this.ShowPaused();
				this.UpdateScores();
			}
			else
			{
				this.HidePaused();
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
			if (!GameVariables.Paused && !this.isTutorial)
			{
				this.TimerCount--;
			}

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
				if (blob.Renderer != null && ((Player)blob).MaxRadius >= this.GetWinningRadius())
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
					player.Score = player.FoodValue; //+ this.TimerCount;
													 //ScoreDisplay.text = "Score: " + player.Score.ToString();

				}
			}

			List<GameVariables.PlayerStation> GameStations = GameVariables.PlayerStations;

			for (int i = 0; i < GameStations.Count; i++)
			{
				Player player = this.Players[i] as Player;
				GameStations[i].SetScore(player.Score);
			}

		}

		/// <summary>
		/// Setup routine for when a level has ended. Scores are updated here
		/// and the remainder of the score screen should be constructed. 
		/// </summary>
		public void OnEndLevel()
		{
			Debug.Log("LEVEL END");
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
			if (NumPlayers == 0)
				NumPlayers = GameVariables.PlayerStations.Count;
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
