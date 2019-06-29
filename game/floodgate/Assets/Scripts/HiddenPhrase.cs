﻿using System.Collections;
using System.Collections.Generic;
using System.Text; 
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq; 

[RequireComponent(typeof(Text))]
public class HiddenPhrase : MonoBehaviour
{
    public string completeText;

    public Text textComp;
    CharPosition cp; 
    private void Awake()
    {
        cp = GetComponent<CharPosition>(); 
        textComp.text = new string('\u00A0', completeText.Length);
    }

    /// <summary>
    /// Displays a letter in the phrase if available. Case-insensitive. Returns whether or not a letter was uncovered. 
    /// </summary>
    /// <param name="dLtr"></param>
    /// <returns></returns>
    public bool tryDisplayLetter(char dLtr)
    {
        dLtr = Char.ToLower(dLtr); 
        for (int i = 0; i < completeText.Length; i++)
        {
            if(dLtr == Char.ToLower(completeText[i]) && '\u00A0' == textComp.text[i])
            {
                StringBuilder sb = new StringBuilder(textComp.text);
                sb[i] = completeText[i];
                textComp.text = sb.ToString();
                
                return true;  
            }
        }
        return false; 
    }


    private void Update()
    {

        for (int i = 0; i < textComp.text.Length; i++)
        {
            print(cp.GetWorldPosition(i)); 
        }

        if (GameManager.Instance.activePhrase == gameObject && completeText.Replace(" ", "") == textComp.text.Replace("\u00A0", "").Replace(" ", ""))
        {
            Debug.Log("replacing");
            GameManager.Instance.NextPhrase();
        }
        

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                char foo = ' ';
                string inputName = vKey.ToString();
                if (inputName.Length == 1)
                {
                    Char.TryParse(inputName, out foo);
                    tryDisplayLetter(foo);
                }
            }
        }
    }

}
