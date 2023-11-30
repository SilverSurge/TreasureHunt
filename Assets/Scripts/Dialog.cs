using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] public string line;

    public string get_line
    {
        get { return line; }
    }
}
