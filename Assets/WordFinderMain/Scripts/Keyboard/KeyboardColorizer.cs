using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    private KeyboardKey[] keys;

    private bool shouldResetKeyboard;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChanhedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChanhedCallback;
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if(shouldResetKeyboard)
                    Initialize();
                break;

            case GameState.LevelComplete:
                shouldResetKeyboard = true;
                break;

            case GameState.GameOver:
                shouldResetKeyboard = true;
                break;
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Initialize();
        }

        shouldResetKeyboard = false;
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();

            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                    continue;

                if (keyLetter == secretWord[j])
                {
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    keys[i].SetPotantial();
                }
                else
                {
                    keys[i].SetInvalid();
                }
            }
        }
    }
}
