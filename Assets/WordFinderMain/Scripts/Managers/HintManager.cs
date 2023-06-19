using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    [SerializeField] private GameObject keyboard;
    [SerializeField] private int letterHintAmount = 5;
    private KeyboardKey[] keys;

    private bool shouldResetHint;

    [SerializeField] private TextMeshProUGUI keyboardPriceText;
    [SerializeField] private TextMeshProUGUI letterPriceText;

    [SerializeField] private int keyboardHintPrice; 
    [SerializeField] private int letterHintPrice;

    [SerializeField] private int keyBlockingAmount = 3;

    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();

        Debug.Log(keys.Length);
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
        keyboardPriceText.text = keyboardHintPrice.ToString();
        letterPriceText.text = letterHintPrice.ToString();
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:

                break;

            case GameState.Game:
                if (shouldResetHint)
                {
                    letterHintGivenIndices.Clear();
                    shouldResetHint = false;
                }
                break;

            case GameState.LevelComplete:
                shouldResetHint = true;
                break;

            case GameState.GameOver:
                shouldResetHint = true;
                break;
        }
    }

    public void KeyboardHint()
    {
        if (DataManager.instance.GetCoins() < keyboardHintPrice)
            return;

        string secretWord = WordManager.instance.GetSecretWord();

        var untouchedKeys = new List<KeyboardKey>();

        for (int i = 0; i < keys.Length; i++)
            if (keys[i].IsUntouched())
                untouchedKeys.Add(keys[i]);

        var t_untouchedKeys = new List<KeyboardKey>(untouchedKeys);

        for (int i = 0; i < untouchedKeys.Count; i++)
            if (secretWord.Contains(untouchedKeys[i].GetLetter()))
                t_untouchedKeys.Remove(untouchedKeys[i]);

        if (t_untouchedKeys.Count <= 0)
            return;

        for (int i = 0; i < keyBlockingAmount; i++)
        {
            int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
            t_untouchedKeys[randomKeyIndex].SetInvalid();
        }

        DataManager.instance.RemoveCoins(keyboardHintPrice);
    }

    List<int> letterHintGivenIndices = new List<int>();
    List<char> hintedLetter = new List<char>();
    public void LetterHint()
    {
        if (DataManager.instance.GetCoins() < letterHintPrice)
            return;

        if (letterHintGivenIndices.Count >= letterHintAmount)
        {
            Debug.Log("All hits have gone");
            return;
        }

        string secretWord = WordManager.instance.GetSecretWord();
        List<int> letterHintNoGivenIndices = new List<int>();
        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();

        if (InputManager.instance.foundLetter.Count <= 0)
        {
            for (int i = 0; i < secretWord.Length; i++)
                if (!letterHintGivenIndices.Contains(i))
                {
                    letterHintNoGivenIndices.Add(i);
                }

            int randomLetterIndex = letterHintNoGivenIndices[Random.Range(0, letterHintNoGivenIndices.Count)];
            letterHintGivenIndices.Add(randomLetterIndex);

            currentWordContainer.AddAsHint(randomLetterIndex, secretWord[randomLetterIndex]);
        }
        else
        {
            //char unfoundedLetter = ' ';
            int indexOfUnfoundedLetter = 0;
            

            for (int i = 0; i < secretWord.Length; i++)
            {
                for (int j = 0; j < InputManager.instance.foundLetter.Count; j++)
                {
                    if (secretWord[i] != InputManager.instance.foundLetter[j] && !hintedLetter.Contains(secretWord[i]))
                    {
                        //unfoundedLetter = secretWord[i];
                        indexOfUnfoundedLetter = i;
                        hintedLetter.Add(secretWord[i]);
                        Debug.Log("Unfounded letter is " + secretWord[indexOfUnfoundedLetter] + " in " + (indexOfUnfoundedLetter + 1));
                    }
                }
            }

            currentWordContainer.AddAsHint(indexOfUnfoundedLetter, secretWord[indexOfUnfoundedLetter]);
        }

        DataManager.instance.RemoveCoins(letterHintPrice);
    }
}
