using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [SerializeField] private string secretWord;

    private string filePath;

    private bool shouldResetWord;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChanhedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChanhedCallback;
    }

    private void Start()
    {
        filePath = Application.dataPath + "/Word Finder Main/Resources/WordsLibrary/words.txt";

        SetSecretWord();
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetSecretWord()
    {
        string[] lines = File.ReadAllLines(filePath);
        Debug.Log(lines.Length);
        int randomLineIndex = Random.Range(0, lines.Length);
        secretWord = lines[randomLineIndex].ToUpper();

        shouldResetWord = false;
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if (shouldResetWord)
                    SetSecretWord();
                break;

            case GameState.LevelComplete:
                shouldResetWord = true;
                break;

            case GameState.GameOver:
                shouldResetWord = true;
                break;
        }
    }
}
