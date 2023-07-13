using System.Collections;
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
    [SerializeField] private CanvasGroup getCoinsForWatchingAD_CG;
    [SerializeField] private CanvasGroup sceneTransitionCG;

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

    [SerializeField] ButtonsAnimation GetCoinsButton;
    [SerializeField] ButtonsAnimation LetterHintButton;

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
        HideGetCoinsForWatchingAD_CG();

        StartCoroutine(LoadSceneTransition());
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
        LetterHintButton.StartAnimation();
    }

    private void HideGameCG()
    {
        if (gameCG == null)
            return;

        HideCG(gameCG);
        LetterHintButton.StopAnimation();
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

        HapticsManager.Vibrate();


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

    public void ShowGetCoinsForWatchingAD_CG()
    {
        if (getCoinsForWatchingAD_CG == null)
            return;

        ShowCG(getCoinsForWatchingAD_CG);
        GetCoinsButton.StartAnimation();
    }

    public void HideGetCoinsForWatchingAD_CG()
    {
        if (getCoinsForWatchingAD_CG == null)
            return;

        HideCG(getCoinsForWatchingAD_CG);
        GetCoinsButton.StopAnimation();
    }

    private void ShowSceneTransitionCG()
    {
        if (sceneTransitionCG == null)
            return;

        ShowCG(sceneTransitionCG);
    }

    private void HideSceneTransitionCG()
    {
        if (sceneTransitionCG == null)
            return;

        HideCG(sceneTransitionCG);
    }

    IEnumerator LoadSceneTransition()
    {
        ShowSceneTransitionCG();

        yield return new WaitForSeconds(3);

        HideSceneTransitionCG();
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
