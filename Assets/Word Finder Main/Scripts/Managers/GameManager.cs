using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Action<GameState> onGameStateChanged;

    public GameState gameState;

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
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
