using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationFactory : MonoBehaviour
{
	public GameObject Prefab; //the prefab to instantiate at each player station
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("hello worldd");
		CreateStationControls(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	/// <summary>
	/// Creates the station controls when the scene is loaded
	/// </summary>
	private void CreateStationControls()
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
				Keyboard = Instantiate(Prefab, startPosition, Quaternion.AngleAxis(-180, Vector3.forward)) as GameObject;
			}
			else if (startPosition.x < 500)
			{
				Keyboard = Instantiate(Prefab, startPosition, Quaternion.AngleAxis(-90, Vector3.forward)) as GameObject;

			}
			else if (startPosition.x > 3000)
			{
				Keyboard = Instantiate(Prefab, startPosition, Quaternion.AngleAxis(90, Vector3.forward)) as GameObject;
			}
			else
			{
				startPosition.y = startPosition.y + 50;
				Keyboard = Instantiate(Prefab, startPosition, Quaternion.identity) as GameObject;
			}
				Keyboard.transform.SetParent(gameObject.transform, false);
				TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
				//Score.text = GameVariables.PlayerStations[i].GetScore().ToString();
				//Keyboard.transform.GetComponent<EndLevelScript>().PlayerNumber = i;
		}
	}
}
