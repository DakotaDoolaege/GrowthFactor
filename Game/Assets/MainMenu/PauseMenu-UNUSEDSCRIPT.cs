using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuOld : MonoBehaviour
{
    public static bool GamePaused = false; // keeps track if game paused or not 

    public GameObject PauseMenuUI; // reference to pause menu


    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().onClick.AddListener(PauseGame);
    }

    /// <summary>
    /// Resumes the game from paused state
    /// </summary>
    void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // set game speed back to normal
        GamePaused = false;
    }

    /// <summary>
    /// Pauses the game and reduces de speed of the movement in the background to half
    /// </summary>
    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.5f;
        GamePaused = true; 

    }
}
