using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private Button notTheWordButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [SerializeField] private int scoreForWin = 10;
    [SerializeField] private int coinsForWin = 30;

    private int currentWordContainerIndex;

    private bool notTheWord;

    private bool canAddLetter = true;
    private bool shouldResetInput;

    public static Action onLetterAdded;
    public static Action onLetterRemoved;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        KeyboardKey.onKeyPressed += KeyPressedCallback;
        GameManager.onGameStateChanged += GameStateChanhedCallback;
    }

    private void OnDisable()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChanhedCallback;
    }

    private void Start()
    {
        Initialize();

        tryButton.interactable = false;
        notTheWordButton.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;

        DisableButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();           
        }

        shouldResetInput = false;
    }

    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }

    private void KeyPressedCallback(char letter)
    {
        if (!canAddLetter)
            return;

        wordContainers[currentWordContainerIndex].Add(letter);

        HapticsManager.Vibrate();

        if (wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLetter = false;
            notTheWord = IsThereWord();
            EnableButton();
        }

        onLetterAdded?.Invoke();
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if(shouldResetInput)
                    Initialize();
                break;

            case GameState.LevelComplete:
                shouldResetInput = true;
                break;

            case GameState.GameOver:
                shouldResetInput = true;
                break;
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
            currentWordContainerIndex++;
            DisableButton();

            if (currentWordContainerIndex >= wordContainers.Length)
            {
                Debug.Log("GameOver");
                DataManager.instance.ResetScore();
                GameManager.instance.SetGameState(GameState.GameOver);
            }
            else
                canAddLetter = true;
            
            
        }
    }

    private bool IsThereWord()
    {
#if UNITY_EDITOR_WIN
        string[] lines = WordManager.instance.FileText.Split("\r\n");
#elif UNITY_EDITOR_OSX
        string[] lines = WordManager.instance.FileText.Split("\n");
#endif
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();

        for(int i = 0; i< lines.Length; i++)
        {
            string wordFromFile = lines[i].ToUpper();

            if (wordToCheck == wordFromFile)
            {   
                Debug.Log(wordToCheck + " - " + lines[i]);
                return false;     
            }
        }

        return true;
    }

    public List<char> foundLetter = new List<char>();
    public void SetFoundLetter(char letter)
    {
        foundLetter.Add(letter);
    }

    private void SetLevelComplete()
    {
        UpdateData();

        if (GameManager.instance == null)
        {
            Debug.LogWarning("There is no object with GameManager component!");
            return;
        }
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    public void ClearFoundLetterList()
    {
        foundLetter.Clear();
    }

    private void UpdateData()
    {
        int scoreToAdd = scoreForWin - currentWordContainerIndex;
        int coinsToAdd = coinsForWin - (currentWordContainerIndex*10);

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoins(coinsToAdd);
    }

    public void BackspacePressedCallback()
    {
        if (!GameManager.instance.IsGameState())
            return;

        bool isRemoveLetter = wordContainers[currentWordContainerIndex].RemoveLetter();

        if (isRemoveLetter)
            DisableButton();

        HapticsManager.Vibrate();

        canAddLetter = true;

        onLetterRemoved?.Invoke();
    }

    private void EnableButton()
    {
        if (notTheWord)
            notTheWordButton.gameObject.SetActive(true);
        else
            tryButton.interactable = true;
    }

    private void DisableButton()
    {
        notTheWordButton.gameObject.SetActive(false);
        tryButton.interactable = false;
    }
}
