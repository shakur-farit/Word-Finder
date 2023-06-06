using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    private int currentWordContainerIndex;
    private bool canAddLetter = true;

    private void OnEnable()
    {
        KeyboardKey.onKeyPressed += KeyPressedCallback;
    }

    private void OnDisable()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
    }

    private void Start()
    {
        Initialize();

        tryButton.interactable = false;
    }

    private void Initialize()
    {
        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
            
        }
    }

    private void KeyPressedCallback(char letter)
    {
        if (!canAddLetter)
            return;

        wordContainers[currentWordContainerIndex].Add(letter);

        if (wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLetter = false;
            EnableTryButton();
        }  
    }

    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if (wordToCheck == secretWord)
        {
            Debug.Log("LevelComplete");
            SetLevelComplete();
        }
        else
        {
            Debug.Log("WrongWord");

            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
        }
    }

    private void SetLevelComplete()
    {
        if (GameManager.instance == null)
        {
            Debug.LogWarning("There is no object with GameManager component!");
            return;
        }
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    public void BackspacePressedCallback()
    {
        bool isRemoveLetter = wordContainers[currentWordContainerIndex].RemoveLetter();

        if (isRemoveLetter)
            DisableTryButton();

        canAddLetter = true;
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }
}
