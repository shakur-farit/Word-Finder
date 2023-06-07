using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [SerializeField] private string secretWord;
    [SerializeField] private TextAsset wordsText;

    private string words;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        words = wordsText.text;

        SetSecretWord();
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetSecretWord()
    {
        Debug.Log("String length : " + words.Length);
        // There is 2 hidden character in every line in txt file (backspace and enter) except first and last lines.
        // In game we use words with 5 letters thus in txt file it turns out 7 characters (5 in word and 2 hidden).
        // Find count of words in our txt file.
        int wordCount = (words.Length + 2) / 7;

        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 7;

        secretWord = words.Substring(wordStartIndex, 5).ToUpper();
    }
}
