using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Action<GameState> onGameStateChanged;

    private GameState gameState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        gameState = GameState.Game;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    public void NextButtonCallback()
    {   
        SetGameState(GameState.Game);
        InputManager.instance.ClearFoundLetterList();
        //AdsManager.instance.interstitialAd.ShowAd();
    }

    //public void BackButtonCallback()
    //{
    //    SetGameState(GameState.Menu);
    //}

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }

    public void ShowRewardAd()
    {
        AdsManager.instance.rewardAd.ShowRewardedAd();
        UIManager.instance.HideGetCoinsForWatchingAD_CG();
    }
}
