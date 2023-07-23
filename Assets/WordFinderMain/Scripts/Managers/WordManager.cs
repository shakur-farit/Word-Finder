using UnityEngine;
using System.IO;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [SerializeField] private string secretWord;
    [SerializeField] private LanguagesState languagesState = LanguagesState.None;

    private string fileText;

    private bool shouldResetWord;

    public string FileText { get { return fileText; } }

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

        SetSecretWord();
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetSecretWord()
    {
#if UNITY_EDITOR_WIN
        string[] lines = fileText.Split("\r\n");
#elif UNITY_EDITOR_OSX
        string[] lines = fileText.Split("\n");
#endif
        Debug.Log(lines.Length);
        int randomLineIndex = Random.Range(0, lines.Length);
        secretWord = lines[randomLineIndex];

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
        switch (languagesState)
        {
            case LanguagesState.English:
                LoadWordsEU();
                break;
            case LanguagesState.Russian:
                LoadWordsRU();
                break;
            case LanguagesState.Azerbaijani:
                LoadWordsAZ();
                break;
        }
    }

    private void LoadWordsEU()
    {
        TextAsset asset = Resources.Load<TextAsset>("wordsEU");
        fileText = asset.text;
    }

    private void LoadWordsAZ()
    {
        TextAsset asset = Resources.Load<TextAsset>("wordsAZ");
        fileText = asset.text;
    }

    private void LoadWordsRU()
    {
        TextAsset asset = Resources.Load<TextAsset>("wordsRU");
        fileText = asset.text;
    }
}
