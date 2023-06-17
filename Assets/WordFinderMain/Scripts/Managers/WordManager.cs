using UnityEngine;
using System.IO;

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
        ChooseLanguage();

        Debug.Log(filePath);

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

    private void ChooseLanguage()
    {
        switch (SceneManagerScript.instance.GetLanguageState())
        {
            case LanguagesState.English:
                filePath = Application.dataPath + "/WordFinderMain/Resources/WordsLibrary/wordsEU.txt";
                break;
            case LanguagesState.Russian:
                filePath = Application.dataPath + "/WordFinderMain/Resources/WordsLibrary/wordsRU.txt";
                break;
            case LanguagesState.Azerbaijani:
                filePath = Application.dataPath + "/WordFinderMain/Resources/WordsLibrary/wordsAZ.txt";
                break;
        }
    }
}
