using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameAnalyticsSDK;

public class AdmobGA_Helper : MonoBehaviour
{
    public static void GA_Log(AdmobGAEvents log)
    {
        switch (log)
        {
            //Initalizing
            case AdmobGAEvents.Initializing:
                LogGAEvent("Admob:Initializing");
                break;
            case AdmobGAEvents.Initialized:
                LogGAEvent("Admob:Initialized");
                break;

            //Request
            case AdmobGAEvents.LoadInterstitialAd:
                LogGAEvent("Admob:iAd:Request");
                break;
            case AdmobGAEvents.LoadOnLoadingInterstitialAd:
                LogGAEvent("Admob:iAdOnLoading:Request");
                break;
            case AdmobGAEvents.LoadVideoAd:
                LogGAEvent("Admob:vAd:Request");
                break;
            case AdmobGAEvents.LoadRewardedAd:
                LogGAEvent("Admob:rAd:Request");
                break;
            case AdmobGAEvents.RequestBannerAd:
                LogGAEvent("Admob:BAd:Request");
                break;
            case AdmobGAEvents.RequestAppOpenAd:
                LogGAEvent("Admob:AppOpen:Request");
                break;


            //LOADED
            case AdmobGAEvents.InterstitialAdLoaded:
                LogGAEvent("Admob:iAd:Loaded");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdLoaded:
                LogGAEvent("Admob:iAdOnLoading:Loaded");
                break;
            case AdmobGAEvents.VideoAdLoaded:
                LogGAEvent("Admob:vAd:Loaded");
                break;
            case AdmobGAEvents.RewardedAdLoaded:
                LogGAEvent("Admob:rAd:Loaded");
                break;
            case AdmobGAEvents.AppOpenAdLoaded:
                LogGAEvent("Admob:AppOpen:Loaded");
                break;
         
            //Show Call
            case AdmobGAEvents.ShowInterstitialAd:
                LogGAEvent("Admob:iAd:ShowCall");
                break;
            case AdmobGAEvents.ShowOnLoadingInterstitialAd:
                LogGAEvent("Admob:iAdOnLoading:ShowCall");
                break;
            case AdmobGAEvents.ShowVideoAd:
                LogGAEvent("Admob:vAd:ShowCall");
                break;
            case AdmobGAEvents.ShowRewardedAd:
                LogGAEvent("Admob:rAd:ShowCall");
                break;
            case AdmobGAEvents.BannerAdStarted:
                LogGAEvent("Admob:BAd:ShowCall");
                break;
            case AdmobGAEvents.ShowAppOpenAd:
                LogGAEvent("Admob:AppOpen:Show");
                break;


            //Will Display
            case AdmobGAEvents.InterstitialAdWillDisplay:
                LogGAEvent("Admob:iAd:WillDisplay");
                break;
            case AdmobGAEvents.VideoAdWillDisplay:
                LogGAEvent("Admob:vAd:WillDisplay");
                break;
            case AdmobGAEvents.RewardedAdWillDisplay:
                LogGAEvent("Admob:rAd:WillDisplay");
                break;
            case AdmobGAEvents.BannerAdWillDisplay:
                LogGAEvent("Admob:BAd:WillDisplay");
                break;

            //Displayed
            case AdmobGAEvents.InterstitialAdDisplayed:
                LogGAEvent("Admob:iAd:Displayed");
                break;
            case AdmobGAEvents.VideoAdDisplayed:
                LogGAEvent("Admob:vAd:Displayed");
                break;
            case AdmobGAEvents.RewardedAdDisplayed:
                LogGAEvent("Admob:rAd:Displayed");
                break;
            case AdmobGAEvents.BannerAdDisplayed:
                LogGAEvent("Admob:BAd:Displayed");
                break;

            //Rewarded Ad Started
            case AdmobGAEvents.RewardedAdStarted:
                LogGAEvent("Admob:rAd:Started");
                break;

            //Rewarded Ad Give Reward
            case AdmobGAEvents.RewardedAdReward:
                LogGAEvent("Admob:rAd:Reward");
                break;

            //No Inventory
            case AdmobGAEvents.RewardedAdNoInventory:
                LogGAEvent("Admob:rAd:NoInventory");
                break;
            case AdmobGAEvents.InterstitialAdNoInventory:
                LogGAEvent("Admob:iAd:NoInventory");
                break;
            case AdmobGAEvents.VideoAdNoInventory:
                LogGAEvent("Admob:vAd:NoInventory");
                break;
            case AdmobGAEvents.AppOpenAdNotLoaded:
                LogGAEvent("Admob:AppOpen:NoInventory");
                break;
           
           //Ad Close
            case AdmobGAEvents.InterstitialAdClosed:
                LogGAEvent("Admob:iAd:Closed");
                break;
            case AdmobGAEvents.VideoAdClosed:
                LogGAEvent("Admob:vAd:Closed");
                break;
            case AdmobGAEvents.RewardedAdClosed:
                LogGAEvent("Admob:rAd:Closed");
                break;
            case AdmobGAEvents.BannerAdRemoved:
                LogGAEvent("Admob:BAd:Removed");
                break;

            //Ad Clicked
            case AdmobGAEvents.InterstitialAdClicked:
                LogGAEvent("Admob:iAd:Clicked");
                break;
            case AdmobGAEvents.VideoAdClicked:
                LogGAEvent("Admob:vAd:Clicked");
                break;
            case AdmobGAEvents.RewardedAdClicked:
                LogGAEvent("Admob:rAd:Clicked");
                break;
            case AdmobGAEvents.BannerAdClicked:
                LogGAEvent("Admob:BAd:Clicked");
                break;

            //Adapters Register
            case AdmobGAEvents.AdaptersInitialized:
                LogGAEvent("Admob:Adapters:Initialized");
                break;

            //Adapters Not Register
            case AdmobGAEvents.AdaptersNotInitialized:
                LogGAEvent("Admob:Adapters:NotInitialized");
                break;


            case AdmobGAEvents.InterstitialAdFailToLoad:
                LogGAEvent("Admob:BAd:Displayed");
                break;
            case AdmobGAEvents.InterstitialAdFailToShow:
                LogGAEvent("Admob:BAd:Displayed");
                break;

        }
    }

    public static void LogGAEvent(string log)
    {
        Logger.Log("event Is:: " + log);
       // GameAnalytics.NewDesignEvent(log);
    }
}
