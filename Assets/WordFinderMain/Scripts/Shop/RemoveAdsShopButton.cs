using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float price;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] private GameObject activeBox;
    [SerializeField] private TMP_Text soldText;


    private void Start()
    {
        priceText.text = price.ToString() + " $";
        UpdateRemoveAdsShopButton();
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("Remove ads");
        UpdateRemoveAdsShopButton();
    }

    private void UpdateRemoveAdsShopButton()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            button.interactable = false;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        activeBox.SetActive(false);
        soldText.gameObject.SetActive(true);
    }
}
