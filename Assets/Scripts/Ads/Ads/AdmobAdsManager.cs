using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

using GoogleMobileAds.Common;
using PopupUtility;

public enum AdStatus
{
    NotLoading,
    Loading,
    Loaded,
    Shown
}

public enum BannerPosition
{
    Top = 0,
    Bottom = 1,
    TopLeft = 2,
    TopRight = 3,
    BottomLeft = 4,
    BottomRight = 5,
    Center = 6
}

[Serializable]
public class AdIDsTemplate
{

    public string appID;
    //public string interstitialID;
    public string rewardedAdID;

    public string appOpenID;
    //public string bannerAdID_A;
    //public string BannerAdID_B;
}

[System.Serializable]
public class AdIds
{
    [SerializeField]
    private AdIDsTemplate androidAdIDs;

    [SerializeField]
    private AdIDsTemplate iosAdIDs;

    public string AppID
    {
        get
        {
            string appIDToReturn;
#if UNITY_ANDROID
            appIDToReturn =  this.androidAdIDs.appID;
#else
            appIDToReturn =  this.iosAdIDs.appID;
#endif
            return appIDToReturn.Trim();
        }
    }

//    public string InterstitialID
//    {
//        get
//        {
//            string interstitialIDToReturn;
//#if UNITY_ANDROID
//            interstitialIDToReturn = this.androidAdIDs.interstitialID;
//#else
//            interstitialIDToReturn =  this.iosAdIDs.interstitialID;
//#endif
//            return interstitialIDToReturn.Trim();
//        }
//    }

    public string AppOpenAdID
    {
        get
        {
            string rewardedAdIDToReturn;
#if UNITY_ANDROID
            rewardedAdIDToReturn = this.androidAdIDs.appOpenID;
#else
            rewardedAdIDToReturn =  this.iosAdIDs.appOpenID;
#endif
            return rewardedAdIDToReturn.Trim();
        }
    }

    public string RewardedAdID
    {
        get
        {
            string rewardedAdIDToReturn;
#if UNITY_ANDROID
            rewardedAdIDToReturn = this.androidAdIDs.rewardedAdID;
#else
            rewardedAdIDToReturn =  this.iosAdIDs.rewardedAdID;
#endif
            return rewardedAdIDToReturn.Trim();
        }
    }

//    public string BannerAdID_A
//    {
//        get
//        {
//            string bannerAdIDToReturn;
//#if UNITY_ANDROID
//            bannerAdIDToReturn = this.androidAdIDs.bannerAdID_A;
//#else
//            bannerAdIDToReturn =  this.iosAdIDs.bannerAdID;
//#endif
//            return bannerAdIDToReturn.Trim();
//        }
//    }
//    public string BannerAdID_B
//    {
//        get
//        {
//            string bannerAdIDToReturn;
//#if UNITY_ANDROID
//            bannerAdIDToReturn = this.androidAdIDs.BannerAdID_B;
//#else
//            bannerAdIDToReturn =  this.iosAdIDs.bannerAdID;
//#endif
//            return bannerAdIDToReturn.Trim();
//        }
//    }

}


public class AdmobAdsManager : MonoSingleton<AdmobAdsManager>
{
    [SerializeField]
    private AdIds adIDs;

    public bool isPluginInitialized = false;

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private BannerView bannerAd_A;
    private BannerView bannerAd_B;
    private AppOpenAd m_AppOpenAd;

    public Action rewardedDelegate;
    public Action actionOnAdShown;
    public Action actionOnAdNotShown;

    public Action OnBannerLoaded;

    private bool isInterstitialRequestGiven = false;
    private AdStatus bannerAdStatus_A = AdStatus.NotLoading;
    private AdStatus bannerAdStatus_B = AdStatus.NotLoading;
    
    private bool isRewardedRequestGiven = false;
    private bool isAdmobInitialized = false;
    private bool isAppOpenAdRequest = false;
    private bool m_CanShowAppOpenAd = true;

    private bool isLinkerAd = false;

    private BannerPosition requiredBannerPosition_A,requiredBannerPosition_B;

    public float AdTime = 40;
    public bool ItsShowTime = true;

    public bool UseBannerAdInGame = false;
    
    private readonly WaitForEndOfFrame m_WaitForFrame = new();
    private Coroutine m_RewardedRoutine;
    
    void Start()
    {
        DontDestroyOnLoad(this);

        this.InitializePlugin();
        MobileAds.SetiOSAppPauseOnBackground(true);

    }
  

    private void Update()
    {
        if (AdTime > 0)
            AdTime -= Time.deltaTime;

        if (AdTime <= 0 & ItsShowTime == false)
            ItsShowTime = true;
    }

    public void InitializePlugin()
    {
        Debug.Log("Super Delivery");
        MobileAds.Initialize(this.OnAdmobInit);
    }

    void OnAdmobInit(InitializationStatus initializationStatus)
    {
        this.isAdmobInitialized = true;
        MobileAdsEventExecutor.ExecuteInUpdate(RequestRewardedVideo);
        MobileAdsEventExecutor.ExecuteInUpdate(RequestAppOpenAd);
        AdmobCalling._instance.RequestInterstitialH();
        AdmobCalling._instance.RequestBottomBanner_High();
    }

    public bool IsRewardedAdReady => this.rewardedAd != null && this.rewardedAd.IsLoaded();

    public bool IsInterstitialAdReady => this.interstitialAd != null && this.interstitialAd.IsLoaded();
    public bool IsBannerAdReady_A => this.bannerAd_A != null;
    public bool IsBannerAdReady_B => this.bannerAd_B != null;
    //   public  bool IsSecondaryRewardedAdReady => this.rewardedAdB != null && this.rewardedAdB.IsLoaded();


    //private void RequestBannerInternal()
    //{
    //    RequestBannerAd_A(BannerPosition.Center);
    //    //RequestBannerAd_B(BannerPosition.TopLeft);
    //}


    //private void RequestBanner_A(AdPosition adPosition)
    //{
    //    this.bannerAd_A = new BannerView(adIDs.BannerAdID_A, AdSize.Banner, adPosition);
    //}
    //private void RequestBanner_B(AdPosition adPosition)
    //{
    //    this.bannerAd_B = new BannerView(adIDs.BannerAdID_B, AdSize.Banner, adPosition);
    //}


    //public void RequestBannerAd_A(BannerPosition bannerPosition = BannerPosition.Top)
    //{
    //    this.requiredBannerPosition_A = bannerPosition;

    //    if (this.bannerAd_A != null)
    //    {
    //        this.bannerAd_A.Destroy();
    //    }

    //    this.bannerAd_A = null;
    //    this.bannerAdStatus_A = AdStatus.NotLoading;

    //    AdPosition adPosition = (AdPosition)((int)bannerPosition);

    //    RequestBanner_A(adPosition);


    //    AdRequest bannerRequest = new AdRequest.Builder().Build();

    //    this.bannerAd_A.LoadAd(bannerRequest);

    //    this.bannerAd_A.OnAdLoaded += this.HandleOnBanner_AdLoaded_A;
    //    this.bannerAd_A.OnAdFailedToLoad += this.HandleOnBanner_FailedToLoad_A;
    //    this.bannerAd_A.OnAdClosed += this.HandleOnBanner_AdClosed_A;
    //    this.bannerAd_A.OnAdClosed += this.HandleOnBanner_AdClosed_A;
    //    this.bannerAd_A.OnAdOpening += this.HandleOnBanner_AdOpened_A;

    //    this.bannerAdStatus_A = AdStatus.Loading;
    //}

    //public void RequestBannerAd_B(BannerPosition bannerPosition = BannerPosition.TopLeft)
    //{
    //    this.requiredBannerPosition_B = bannerPosition;

    //    if (this.bannerAd_B != null)
    //    {
    //        this.bannerAd_B.Destroy();
    //    }

    //    this.bannerAd_B = null;
    //    this.bannerAdStatus_B = AdStatus.NotLoading;

    //    AdPosition adPosition = (AdPosition)((int)bannerPosition);

    //    RequestBanner_B(adPosition);


    //    AdRequest bannerRequest = new AdRequest.Builder().Build();

    //    this.bannerAd_A.LoadAd(bannerRequest);

    //    this.bannerAd_B.OnAdLoaded += this.HandleOnBanner_AdLoaded_B;
    //    this.bannerAd_B.OnAdFailedToLoad += this.HandleOnBanner_FailedToLoad_B;
    //    this.bannerAd_B.OnAdClosed += this.HandleOnBanner_AdClosed_B;
    //    this.bannerAd_B.OnAdClosed += this.HandleOnBanner_AdClosed_B;
    //    this.bannerAd_B.OnAdOpening += this.HandleOnBanner_AdOpened_B;

    //    this.bannerAdStatus_B = AdStatus.Loading;
    //}

    #region Banner_A_Subscription
    //public void HandleOnBanner_AdLoaded_A(object sender, EventArgs args)
    //{
    //    if (this.OnBannerLoaded != null)
    //        this.OnBannerLoaded();

    //    this.bannerAdStatus_A = AdStatus.Loaded;
    //}

    //public void CloseBannerAdd_A()
    //{
    //    if (this.bannerAd_A != null)
    //    {
    //        this.bannerAd_A.Destroy();
    //    }

    //    this.bannerAd_A = null;
    //    this.bannerAdStatus_A = AdStatus.NotLoading;
    //    //this.RequestBannerAd(false);
    //}

    //public void ShowBannerAdd_A(Action onBannerShowAction)
    //{
    //    UseBannerAdInGame = true;

    //    if (this.IsBannerAdReady_A && this.bannerAdStatus_A != AdStatus.Shown)
    //    {
    //        this.bannerAd_A.Show();
    //        this.bannerAdStatus_A = AdStatus.Shown;

    //        if (onBannerShowAction != null)
    //            onBannerShowAction();

    //        //AdmobGA_Helper.GA_Log(AdmobGAEvents.BannerAdDisplayed);
    //    }
    //    else
    //    {
    //        MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //        {
    //            this.RequestBannerAd_A(this.requiredBannerPosition_A);
    //        });
    //    }
    //}

    //public void HandleOnBanner_FailedToLoad_A(object sender, AdFailedToLoadEventArgs args)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.BannerAdFailedToLoad);
    //    this.bannerAdStatus_A = AdStatus.NotLoading;
    //}

    //public void HandleOnBanner_AdOpened_A(object sender, EventArgs args)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.BannerAdDisplayed);
    //    this.bannerAdStatus_A = AdStatus.Shown;
    //}

    //public void HandleOnBanner_AdClosed_A(object sender, EventArgs args)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.BannerAdRemoved);
    //    this.bannerAdStatus_A = AdStatus.NotLoading;
    //}
    #endregion

    //#region Banner_B_Subscription
    //public void HandleOnBanner_AdLoaded_B(object sender, EventArgs args)
    //{
    //    if (this.OnBannerLoaded != null)
    //        this.OnBannerLoaded();

    //    this.bannerAdStatus_B = AdStatus.Loaded;
    //}

    //public void CloseBannerAdd_B()
    //{
    //    if (this.bannerAd_B != null)
    //    {
    //        this.bannerAd_B.Destroy();
    //    }

    //    this.bannerAd_B = null;
    //    this.bannerAdStatus_B = AdStatus.NotLoading;
    //    //this.RequestBannerAd(false);
    //}

    //public void ShowBannerAdd_B(Action onBannerShowAction)
    //{
    //    UseBannerAdInGame = true;

    //    if (this.IsBannerAdReady_B && this.bannerAdStatus_B != AdStatus.Shown)
    //    {
    //        this.bannerAd_B.Show();
    //        this.bannerAdStatus_B = AdStatus.Shown;

    //        if (onBannerShowAction != null)
    //            onBannerShowAction();
    //    }
    //    else
    //    {
    //        MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //        {
    //            this.RequestBannerAd_B(this.requiredBannerPosition_B);
    //        });
    //    }
    //}

    //public void HandleOnBanner_FailedToLoad_B(object sender, AdFailedToLoadEventArgs args)
    //{
    //    this.bannerAdStatus_B = AdStatus.NotLoading;
    //}

    //public void HandleOnBanner_AdOpened_B(object sender, EventArgs args)
    //{
    //    this.bannerAdStatus_B = AdStatus.Shown;
    //}

    //public void HandleOnBanner_AdClosed_B(object sender, EventArgs args)
    //{
    //    this.bannerAdStatus_B = AdStatus.NotLoading;
    //}
    //#endregion

    //public void RequestInterstitial()
    //{
    //    Debug.Log("FS>Custom Request Inter Load");
    //    if (interstitialAd is not null)
    //    {
    //        if (interstitialAd.IsLoaded())
    //            return;

    //        interstitialAd.Destroy();
    //    }

    //    interstitialAd = new InterstitialAd(adIDs.InterstitialID);

    //    AdRequest request = new AdRequest.Builder().Build();



    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdLoaded);

    //    this.interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
    //    this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
    //    this.interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;

    //    this.interstitialAd.LoadAd(request);

    //    this.isInterstitialRequestGiven = true;
    //}

    private AdRequest BuildAdRequestObject => new AdRequest.Builder().Build();
    
    public bool IsAppOpenAddLoaded => m_AppOpenAd != null;

    public void RequestAppOpenAd()
    {
        if (!isAdmobInitialized || isAppOpenAdRequest || IsAppOpenAddLoaded)
            return;

        AdRequest request = BuildAdRequestObject;
        AppOpenAd.LoadAd(adIDs.AppOpenAdID, ScreenOrientation.AutoRotation, request, OnAppOpenRequestResponse);
    }

    private void OnAppOpenRequestResponse(AppOpenAd appOpenAd, AdFailedToLoadEventArgs failedToLoadEventArgs)
    {
        isAppOpenAdRequest = false;
        if (failedToLoadEventArgs != null)
        {
            AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAdNotLoaded);
            MobileAdsEventExecutor.ExecuteInUpdate(RequestAppOpenAd);
            return;
        }

        m_AppOpenAd = appOpenAd;
        m_AppOpenAd.OnAdDidDismissFullScreenContent += AppOpenAdOnOnAdClosed;
        AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAdLoaded);
        MobileAdsEventExecutor.ExecuteInUpdate(ShowAppOpenAd);
    }

    private void AppOpenAdOnOnAdClosed(object sender, EventArgs e)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(RequestAppOpenAd);
        m_AppOpenAd = null;
    }
    
    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAddLoaded || !m_CanShowAppOpenAd)
        {
            m_CanShowAppOpenAd = true;
            RequestAppOpenAd();
            return;
        }
        AdmobGA_Helper.GA_Log(AdmobGAEvents.ShowAppOpenAd);
        m_AppOpenAd.Show();
    }

    public void RequestRewardedVideo()
    {
        if (this.IsRewardedAdReady || this.isRewardedRequestGiven || !this.isAdmobInitialized)
            return;

        rewardedAd = new RewardedAd(adIDs.RewardedAdID);

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
        rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow; 
        rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;

        rewardedAd.LoadAd(request);

        isRewardedRequestGiven = true;
        AdmobGA_Helper.GA_Log(AdmobGAEvents.LoadRewardedAd);

    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        if(this.rewardedDelegate!=null)
        {
            AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdReward);
            MobileAdsEventExecutor.ExecuteInUpdate(this.rewardedDelegate);
        }
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdLoaded);
        this.isRewardedRequestGiven = false;
    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdNoInventory);
        this.isRewardedRequestGiven = false;
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdNoInventory);
        this.isRewardedRequestGiven = false;
    }

    private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdClosed);
        this.isRewardedRequestGiven = false;
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
        SetAppOpenAllowed(true);
    }
    
    // Interstitial 
    //private void InterstitialAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdNoInventory);
    //    this.isInterstitialRequestGiven = false;
    //}

    //private void InterstitialAd_OnAdClosed(object sender, EventArgs e)
    //{
    //    if (this.isLinkerAd)
    //    {
    //        if (this.rewardedDelegate != null)
    //        {
    //            MobileAdsEventExecutor.ExecuteInUpdate(this.rewardedDelegate);
    //        }
    //        this.isLinkerAd = false;

    //    }
    //    this.isInterstitialRequestGiven = false;
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdClosed);
    //    if (UseBannerAdInGame)
    //    {
    //        //RequestBannerAd();
    //        Invoke("ShowBannerAdd",0.5f);
    //        Debug.Log("Banner Ad showed");
    //    }
    //    //MobileAdsEventExecutor.ExecuteInUpdate(this.RequestInterstitial);
    //}

    //private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdFailToLoad);
    //    this.isInterstitialRequestGiven = false;
    //}

    //private void InterstitialAd_OnAdLoaded(object sender, EventArgs e)
    //{
    //    AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdLoaded);

    //    this.isInterstitialRequestGiven = false;

    //    //RequestBannerAd_A();
    //}

    public void RequestRewardedInterstitial()
    {
        
    }

    public void SetAppOpenAllowed(bool allow)
    {
        m_CanShowAppOpenAd = allow;
    }

    public void ShowInterstitial()
    {
        //#if UNITY_EDITOR
        //return;
        //#endif
        AdmobGA_Helper.GA_Log(AdmobGAEvents.ShowInterstitialAd);

        AdmobCalling._instance.ShowInterstialAd();
        //if (this.IsInterstitialAdReady && ItsShowTime)
        //{
        //    //Open_CloseBanner();

        //    this.isInterstitialRequestGiven = false;
        //    this.interstitialAd.Show();
        //    ItsShowTime = false;
        //    AdTime = 3;
        //    Invoke("RequestInterstitial", 1);

        //}

        //else
        //{
        //    RequestInterstitial();
        //}
    }



    void Open_CloseBanner()
    {
        if (this.bannerAdStatus_A == AdStatus.Shown)
        {
            Debug.Log("Banner A has been removed");

            //CloseBannerAdd_A();
            
        }
        //if (this.bannerAdStatus_B == AdStatus.Shown)
        //{
        //    Debug.Log("Banner B has been removed");

        //    CloseBannerAdd_B();
        //    RequestBannerAd_B();
        //}
    }

    public void ShowRewardedVideo(Action rewardedDelegate)
    {
//#if UNITY_EDITOR
//        rewardedDelegate();
//        return;
//#endif

        this.rewardedDelegate = rewardedDelegate;

        if (this.rewardedAd.IsLoaded())
        {
            this.isRewardedRequestGiven = false;
            this.rewardedAd.Show();
            AdmobGA_Helper.GA_Log(AdmobGAEvents.ShowRewardedAd);
            m_CanShowAppOpenAd = false;
        }

        else
        {
            // if(this.IsInterstitialAdReady)
            // {
            //     this.isLinkerAd = true;
            //     this.ShowInterstitial();
            //     AdmobGA_Helper.GA_Log(AdmobGAEvents.ShowRewardedAd);
            // }
            MobileAdsEventExecutor.ExecuteInUpdate(RequestRewardedVideo);
            m_RewardedRoutine = StartCoroutine(RewardedMethodRoutine());
            PopupsManager.Instance.ShowWaitingScreen(new WaitingScreenConfig()
            {
                Title = "Loading Rewarded",
                BodyText = "Loading Ad, Please Wait",
                WaitDuration = 3,
                ActionAfterWait = OnRewardedLoadFailed
            });
        }
    }

    private IEnumerator RewardedMethodRoutine()
    {
        while (!IsRewardedAdReady)
        {
            yield return m_WaitForFrame;
        }
        PopupsManager.Instance.HideWaitingScreen();
        ShowRewardedVideo(rewardedDelegate);
    }

    private void OnRewardedLoadFailed()
    {
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
        PopupsManager.Instance.ShowSimpleToastMessage("Please, Try Again");
        StopCoroutine(m_RewardedRoutine);
    }
}
