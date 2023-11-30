using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement player_controller;

    GameState state;

    private void Start()
    {
        state = GameState.FreeRoam;
    }

    public void flip_state()
    {
        Debug.Log("Flipping the state");
        state = state == GameState.FreeRoam ? GameState.Dialog : GameState.FreeRoam;
    }
    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            player_controller.HandleUpdate();
        }
        else if(state == GameState.Dialog)
        {
            //DialogManager.Instance.HandleUpdate();
        }
        
    }
}
