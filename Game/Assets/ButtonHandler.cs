using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Classes.Driver;

[RequireComponent(typeof(Button))]
public class ButtonHandler : MonoBehaviour
{
    public Driver Driver;
    public Sprite activated;
    public Sprite deactivated;
    public bool active; //0 is off and 1 is activated
    public Button Player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Player = GetComponent<Button>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        Debug.Log("Here");
    }

    public void SetStatus(){
        Player.image.overrideSprite = activated;
        if (active)
        {
            Driver.NumPlayers--;
            active = false;
            Player.image.overrideSprite = deactivated;
        }
        else
        {
            Driver.NumPlayers++;
            active = true;
            Player.image.overrideSprite = activated;
            Debug.Log("Activated: " + active);
        }
        }

}
