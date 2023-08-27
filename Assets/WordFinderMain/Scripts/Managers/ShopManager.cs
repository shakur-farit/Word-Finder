using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
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
        AdsManager.instance.adsRemoved = true;
        Debug.Log("Remove ads");
    }

    private void BuyCoins(int amount)
    {
        DataManager.instance.AddCoins(amount);
        Debug.Log($"Add {amount} coins");
    }
}
