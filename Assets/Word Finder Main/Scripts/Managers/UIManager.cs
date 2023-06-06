using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;

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
        ShowGameCG();
        HideLevelCompleteCG();  
    }

    private void GameStateChanhedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                ShowLevelCompleteCG();
                HideGameCG();
                break;
        }
    }

    private void ShowGameCG()
    {
        ShowCG(gameCG);
    }

    private void HideGameCG()
    {
        HideCG(gameCG);
    }

    private void ShowLevelCompleteCG()
    {
        ShowCG(levelCompleteCG);
    }

    private void HideLevelCompleteCG()
    {
        HideCG(levelCompleteCG);
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
