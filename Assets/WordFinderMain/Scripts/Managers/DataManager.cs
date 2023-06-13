using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private int coins;
    private int score;
    private int bestScore;

    public static Action onCoinsUpdate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        SaveData();

        onCoinsUpdate?.Invoke();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        coins = Mathf.Max(coins, 0);
        SaveData();

        onCoinsUpdate?.Invoke();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        if(score > bestScore)
            bestScore = score;

        SaveData();
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins", 450);
        score = PlayerPrefs.GetInt("Score");
        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

    public void ResetScore()
    {
        score = 0;
        SaveData();
    }
}
