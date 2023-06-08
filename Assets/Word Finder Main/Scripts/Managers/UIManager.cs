using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;

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
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChanhedCallback;
    }

    private void Start()
    {
        ShowMenuCG();
        HideGameCG();
        HideLevelCompleteCG();
        HideGameOverCG();
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

    private void ShowMenuCG()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }

    private void HideMenuCG()
    {
        HideCG(menuCG);
    }

    private void ShowGameCG()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();

        ShowCG(gameCG);
    }

    private void HideGameCG()
    {
        HideCG(gameCG);
    }

    private void ShowLevelCompleteCG()
    {
        if(DataManager.instance == null)
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
        HideCG(levelCompleteCG);
    }

    private void ShowGameOverCG()
    {
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverSecretWord.text = WordManager.instance.GetSecretWord();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameOverCG);
    }

    private void HideGameOverCG()
    {
        HideCG(gameOverCG);
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
