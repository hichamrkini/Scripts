using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { set; get; }

    private Yodo1U3dBannerAdView bannerAdView;
    private Yodo1U3dNativeAdView nativeAdView;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        Yodo1U3dMasCallback.OnSdkInitializedEvent += (success, error) =>
        {
            Debug.Log("[Yodo1 Mas] OnSdkInitializedEvent, success:" + success + ", error: " + error.ToString());
            if (success)
            {
                Debug.Log("[Yodo1 Mas] The initialization has succeeded");

            //    InitializeInterstitial();
             //   InitializeRewardedAds();

            }
            else
            {
                Debug.Log("[Yodo1 Mas] The initialization has failed");
            }
        }; 

        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig()
            .enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);

       Yodo1U3dMas.InitializeSdk();

    }

    /**************** Interstitials code ****************/

    public void InitializeInterstitial()
    {
        // Instantiate
        Yodo1U3dInterstitialAd.GetInstance();

        // Ad Events
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadedEvent += OnInterstitialAdLoadedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadFailedEvent += OnInterstitialAdLoadFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenedEvent += OnInterstitialAdOpenedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenFailedEvent += OnInterstitialAdOpenFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdClosedEvent += OnInterstitialAdClosedEvent;

        // Load an ad
        LoadInterstitial();
    }

    public void LoadInterstitial()
    {
        Yodo1U3dInterstitialAd.GetInstance().LoadAd();
    }

    public void ShowInterstitial()
    {
        Yodo1U3dMas.ShowInterstitialAd();

        /*
        if (Yodo1U3dInterstitialAd.GetInstance().IsLoaded())
        {
            Yodo1U3dInterstitialAd.GetInstance().ShowAd("Your Placement");
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
        } */
    }

    private void OnInterstitialAdLoadedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdLoadedEvent event received" + ad.GetHashCode());
        
    }

    private void OnInterstitialAdLoadFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdLoadFailedEvent event received with error: " + adError.ToString());
    }

    private void OnInterstitialAdOpenedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdOpenedEvent event received");
    }

    private void OnInterstitialAdOpenFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdOpenFailedEvent event received with error: " + adError.ToString());
        LoadInterstitial();
    }

    private void OnInterstitialAdClosedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdClosedEvent event received");
       LoadInterstitial();
    }

    /**************** Rewarded ads code ****************/

    public void InitializeRewardedAds()
    {
        Yodo1U3dRewardAd.GetInstance();

        // Ad Events
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;

        // Load an ad
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }

    public void ShowRewarded()
    {
        Yodo1U3dMas.ShowRewardedAd();
        /*
        if (Yodo1U3dRewardAd.GetInstance().IsLoaded())
        {
            Yodo1U3dRewardAd.GetInstance().ShowAd("Your Placement");
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
        }*/
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdLoadedEvent event received" + ad.GetHashCode());
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdLoadFailedEvent event received with error: " + adError.ToString());
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdOpenedEvent event received");
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdOpenFailedEvent event received with error: " + adError.ToString());
        LoadRewardedAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdClosedEvent event received");
        LoadRewardedAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdEarnedEvent event received");
    }

    /**************** Banner ads code ****************/

    public void InitializeBanner()
    {

        // Clean up banner before reusing
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
        }

        //Create a banner ad
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerBottom | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);

        // Add Events
        bannerAdView.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        bannerAdView.OnAdFailedToLoadEvent += OnBannerAdFailedToLoadEvent;
        bannerAdView.OnAdOpenedEvent += OnBannerAdOpenedEvent;
        bannerAdView.OnAdFailedToOpenEvent += OnBannerAdFailedToOpenEvent;
        bannerAdView.OnAdClosedEvent += OnBannerAdClosedEvent;

        // Load banner ads, the banner ad will be displayed automatically after loaded
        bannerAdView.LoadAd();

    }

    public void ShowBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Show();
        }
    }

    public void HideBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Hide();
        }
    }

    public void DestroyBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
            bannerAdView = null;
        }
    }

    private void OnBannerAdLoadedEvent(Yodo1U3dBannerAdView adView)
    {
        // Banner ad is ready to be shown.
        Debug.Log("[Yodo1 Mas] OnBannerAdLoadedEvent event received");
    }

    private void OnBannerAdFailedToLoadEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToLoadEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdOpenedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdOpenedEvent event received");
    }

    private void OnBannerAdFailedToOpenEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToOpenEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdClosedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdClosedEvent event received");
    }

    /**************** Native ads code ****************/

    public void InitializeNative()
    {
        // Clean up native before reusing
        if (nativeAdView != null)
        {
            nativeAdView.Destroy();
        }

        // Create a 375x200 native at top of the screen
        nativeAdView = new Yodo1U3dNativeAdView(0, 0, 375, 200);

        // Ad Events
        nativeAdView.OnAdLoadedEvent += OnNativeAdLoadedEvent;
        nativeAdView.OnAdFailedToLoadEvent += OnNativeAdFailedToLoadEvent;

        // Load native ads, the native ad will be displayed automatically after loaded
        nativeAdView.LoadAd();
    }

    public void ShowNative()
    {
        if(nativeAdView != null)
        {
            nativeAdView.Show();
        }
    }

    public void HideNative()
    {
        if (nativeAdView != null)
        {
            nativeAdView.Hide();
        }
    }

    public void DestroyNative()
    {
        if (nativeAdView != null)
        {
            nativeAdView.Destroy();
            nativeAdView = null;
        }
    }

    private void OnNativeAdLoadedEvent(Yodo1U3dNativeAdView adView)
    {
        // Native ad is ready to be shown.
        Debug.Log("[Yodo1 Mas] OnNativeAdLoadedEvent event received");
    }

    private void OnNativeAdFailedToLoadEvent(Yodo1U3dNativeAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnNativeAdFailedToLoadEvent event received with error: " + adError.ToString());
    }
}
