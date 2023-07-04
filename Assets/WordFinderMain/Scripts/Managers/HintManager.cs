using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HintManager : MonoBehaviour
{
    [SerializeField] private GameObject keyboard;
    [SerializeField] private bool hasLimitHint = false;
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
        {
            UIManager.instance.ShowGetCoinsForWatchingAD_CG();
            return;
        }

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
        {
            UIManager.instance.ShowNothingToHintCG();
            return;
        }

        for (int i = 0; i < keyBlockingAmount; i++)
        {
            int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
            t_untouchedKeys[randomKeyIndex].SetInvalid();
        }

        DataManager.instance.RemoveCoins(keyboardHintPrice);
    }

    List<int> letterHintGivenIndices = new List<int>();
    int indexOfUnfoundedLetter = -1;
    public void LetterHint()
    {
        if (DataManager.instance.GetCoins() < letterHintPrice)
        {
            UIManager.instance.ShowGetCoinsForWatchingAD_CG();
            return;
        }

        if (hasLimitHint && letterHintGivenIndices.Count >= letterHintAmount)
        {
            Debug.Log("All hints have been used");
            return;
        }

        string secretWord = WordManager.instance.GetSecretWord();
        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();

        Debug.Log(InputManager.instance.foundLetter.Count);

        if (InputManager.instance.foundLetter.Count <= 0)
        {
            var letterHintNoGivenIndices = Enumerable.Range(0, secretWord.Length)
                .Where(i => !letterHintGivenIndices.Contains(i))
                .ToList();

            Debug.Log(letterHintNoGivenIndices.Count);

            if (letterHintNoGivenIndices.Count <= 0)
            {
                UIManager.instance.ShowNothingToHintCG();
                Debug.Log("Nothing to hint");
                return;
            }

            int randomLetterIndex = letterHintNoGivenIndices[Random.Range(0, letterHintNoGivenIndices.Count)];
            letterHintGivenIndices.Add(randomLetterIndex);

            currentWordContainer.AddAsHint(randomLetterIndex, secretWord[randomLetterIndex]);
        }
        else
        {
            var unfoundedLetterIndices = Enumerable.Range(indexOfUnfoundedLetter + 1, secretWord.Length - indexOfUnfoundedLetter - 1)
                .Where(i => !InputManager.instance.foundLetter.Contains(secretWord[i]))
                .ToList();

            if (unfoundedLetterIndices.Count <= 0)
            {
                Debug.Log("Nothing to hint");
                UIManager.instance.ShowNothingToHintCG();
                return;
            }

            int unfoundedLetterIndex = unfoundedLetterIndices.First();
            indexOfUnfoundedLetter = unfoundedLetterIndex;
            Debug.Log("Unfounded letter is " + secretWord[indexOfUnfoundedLetter] + " in " + (indexOfUnfoundedLetter + 1));
            currentWordContainer.AddAsHint(unfoundedLetterIndex, secretWord[unfoundedLetterIndex]);
        }

        DataManager.instance.RemoveCoins(letterHintPrice);
    }
}
