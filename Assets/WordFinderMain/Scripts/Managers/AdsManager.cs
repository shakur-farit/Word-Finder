using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    public InterstitialAdScript interstitialAd;
    public RewardAdScript rewardAd;

    public bool adsRemoved = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        AdsRemovedCheck();

        MobileAds.Initialize((InitializationStatus initStatus) =>
        { 
            interstitialAd.LoadInterstitialAd();
            rewardAd.LoadRewardedAd();
        });
    }
    private void AdsRemovedCheck()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
            adsRemoved = true;
    }
}
