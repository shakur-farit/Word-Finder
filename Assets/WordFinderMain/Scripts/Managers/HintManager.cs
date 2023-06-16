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
    public void LetterHint()
    {
        if (DataManager.instance.GetCoins() < letterHintPrice)
            return;

        if (letterHintGivenIndices.Count >= letterHintAmount)
        {
            Debug.Log("All hits have gone");
            return;
        }

        var letterHintNoGivenIndices = new List<int>();

        for (int i = 0; i < 5; i++)
            if (!letterHintGivenIndices.Contains(i))
            {
                letterHintNoGivenIndices.Add(i);
                Debug.Log(letterHintNoGivenIndices[i]);
            }

        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();

        string secretWord = WordManager.instance.GetSecretWord();

        int randomLetterIndex = letterHintNoGivenIndices[Random.Range(0, letterHintNoGivenIndices.Count)];
        letterHintGivenIndices.Add(randomLetterIndex);

        currentWordContainer.AddAsHint(randomLetterIndex, secretWord[randomLetterIndex]);

        DataManager.instance.RemoveCoins(letterHintPrice);
    }
}
