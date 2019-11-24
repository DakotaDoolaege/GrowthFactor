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
		/// Restarts the level to beginning by "switching" to it again
		/// </summary>
		public void RestartLevel()
		{
			GameVariables.Paused = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
		}

		/// <summary>
		/// Activates the SaveScoresOverlay and instantiates keyboards at each player station
		/// </summary>
		public void SaveMenu()
		{
			gameObject.SetActive(false);
			//EndLevelScript.CreatePlayerPanels();
			gameObject.SetActive(true);
			//GameVariables.Paused = false;
		}


	}
}
