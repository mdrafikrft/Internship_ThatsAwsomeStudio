using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.UI;

public class AdmobAdsDisplay : MonoBehaviour
{
    public string appId = "";
    [SerializeField] private string bannerId = "";
    [SerializeField] private string interstitialId = "";
    [SerializeField] private string rewardId = "";

    BannerView bannerAd;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;

    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            print("Ads Initialised!!");
        });

    }

    public void LoadBannerAd()
    {
        createBanner();

        ListenToBannerEvents();

        if (bannerAd == null)
        {
            createBanner();
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        print("Loading banner Ad!");
        bannerAd.LoadAd(adRequest);
    }

    void createBanner()
    {
        if(bannerAd != null)
        {
            DestroyBannerAd();
        }
        bannerAd = new BannerView(bannerId, AdSize.Leaderboard, AdPosition.Top);
    }

    void ListenToBannerEvents()
    {
        bannerAd.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerAd.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerAd.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Banner view paid {0} {1}."+
                adValue.Value+
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        bannerAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerAd.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyBannerAd()
    {
        if(bannerAd != null)
        {
            print("Destroying banner Ad");
            bannerAd.Destroy();
            bannerAd = null;
        }
    }
}
