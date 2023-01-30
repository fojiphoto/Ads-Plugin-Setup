
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;


public class AdmobCalling : MonoBehaviour
{
    [Header("Bottom Banner Id")]
    public string BottomBannerId_High;
    public string BottomBannerId_Med;
    public string BottomBannerId_Defualt;

    [SerializeField] AdPosition AdPosition = AdPosition.Top;
    [SerializeField] AdSize Adsize = AdSize.Banner;

    private BannerView bannerViewBottom_High;
    private BannerView bannerViewBottom_Med;
    private BannerView bannerViewBottom_Default;

    private bool bottomBannerEventsAttatched_High = false;
    private bool bottomBannerEventsAttatched_Med = false;
    private bool bottomBannerEventsAttatched_Default = false;

    [Header("Interstitial ID")]
    public string interstitialHigh, interstitialMedium, interstitialLow;

    [SerializeField] float timeToRequest=10f;
    [SerializeField] float NextAdRequstTime = 2f;

    public static InterstitialAd interstitial;
    //public static RewardBasedVideoAd rewardBasedVideo;

    public static AdmobCalling _instance;
    // Use this for initialization

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        _instance = this;
        //MobileAds.Initialize(initStatus =>
        //{
        //    try
        //    {
        //        RequestBottomBanner_High();
        //        Invoke(nameof(RequestInterstitialH), 0f);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Log(ex.ToString());
        //    }
        //});
    }

    #region Bottom Banner High
    public void RequestBottomBanner_High()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerViewBottom_High = new BannerView(BottomBannerId_High, Adsize, AdPosition);


        if (!bottomBannerEventsAttatched_High)
        {
            // Called when an ad request has successfully loaded. 
            this.bannerViewBottom_High.OnAdLoaded += this.HandleOnAdLoadedBottom_High;
            // Called when an ad request failed to load.
            this.bannerViewBottom_High.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBottom_High;
            // Called when an ad is clicked.
            this.bannerViewBottom_High.OnAdOpening += this.HandleOnAdOpenedBottom_High;
            // Called when the user returned from the app after an ad click.
            this.bannerViewBottom_High.OnAdClosed += this.HandleOnAdClosedBottom_High;
            // Called when the ad click caused the user to leave the application.
            //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
            bottomBannerEventsAttatched_High = true;
        }

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerViewBottom_High.LoadAd(request);
    }
    public bool IsBottomAdmobBannerLoaded_High()
    {
        return (this.bannerViewBottom_High != null);
    }
    public void ShowBottomAdmobBanner_High()
    {
        if (this.bannerViewBottom_High != null)
        {
            this.bannerViewBottom_High.Show();
        }
        else
        {
            RequestBottomBanner_High();
        }
    }
    public void HideBottomAdmobBanner_High()
    {
        if (this.bannerViewBottom_High != null)
            this.bannerViewBottom_High.Hide();
    }

    public void HandleOnAdLoadedBottom_High(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoadBottom_High(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
        RequestBottomBanner_Med();
    }

    public void HandleOnAdOpenedBottom_High(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosedBottom_High(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");

        RequestBottomBanner_High();
    }

    public void HandleOnAdLeavingApplicationBottom_High(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    #region Bottom Banner Med
    public void RequestBottomBanner_Med()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerViewBottom_Med = new BannerView(BottomBannerId_Med, Adsize, AdPosition);


        if (!bottomBannerEventsAttatched_Med)
        {
            // Called when an ad request has successfully loaded. 
            this.bannerViewBottom_Med.OnAdLoaded += this.HandleOnAdLoadedBottom_Med;
            // Called when an ad request failed to load.
            this.bannerViewBottom_Med.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBottom_Med;
            // Called when an ad is clicked.
            this.bannerViewBottom_Med.OnAdOpening += this.HandleOnAdOpenedBottom_Med;
            // Called when the user returned from the app after an ad click.
            this.bannerViewBottom_Med.OnAdClosed += this.HandleOnAdClosedBottom_Med;
            // Called when the ad click caused the user to leave the application.
            //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
            bottomBannerEventsAttatched_Med = true;
        }

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerViewBottom_Med.LoadAd(request);
    }
    public bool IsBottomAdmobBannerLoaded_Med()
    {
        return (this.bannerViewBottom_Med != null);
    }
    public void ShowBottomAdmobBanner_Med()
    {
        if (this.bannerViewBottom_Med != null)
        {
            this.bannerViewBottom_Med.Show();
        }
        else
        {
            RequestBottomBanner_Med();
        }
    }
    public void HideBottomAdmobBanner_Med()
    {
        if (this.bannerViewBottom_Med != null)
            this.bannerViewBottom_Med.Hide();
    }

    public void HandleOnAdLoadedBottom_Med(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoadBottom_Med(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
        RequestBottomBanner_Default();
    }

    public void HandleOnAdOpenedBottom_Med(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosedBottom_Med(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestBottomBanner_Med();
    }

    public void HandleOnAdLeavingApplicationBottom_Med(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    #region Bottom Banner Default
    public void RequestBottomBanner_Default()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerViewBottom_Default = new BannerView(BottomBannerId_Defualt, Adsize, AdPosition);


        if (!bottomBannerEventsAttatched_Default)
        {
            // Called when an ad request has successfully loaded. 
            this.bannerViewBottom_Default.OnAdLoaded += this.HandleOnAdLoadedBottom_Default;
            // Called when an ad request failed to load.
            this.bannerViewBottom_Default.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBottom_Default;
            // Called when an ad is clicked.
            this.bannerViewBottom_Default.OnAdOpening += this.HandleOnAdOpenedBottom_Default;
            // Called when the user returned from the app after an ad click.
            this.bannerViewBottom_Default.OnAdClosed += this.HandleOnAdClosedBottom_Default;
            // Called when the ad click caused the user to leave the application.
            //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
            bottomBannerEventsAttatched_Default = true;
        }

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerViewBottom_Default.LoadAd(request);
    }
    public bool IsBottomAdmobBannerLoaded_Default()
    {
        return (this.bannerViewBottom_Default != null);
    }
    public void ShowBottomAdmobBanner_Default()
    {
        if (this.bannerViewBottom_Default != null)
        {
            this.bannerViewBottom_Default.Show();
        }
        else
        {
            RequestBottomBanner_Default();
        }
    }
    public void HideBottomAdmobBanner_Default()
    {
        if (this.bannerViewBottom_Default != null)
            this.bannerViewBottom_Default.Hide();
    }

    public void HandleOnAdLoadedBottom_Default(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoadBottom_Default(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError);
    }

    public void HandleOnAdOpenedBottom_Default(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosedBottom_Default(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestBottomBanner_Default();
    }

    public void HandleOnAdLeavingApplicationBottom_Default(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    #region Interstitial

    
    private InterstitialAd interstitialH, interstitialM, interstitialL;

    public void ShowInterstialAd()
    {
        ShowInterstitialH();
    }

    public void ShowInterstitialH()
    {
        if (interstitialH != null && interstitialH.IsLoaded())
        {
            Debug.Log("Illyas ShowInterstitialH");
            interstitialH.Show();
        }
        else
        {
            Invoke(nameof(RequestInterstitialH), 0f);
            ShowInterstitialM();
        }
    }

    public void RequestInterstitialH()
    {
        if (interstitialH != null)
        {
            if (interstitialH.IsLoaded())
            {
                return;
            }
        }

        Debug.Log("Illyas RequestInterstitialH");
        // Initialize an InterstitialAd.
        this.interstitialH = new InterstitialAd(interstitialHigh);
        // Called when an ad request has successfully loaded.
        this.interstitialH.OnAdLoaded += InterstitialHandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialH.OnAdFailedToLoad += InterstitialHandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialH.OnAdOpening += InterstitialHandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialH.OnAdClosed += InterstitialHandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.interstitial.OnAdLeavingApplication += InterstitialHandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialH.LoadAd(request);

        Invoke(nameof(InterTest), timeToRequest);
    }

    public void ShowInterstitialM()
    {
        if (interstitialM != null && interstitialM.IsLoaded())
        {
            Debug.Log("Illyas ShowInterstitialM");
            interstitialM.Show();
        }
        else
        {
            ShowInterstitialL();
        }
    }

    public void RequestInterstitialM()
    {
        if (interstitialM != null)
        {
            if (interstitialM.IsLoaded())
            {
                return;
            }
        }

        Debug.Log("Illyas RequestInterstitialM");
        // Initialize an InterstitialAd.
        this.interstitialM = new InterstitialAd(interstitialMedium);
        // Called when an ad request has successfully loaded.
        this.interstitialM.OnAdLoaded += InterstitialHandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialM.OnAdFailedToLoad += InterstitialHandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialM.OnAdOpening += InterstitialHandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialM.OnAdClosed += InterstitialHandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.interstitial.OnAdLeavingApplication += InterstitialHandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialM.LoadAd(request);


        Invoke(nameof(InterTest1), timeToRequest);
    }

    public void ShowInterstitialL()
    {
        if (interstitialL != null && interstitialL.IsLoaded())
        {
            Debug.Log("Illyas ShowInterstitialL");
            interstitialL.Show();
        }
    }

    public void RequestInterstitialL()
    {
        if (interstitialL != null)
        {
            if (interstitialL.IsLoaded())
            {
                return;
            }
        }

        Debug.Log("Illyas RequestInterstitialL");
        // Initialize an InterstitialAd.
        this.interstitialL = new InterstitialAd(interstitialLow);
        // Called when an ad request has successfully loaded.
        this.interstitialL.OnAdLoaded += InterstitialHandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialL.OnAdFailedToLoad += InterstitialHandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialL.OnAdOpening += InterstitialHandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialL.OnAdClosed += InterstitialHandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.interstitial.OnAdLeavingApplication += InterstitialHandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialL.LoadAd(request);
    }

    private void InterstitialHandleOnAdClosed(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleAdClosed event received");

        Invoke(nameof(RequestInterstitialH), NextAdRequstTime);
    }

    private void InterstitialHandleOnAdOpened(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    private void InterstitialHandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
    }

    private void InterstitialHandleOnAdLoaded(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }


    void InterTest()
    {
        if (interstitialH != null)
        {
            if (interstitialH.IsLoaded())
            {
                return;
            }
        }
        Invoke(nameof(RequestInterstitialM), NextAdRequstTime);
    }
    void InterTest1()
    {
        if (interstitialM != null)
        {
            if (interstitialM.IsLoaded())
            {
                return;
            }
        }
        Invoke(nameof(RequestInterstitialL), NextAdRequstTime);
    }


    #endregion

}
