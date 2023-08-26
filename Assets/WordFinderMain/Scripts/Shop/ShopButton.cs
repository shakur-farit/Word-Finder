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

    public Button GetButton => button;

    private void Start()
    {
        coinsAmountText.text = "+" + coinsAmount.ToString();
        priceText.text = price.ToString() + " $";

        button.onClick.AddListener(() => Buying(coinsAmount,price));
    }

    private void Buying(int coinsAmount, float price)
    {
        IncreaeCoins(coinsAmount);
        Debug.Log("Buy " + coinsAmount + " for " + price);
    }

    private void IncreaeCoins(int amount)
    {
        DataManager.instance.AddCoins(amount);
    }

}
