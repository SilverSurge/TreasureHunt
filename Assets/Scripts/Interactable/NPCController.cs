using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] string npc_name;

    GameController universal_game_controller;
    DialogManager universal_dialog_manager;
    PlayerDS player_ds;

    private void Start()
    {
        player_ds = FindAnyObjectByType<PlayerDS>();
        universal_dialog_manager = FindObjectOfType<DialogManager>();
        universal_game_controller = FindObjectOfType<GameController>();
        // Debug.Log(universal_game_controller);
    }
    public void Interact()
    {
        //DialogManager.Instance.ShowDialog()
        universal_game_controller.flip_state();
        
        Debug.Log("You Will talk to this NPC");
        universal_dialog_manager.ShowDialog(dialog);
        universal_game_controller.flip_state();

        if(npc_name == "shovel_guy")
        {
            Debug.Log("SHOVEL GUY FOUND");
            player_ds.gets_shovel();    
        }
        if(npc_name == "compass_guy")
        {
            Debug.Log("COMPASS GUY FOUND");
            player_ds.gets_compass();
        }

    }
}
