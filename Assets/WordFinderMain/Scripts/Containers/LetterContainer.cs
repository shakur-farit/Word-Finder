using System;
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

    public void SetLetter(char letter, bool isHint = false)
    {
        if (isHint)
            this.letter.color = Color.gray;
        else
            this.letter.color = Color.black;

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
