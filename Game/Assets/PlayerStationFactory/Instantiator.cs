using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="Prefab">The object to instantiate</param>
	/// <param name="StartPosition">Position to instantiate at</param>
	/// <param name="Rotation">Rotation for the object</param>
	/// <param name="Player">Which playerstation object is at</param>
	public void InstantiateObject(GameObject Prefab, Vector3 StartPosition, Quaternion Rotation, int Player)
	{

		GameObject NewObject = Instantiate(Prefab, StartPosition, Quaternion.AngleAxis(-180, Vector3.forward)) as GameObject;
		NewObject.transform.SetParent(gameObject.transform, false);
		TextMeshProUGUI Score = NewObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
		Score.text = GameVariables.PlayerStations[Player].GetScore().ToString();
		NewObject.transform.GetComponent<SaveScoresMenu>().PlayerNumber = Player;

	}

}
