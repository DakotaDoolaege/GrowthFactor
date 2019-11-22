using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
    /// Loads main game scene when player clicks play button
    /// </summary>
    public void StartLevel()
    {
        //check if there is any players selected
        if (GameVariables.PlayerStations.Count > 0)
        {
            // call to coroutine
            StartCoroutine("Wait");
        }
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

    public void ShowScore()
    {
        //Rect buttonRect = new Rect();
        //PopupWindow.Show(buttonRect, new ScoreWindow());
        //SceneManager.LoadScene("Scores");
        SceneManager.LoadScene("Scoreboard");
    }
    /// <summary>
    /// Loads the main menu
    /// </summary>
    public void ReturnToMenu()
    {
        string menuName = "Menu";
        StartCoroutine(LoadMainMenu(menuName));
    }

    /// <summary>
    /// Loads the theme picker menu
    /// </summary>
    public void LoadThemesMenu()
    {
        string themeMenu = "ThemePickerMenu";
        StartCoroutine(LoadMainMenu(themeMenu));
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    /// <summary>
    /// Loads the scene with the name menuName
    /// </summary>
    /// <param name="menuName">The name of the scene to load</param>
    /// <returns>
    /// IEnumerator in order to have to coroutine load the menu in the background
    /// </returns>
    IEnumerator LoadMainMenu(string menuName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(menuName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
