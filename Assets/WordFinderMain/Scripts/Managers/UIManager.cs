using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private CanvasGroup shopCG;

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

    [SerializeField] private TextMeshProUGUI shopCoins;


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
        ShowMenuCG();
        HideSettingsCG();
        ShowGameCG();
        HideLevelCompleteCG();
        HideGameOverCG();
        HideHelpCG();
        HideShopCG();
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
        int currentCoins = DataManager.instance.GetCoins();
        int targetCoins = currentCoins;

        if (!string.IsNullOrEmpty(menuCoins.text))
            int.TryParse(menuCoins.text, out targetCoins);

        if (targetCoins != currentCoins)
        {
            DOTween.To(() => targetCoins, x => targetCoins = x, currentCoins, 1f)
                .OnUpdate(() => {

                    menuCoins.text = targetCoins.ToString();
                    gameCoins.text = targetCoins.ToString();
                    levelCompleteCoins.text = targetCoins.ToString();
                    gameOverCoins.text = targetCoins.ToString();
                    shopCoins.text = targetCoins.ToString();
                })
                .OnComplete(() => {

                    menuCoins.text = currentCoins.ToString();
                    gameCoins.text = currentCoins.ToString();
                    levelCompleteCoins.text = currentCoins.ToString();
                    gameOverCoins.text = currentCoins.ToString();
                    shopCoins.text = currentCoins.ToString();
                });
        }
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

    public void ShowShopCG()
    {
        if (shopCG == null)
            return;

        ShowCG(shopCG);
    }

    public void HideShopCG()
    {
        if (shopCG == null)
            return;

        HideCG(shopCG);
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
