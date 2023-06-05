using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [SerializeField] private TextMeshPro letter;
    [SerializeField] private SpriteRenderer containerRendere;
 
    public void Initialize()
    {
        letter.text = String.Empty;
        containerRendere.color = Color.white;
    }

    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }

    public char GetLetter()
    {
        return letter.text[0];
    }

    public void SetValid()
    {
        containerRendere.color = Color.green;
    }

    public void SetPotantial()
    {
        containerRendere.color = Color.yellow;
    }

    public void SetInvalid()
    {
        containerRendere.color = Color.red;
    }
}
