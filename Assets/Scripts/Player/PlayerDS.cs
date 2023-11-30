using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDS : MonoBehaviour
{
    [SerializeField] float threshold;
    public bool has_shovel;
    public bool has_compass;
    private bool victory;
    public Vector2 treasure_coordinates;

    [SerializeField] GameObject compass_img;
    [SerializeField] GameObject shovel_img;
    [SerializeField] GameObject treasure_img;

    DialogManager universal_dialog_manager;
    private void Awake()
    {
        victory = false;
        has_shovel = false; 
        has_compass = false;
        universal_dialog_manager = FindObjectOfType<DialogManager>();
        int randnum = Random.Range(0, 3);
        if(randnum == 0)
            treasure_coordinates = new Vector2(-15, 4);
        else if(randnum == 1)
            treasure_coordinates = new Vector2(23, 10);
        else if(randnum == 2)
            treasure_coordinates = new Vector2(15, 11);
        else
            treasure_coordinates = new Vector2(-22, -24);



    }

    private void Update()
    {
        if(victory)
        {
            treasure_img.SetActive(true);
            Debug.Log("YOU FOUND THE TREASURE");
        }
    }
    public void gets_shovel()
    {
        has_shovel = true;
        shovel_img.SetActive(true);
    }

    public void gets_compass()
    {
        has_compass = true;
        compass_img.SetActive(true);
    }

    public void use_compass()
    {
        if (!has_compass)
            return;
        bool flag = (new Vector2(transform.position.x, transform.position.y) - treasure_coordinates).sqrMagnitude <= threshold;
        Dialog diag = new Dialog();
        if (flag)
            diag.line = "You're getting hotter! The treasure is just around the corner – keep going!";
        else
            diag.line = "The mystery remains a cold case. Keep searching for clues to unravel the secrets hidden in the frosty shadows.";

        universal_dialog_manager.ShowDialog(diag);
    }

    public void use_shovel()
    {
        if (!has_shovel)
            return;
        Dialog diag = new Dialog();
        
        if (transform.position.x == treasure_coordinates.x && transform.position.y == treasure_coordinates.y)
            victory = true;

        if(victory == false)
        {
            diag.line = "Using The Shovel ...... \nNothing was found";
            universal_dialog_manager.ShowDialog(diag);
        }

    }

    public void use_cheat()
    {
        treasure_coordinates = transform.position; 
    }
}
