using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    public InterstitialAdScript interstitialAd;
    public RewardAdScript rewardAd;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            interstitialAd.LoadInterstitialAd();
            rewardAd.LoadRewardedAd();
        });
    }
}
