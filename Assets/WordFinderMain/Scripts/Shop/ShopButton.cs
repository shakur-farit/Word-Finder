using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] private int coinsAmount;
    [SerializeField] protected float price;
    [SerializeField] private TMP_Text coinsAmountText;
    [SerializeField] protected TMP_Text priceText;

    private void Start()
    {
        coinsAmountText.text = "+" + coinsAmount.ToString();
        priceText.text = price.ToString() + " $";
    }

    public void BuyCoins()
    {
        DataManager.instance.AddCoins(coinsAmount);
        Debug.Log($"Add {coinsAmount} coins");
    }
}
