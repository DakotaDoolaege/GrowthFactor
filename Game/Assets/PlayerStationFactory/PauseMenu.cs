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
		/// Ends the level by setting the remaining time to 0
		/// </summary>
		public void EndLevel()
		{
			Debug.Log(GameObject.FindGameObjectWithTag("Driver").GetComponent("Driver"));
			GameVariables.EndLevel = true;
			GameVariables.Paused = false;
			//gameObject.SetActive(false);
			//EndLevelScript.CreatePlayerPanels();
			//gameObject.SetActive(true);
		}


	}
}
