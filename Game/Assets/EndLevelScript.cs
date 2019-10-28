using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes


public class EndLevelScript : MonoBehaviour
{
        public GameObject PlayerPanel;
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
        GameObject.Find("SaveScoresOverlay"); //.SetActive(true);
        Debug.Log("IT IS: " + gameObject.name);  
        GameVariables.Paused = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
    }
    public void MainMenu()
    {
        Debug.Log("PARENT: " + gameObject.transform.parent.name);
        gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);
        
        GameVariables.Paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
    }

    private void CreatePlayerPanels()
    {

        GameObject overlay = gameObject.transform.gameObject;
        Debug.Log("Object name: " + overlay.name);
        overlay.transform.Translate(new Vector3(0, 0, 0)); 
        for (int i = 0; i < GameVariables.Players.Count; i++)
            {
            Debug.Log("Creating keyboard");
                Vector3 startPosition = GameVariables.Players[i];
                startPosition.x = startPosition.x - 1920;//remove offset n replace with proper function
                startPosition.y = startPosition.y;

                GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.identity) as GameObject;
                Keyboard.transform.SetParent(gameObject.transform, false);
            Debug.Log("Setting parent to:" + gameObject.name);
            }
    }

}
