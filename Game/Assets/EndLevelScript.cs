using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes
using TMPro;
using UnityEngine.UI;


public class EndLevelScript : MonoBehaviour
{
	public GameObject PlayerPanel;
	public int PlayerNumber;
	public void ResumeGame()
	{
		GameVariables.Paused = false;
	}

	public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // get the scene next in the queue after current 
	}

	public void RestartLevel()
	{
		GameVariables.Paused = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
	}

	public void SaveMenu()
	{
		gameObject.SetActive(false);
		CreatePlayerPanels();
		gameObject.SetActive(true);
		GameVariables.Paused = false;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
	}

	public void MainMenu()
	{
		//gameObject.SetActive(false);
		//gameObject.transform.parent.gameObject.SetActive(false);

		SaveScores();
		GameVariables.Paused = false;
		GameVariables.PlayerStations = new List<GameVariables.PlayerStation>();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
	}


	public void SaveName(Text Name)
	{
		string username = Name.text;

		int PlayerNum = transform.GetComponent<EndLevelScript>().PlayerNumber;
		if (Profanity.ValidateName(username))
		{
			GameVariables.PlayerStations[PlayerNum].SetName(username);
			Name.transform.parent.parent.gameObject.SetActive(false);
			CheckLast();
		}
		else
		{
			Debug.Log("We can't use " + username);
			//Todo: clear input and alert user to change their name
		}
 }

	private void CheckLast()
	{

		//will check to see if all players have exited or entered a name

		MainMenu();

	}

	private void SaveScores()
	{
		Debug.Log("Object is: " + transform.parent);// savescoresoverlay
		int PlayerNum;
		foreach (Transform child in transform.parent)
		{
			if (child.name == "PlayerInputPanel(Clone)")
			{
				Debug.Log("Child is: " + child + "Child playernumber is: " + child.GetComponent<EndLevelScript>().PlayerNumber);
				Debug.Log("Child position: " + child.transform.position + "corresponding playerstation location: " + GameVariables.PlayerStations[child.GetComponent<EndLevelScript>().PlayerNumber].GetPosition());
				Debug.Log("name is: " + child.transform.GetChild(1).gameObject.transform.GetChild(2).GetComponent<Text>().text);

				PlayerNum = child.GetComponent<EndLevelScript>().PlayerNumber;
				
					

				GameVariables.PlayerStations[PlayerNum].SaveScore();
			}
		}

	}

	private void CreatePlayerPanels()
	{

		GameObject overlay = gameObject.transform.gameObject;
		overlay.transform.Translate(new Vector3(0, 0, 0));
		for (int i = 0; i < GameVariables.PlayerStations.Count; i++)
		{
			Vector3 startPosition = GameVariables.PlayerStations[i].GetPosition();
			Debug.Log(startPosition);
			if (startPosition.y > 1500)
			{
				startPosition.y = startPosition.y - 50;
				GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-180, Vector3.forward)) as GameObject;
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;
			}
			else if (startPosition.x < 500)
			{
				GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-90, Vector3.forward)) as GameObject;
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;

			}
			else if (startPosition.x > 3000)
			{
				GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(90, Vector3.forward)) as GameObject;
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;

			}
			else
			{
				startPosition.y = startPosition.y + 50;
				GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.identity) as GameObject;
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;

			}
			

			Debug.Log("Setting parent to:" + gameObject.name);
		}
	}

}
