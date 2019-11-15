using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes
using TMPro;
using UnityEngine.UI;


/// <summary>
/// Handles pause, end and player name input UI logic
/// </summary>
public class EndLevelScript : MonoBehaviour
{
	public GameObject PlayerPanel;
	public int PlayerNumber;

	/// <summary>
	/// Resumes the game
	/// </summary>
	public void ResumeGame()
	{
		GameVariables.Paused = false;
	}

	/// <summary>
	/// Moves to the next level
	/// </summary>
	/// <remarks>
	/// Once we add additional levels we can use this to either move to a new scene like we are doing now
	/// OR
	/// Resettings values similar to the implemented restart method and then adding various tweaks to make the level harder
	/// </remarks>
	public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // get the scene next in the queue after current 
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
		CreatePlayerPanels();
		gameObject.SetActive(true);
		//GameVariables.Paused = false;
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
		GameVariables.Paused = false;
		GameVariables.Ready = 0;
		GameVariables.PlayerStations = new List<GameVariables.PlayerStation>();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
	}

	/// <summary>
	/// Adds the inputed player name to their player station if it is not profane
	/// </summary>
	/// <param name="Name">The players name</param>
	public void SaveName(Text Name)
	{
		string username = Name.text;
		int PlayerNum = transform.GetComponent<EndLevelScript>().PlayerNumber;
		if (Profanity.ValidateName(username))
		{
			GameVariables.PlayerStations[PlayerNum].SetName(username);
			Name.transform.parent.parent.gameObject.SetActive(false);
			GameVariables.Ready++;

			if( GameVariables.Ready == GameVariables.PlayerStations.Count) //Calls main menu if all players have finished
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
			if( GameVariables.Ready == GameVariables.PlayerStations.Count) //Calls main menu if all players have finished
				MainMenu();
	}

	/// <summary>
	/// Saves the scores for the PlayerStations to the database
	/// </summary>
	private void SaveScores()
	{
		int PlayerNum;
		foreach (Transform child in transform.parent)
		{
			if (child.name == "PlayerInputPanel(Clone)")
			{
				PlayerNum = child.GetComponent<EndLevelScript>().PlayerNumber;
				GameVariables.PlayerStations[PlayerNum].SaveScore();
			}
		}

	}

	/// <summary>
	/// Creates the PlayerPanels when the SaveScoresOverlay is activated
	/// </summary>
	private void CreatePlayerPanels()
	{
		GameObject overlay = gameObject.transform.gameObject;
		overlay.transform.Translate(new Vector3(0, 0, 0));
		for (int i = 0; i < GameVariables.PlayerStations.Count; i++)
		{
			Vector3 startPosition = GameVariables.PlayerStations[i].GetPosition();
			GameObject Keyboard;
			
			if (startPosition.y > 1500)
			{
				startPosition.y = startPosition.y - 50;
				Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-180, Vector3.forward)) as GameObject;
			}
			else if (startPosition.x < 500)
			{
				Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-90, Vector3.forward)) as GameObject;

			}
			else if (startPosition.x > 3000)
			{
				Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(90, Vector3.forward)) as GameObject;
			}
			else
			{
				startPosition.y = startPosition.y + 50;
				Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.identity) as GameObject;
			}
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;
		}
	}

}
