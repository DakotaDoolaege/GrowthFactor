using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminVerification : MonoBehaviour
{
	public GameObject SoundButton;
	public GameObject RemoveName;
	public GameObject LoginUI;

	//add remaining authenticated admin options here

	private static Database DB;
    // Start is called before the first frame update
    void Start()
    {
		DB = GameObject.Find("DBScript").GetComponent<Database>();
 }

	/// <summary>
	/// Logs the admin in and shows the full menu
	/// </summary>
	/// <param name="Password"></param>
	public void Login(InputField Password)
	{
		if(DB.Authenticate("admin",Password.text) == true)
		{
			Debug.Log("User authenticated");
			SoundButton.SetActive(true);
			RemoveName.SetActive(true);
			LoginUI.SetActive(false);
			Password.text = "";
			//Password.transform.parent.gameObject.SetActive(false);
		}
	}

	public void Logout()
	{
		// SoundButton.SetActive(false);
		RemoveName.SetActive(false);
		LoginUI.SetActive(true);
	}

	/// <summary>
	/// Removes a score from the database
	/// </summary>
	/// <param name="Name"></param>
	public void DeleteName(InputField Name)
	{
		DB.RemoveScore(Name.text);
		Profanity.BlacklistWord(Name.text);
		Name.text = "";

	}




}
