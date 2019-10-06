using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Classes.Driver;

public class PlayerPicker : MonoBehaviour
{

    public Driver Driver;
    public ButtonHandler player0;
    public ButtonHandler player1;
    public ButtonHandler player2;
    public ButtonHandler player3;
    public ButtonHandler player4;
    public ButtonHandler player5;

    public int activePlayers;
    public void ActivatePlayer()
    {
        if (activePlayers < 6)
        {
            activePlayers++;
        }
        Debug.Log("Active Players:" + activePlayers);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void AddPlayer()
    {

        Debug.Log("Added a player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
