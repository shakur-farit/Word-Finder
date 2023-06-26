using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup settingsCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;
    [SerializeField] private CanvasGroup helpCG;
    [SerializeField] private CanvasGroup nothingToHintCG;

    [SerializeField] private TextMeshProUGUI menuBestScore;
    [SerializeField] private TextMeshProUGUI menuCoins;

    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI gameCoins;

    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;

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
        DataManager.onCoinsUpdate += UpdateCoinsText;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChanhedCallback;
        DataManager.onCoinsUpdate -= UpdateCoinsText;
    }

    private void Start()
    {
        //ShowMenuCG();
        HideSettingsCG();
        ShowGameCG();
        HideLevelCompleteCG();
        HideGameOverCG();
        HideHelpCG();
        HideNothingToHintCG();
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                ShowMenuCG();
                HideGameCG();
                break;

            case GameState.Game:
                ShowGameCG();
                HideMenuCG();
                HideLevelCompleteCG();
                HideGameOverCG();
                break;

            case GameState.LevelComplete:
                ShowLevelCompleteCG();
                HideGameCG();
                break;

            case GameState.GameOver:
                ShowGameOverCG();
                HideGameCG();
                break;
        }
    }

    private void UpdateCoinsText()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameOverCoins.text = menuCoins.text;
    }

    private void ShowMenuCG()
    {
        if (menuCG == null || DataManager.instance == null)
            return;

        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }

    private void HideMenuCG()
    {
        if (menuCG == null)
            return;

        HideCG(menuCG);
    }

    private void ShowGameCG()
    {
        if (gameCG == null)
            return;

        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();

        ShowCG(gameCG);
    }

    private void HideGameCG()
    {
        if (gameCG == null)
            return;

        HideCG(gameCG);
    }

    private void ShowLevelCompleteCG()
    {
        if (levelCompleteCG == null)
            return;

        if (DataManager.instance == null)
        {
            Debug.LogWarning("There is no object with DataManager component.");
            return;
        }

        if (WordManager.instance == null)
        {
            Debug.LogWarning("There is no object with WordManager component.");
            return;
        }

        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(levelCompleteCG);
    }

    private void HideLevelCompleteCG()
    {
        if(levelCompleteCG == null)
            return;

        HideCG(levelCompleteCG);
    }

    private void ShowGameOverCG()
    {
        if(gameOverCG == null)
            return;

        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverSecretWord.text = WordManager.instance.GetSecretWord();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameOverCG);
    }

    private void HideGameOverCG()
    {
        if(gameOverCG == null)
            return;

        HideCG(gameOverCG);
    }

    public void ShowSettingsCG()
    {
        if(settingsCG == null)
            return;

        ShowCG(settingsCG);
    }

    public void HideSettingsCG()
    {
        if(settingsCG == null)
            return;

        HideCG(settingsCG);
    }

    public void ShowHelpCG()
    {
        if (helpCG == null)
            return;

        ShowCG(helpCG);
    }

    public void HideHelpCG()
    {
        if (helpCG == null)
            return;

        HideCG(helpCG);
    }

    public void ShowNothingToHintCG()
    {
        if (nothingToHintCG == null)
            return;

        ShowCG(nothingToHintCG);
    }

    public void HideNothingToHintCG()
    {
        if (nothingToHintCG == null)
            return;

        HideCG(nothingToHintCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
