using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // change scenes
using TMPro;


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
        GameVariables.Paused = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
    }
    public void MainMenu()
    {
        gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);

        GameVariables.Paused = false;
        GameVariables.Players = new List<Vector3>();
        GameVariables.PlayerList = new List<GameVariables.PlayerStation>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex); // get the scene next in the queue after current 
    }

    private void CreatePlayerPanels()
    {

        GameObject overlay = gameObject.transform.gameObject;
        overlay.transform.Translate(new Vector3(0, 0, 0));
        for (int i = 0; i < GameVariables.Players.Count; i++)
        {
            Vector3 startPosition = GameVariables.Players[i];
            Debug.Log(startPosition);
            if (startPosition.y > 1500)
            {
                startPosition.y = startPosition.y - 50;
                GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-180, Vector3.forward)) as GameObject;
                Keyboard.transform.SetParent(gameObject.transform, false);
                TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
                Score.text = GameVariables.PlayerList[i].GetScore();
            }
            else if (startPosition.x < 500)
            {
                GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(-90, Vector3.forward)) as GameObject;
                Keyboard.transform.SetParent(gameObject.transform, false);
                TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
                Score.text = GameVariables.PlayerList[i].GetScore();

            }
            else if (startPosition.x > 3000)
            {
                GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.AngleAxis(90, Vector3.forward)) as GameObject;
                Keyboard.transform.SetParent(gameObject.transform, false);
                TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
                Score.text = GameVariables.PlayerList[i].GetScore();

            }
            else
            {
                startPosition.y = startPosition.y + 50;
                GameObject Keyboard = Instantiate(PlayerPanel, startPosition, Quaternion.identity) as GameObject;
                Keyboard.transform.SetParent(gameObject.transform, false);
                TextMeshProUGUI Score = Keyboard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
                Score.text = GameVariables.PlayerList[i].GetScore();

            }
            

            Debug.Log("Setting parent to:" + gameObject.name);
        }
    }

}
