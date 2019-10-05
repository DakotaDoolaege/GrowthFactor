using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes

/// <summary>
/// Deals with every fucntion related to the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads main game scene when player clicks play button
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // get the scene next in the queue after current 
    }
}
