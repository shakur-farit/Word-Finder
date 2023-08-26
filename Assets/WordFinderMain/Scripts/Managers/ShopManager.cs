using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Button[] shopButtons;

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.MareCo.WordFinder.removeads":
                RemoveAds();
                break;

            case "com.MareCo.WordFinder.500coins":
                BuyCoins(500);
                break;
        }
    }

    private void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("Remove ads");
    }

    private void BuyCoins(int amount)
    {
        DataManager.instance.AddCoins(amount);
        Debug.Log($"Add {amount} coins");
    }
}
