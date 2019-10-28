using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes


public class EndLevelScript : MonoBehaviour
{

    public GameObject ScoresOverlay;


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

    public void MainMenu()
    {
        gameObject.SetActive(false);
        ScoresOverlay.SetActive(true); 
        GameVariables.Paused = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
    }

}
