using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes

/// <summary>
/// Deals with every function related to the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads main game scene when player clicks play button
    /// </summary>
    public void PlayGame()
    {
        // call to coroutine
        StartCoroutine("Wait");
        
    }

    /// <summary>
    /// Corountine will load the next scene after waiting for half a second
    /// </summary>
    /// <returns>none</returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f); // wait 1/2 a second

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // get the scene next in the queue after current 
    }
}
