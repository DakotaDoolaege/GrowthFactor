using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHandler : MonoBehaviour
{
    public Sprite activated;
    public Sprite deactivated;
    public bool active; //0 is off and 1 is activated
    public Button Player;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        Player = GetComponent<Button>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
    }


    public void PauseGame()
    {
        Debug.Log("STOPING THE GAME!!");
    }

    /// <summary>
    /// Used to update the player count and also the sprite for each player position
    /// </summary>
    public void SetStatus(){
        Player.image.overrideSprite = activated;
        if (active)
        {
            List<GameVariables.PlayerStation> GameStations = GameVariables.PlayerStations;
            GameVariables.PlayerStation Station = new GameVariables.PlayerStation();
            Station.SetPosition(transform.position);
            GameStations.Remove(Station);
            GameVariables.PlayerStations = GameStations;
            //Debug.Log("Players counter after remove:" + GameVariables.PlayerStations.Count);


            active = false;
            Player.image.overrideSprite = deactivated;
        }
        else
        {
            List<GameVariables.PlayerStation> GameStations = GameVariables.PlayerStations;
            GameVariables.PlayerStation Station = new GameVariables.PlayerStation();
            Station.SetPosition(transform.position);
            GameStations.Add(Station);
            GameVariables.PlayerStations = GameStations;
            //Debug.Log("Players counter" + GameVariables.PlayerStations.Count + "player position: " + Station.GetPosition());

            
            active = true;
            Player.image.overrideSprite = activated;

        }
        }

}
