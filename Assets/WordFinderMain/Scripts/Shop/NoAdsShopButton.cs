using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoAdsShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float price;
    [SerializeField] private TMP_Text priceText;


    private void Start()
    {
        priceText.text = price.ToString() + " $";

        button.onClick.AddListener(() => Buying(price));
    }

    private void Buying(float price)
    {
        NoAds();
        Debug.Log("Deleted ads for " + price);
    }

    private void NoAds()
    {
        Debug.Log("Delete Ads");
    }
}
