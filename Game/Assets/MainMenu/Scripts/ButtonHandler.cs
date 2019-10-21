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


    /// <summary>
    /// Used to update the player count and also the sprite for each player position
    /// </summary>
    public void SetStatus(){
        Player.image.overrideSprite = activated;
        if (active)
        {
            List<Vector3> positions = GameVariables.Players;
            positions.Remove(transform.position);
            GameVariables.Players = positions;

            active = false;
            Player.image.overrideSprite = deactivated;
        }
        else
        {
            List<Vector3> positions = GameVariables.Players;
            positions.Add(transform.position);
            GameVariables.Players = positions;
            active = true;
            Player.image.overrideSprite = activated;

        }
        }

}
