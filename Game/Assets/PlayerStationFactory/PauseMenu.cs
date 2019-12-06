using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PlayerStationFactory.PauseMenu
{
	public class PauseMenu : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		/// <summary>
		/// Resumes the game
		/// </summary>
		public void ResumeGame()
		{
			GameVariables.Paused = false;
		}

		/// <summary>
		/// Restarts the level to beginning by "switching" to it again
		/// </summary>
		public void RestartLevel()
		{
			GameVariables.Paused = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
		}

		/// <summary>
		/// Ends the level 
		/// </summary>
		public void EndLevel()
		{
			GameVariables.Paused = false;
			GameVariables.EndLevel = true;
			//gameObject.SetActive(false);
			//EndLevelScript.CreatePlayerPanels();
			//gameObject.SetActive(true);
		}
		
		/// <summary>
		/// Quits the game without saving scores 
		/// </summary>
		public void QuitGame()
		{
			GameVariables.Paused = false;
			GameVariables.EndLevel = false;
			GameVariables.ShowScores = false;
			GameVariables.Ready = 0;
			GameVariables.ClearPlayers();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
			//gameObject.SetActive(false);
			//EndLevelScript.CreatePlayerPanels();
			//gameObject.SetActive(true);
		}


	}
}
