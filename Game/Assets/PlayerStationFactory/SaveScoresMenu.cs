using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveScoresMenu : MonoBehaviour
{

	public int PlayerNumber;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Adds the inputed player name to their player station if it is not profane
	/// </summary>
	/// <param name="Name">The players name</param>
	public void SaveName(Text Name)
	{
		string username = Name.text;
		if (Profanity.ValidateName(username))
		{
			GameVariables.PlayerStations[PlayerNumber].SetName(username);
			Name.transform.parent.parent.gameObject.SetActive(false);
			GameVariables.Ready++;


			if (GameVariables.Ready == GameVariables.PlayerStations.Count) //Calls main menu if all players have finished
				MainMenu();
		}
		else
		{
			//Todo: clear input and alert user to change their name
		}
	}
	/// <summary>
	/// Doesn't save users score, just deactivates their PlayerStation
	/// </summary>
	/// <param name="Name"></param>
	public void NoSave(GameObject PlayerStation)
	{
		GameVariables.Ready++;
		PlayerStation.SetActive(false);
		if (GameVariables.Ready == GameVariables.PlayerStations.Count) //Calls main menu if all players have finished
			MainMenu();
	}

	/// <summary>
	/// Switches to the main menu
	/// To be called after saving all the names for players at the end of the game.
	/// </summary>
	/// <remarks>
	/// Good point to add any resets or database saving for things that are not to be carried to the next game.
	/// </remarks>
	public void MainMenu()
	{
		SaveScores();
		GameVariables.SaveScores();
		GameVariables.ShowScores = false;
		GameVariables.Paused = false;
		GameVariables.Ready = 0;
		GameVariables.PlayerStations = new List<GameVariables.PlayerStation>();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
																													 // Destroy(FindObjectOfType<Driver>());
	}

	/// <summary>
	/// Saves the scores for the PlayerStations to the database
	/// </summary>
	private void SaveScores()
	{
//		int PlayerNum;
//		foreach (Transform child in transform.parent)
//		{
//			if (child.name == "PlayerInputPanel(Clone)")
//			{
//				PlayerNum = child.GetComponent<EndLevelMenu>().PlayerNumber;
//				GameVariables.PlayerStations[PlayerNum].SaveScore();
//			}
//		}


	}
}
