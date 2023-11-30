using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    
    [SerializeField] GameObject dialog_box;
    [SerializeField] TextMeshProUGUI dialog_text;
    [SerializeField] int letters_per_second;


    /*public event Action OnShowDialog;
    public event Action OnHideDialog;   

    public static DialogManager Instance { get; private set; }*/

    /*private void Awake()
    {
        Instance = this;
    }*/
    bool guard = false;
    public void ShowDialog(Dialog dialog)
    {
        dialog_box.SetActive(true);
        float f = dialog.get_line.Length / letters_per_second * 1.1f;
        StartCoroutine(TypeDialog(dialog.get_line));
        
        //dialog_box.SetActive(false);
    }

    private void HandleUpdate()
    {
        //if (input)
    }

    public IEnumerator TypeDialog(string line)
    {
        if (guard)
        { }
        else
        {
            guard = true;
            dialog_text.text = "";
            foreach (var letter in line.ToCharArray())
            {
                dialog_text.text += letter;
                yield return new WaitForSeconds(1f / letters_per_second);
            }

            yield return new WaitForSeconds(2f);
            dialog_box.SetActive(false);
            guard = false;
        }
        
    }

    

}
