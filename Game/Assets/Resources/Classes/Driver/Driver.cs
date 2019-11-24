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
		private GameObject Background { get; set; }

		public Instantiator.Instantiator PlayerInstantiator;
		public Instantiator.Instantiator ConsumableInstantiator;

		public TextMeshProUGUI ScoreDisplay;
		public int Level = 0;
		public IList<Blob> Players => this.PlayerInstantiator.CurrentBlobs;
		public int NumPlayers { get => GameVariables.PlayerStations.Count; }
		public const int MillisecondsPerSecond = 1000;

		/// <summary>
		/// The variable that keeps track of the game seconds. It is an integer
		/// that we decrement every time the CountDown variable decreases by a
		/// second. Once this reaches 0, it's game over.
		/// </summary>
		public float TimerCount { get; set; }

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
			this.SetBackground();

			// GetPlayerCount();
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

			render.sprite = ApplicationTheme.CurrentTheme.GetBackground();

			ApplicationTheme.ScaleBackground(this.Background, ApplicationTheme.CurrentTheme.GetBackground());
		}

		/// <summary>
		/// Hides the pause screen
		/// </summary>
		public void HidePaused()
		{
			Time.timeScale = 1;
			this._pauseMenu.SetActive(false);
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
			// this.CountDown.Stop();
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
			this.TimerCount = this.GetLevelTime();
		}

		// Update is called once per frame
		void Update()
		{
			this.CheckWin();

			if (this.LevelEnded || this.TimerCount <= 0.0f || GameVariables.EndLevel)
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

			this.TimerCount -= Time.deltaTime;
		}

		/// <summary>
		/// Gets the allowable time per level
		/// </summary>
		/// <returns></returns>
		public float GetLevelTime()
		{
			float extraSecondsPerLevel = 5.0f;
			float baseSecondsPerLevel = 25.0f;
			return baseSecondsPerLevel + (this.Level * extraSecondsPerLevel);
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
					this.LevelEnded = true;
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
		/// Determines the radius a player must get to win
		/// </summary>
		/// <returns>
		/// The radius that determines when a player wins
		/// </returns>
		private float GetWinningRadius()
		{
			return 3.0f + this.Level;
		}
	}
}
