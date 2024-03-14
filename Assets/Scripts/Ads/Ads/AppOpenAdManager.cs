// Copyright 2021 Google LLC
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AppOpenAdManager : MonoBehaviour
{
#if UNITY_ANDROID
    public string AD_UNIT_ID = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5662855259";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

    private static AppOpenAdManager instance;

    private AppOpenAd AOA;

    private bool isShowingAd = false;

    // COMPLETE: Add loadTime field
    private DateTime loadTime;
    private bool m_IsStartupAppOpenAdShown;
    private void Awake()
    {
        MobileAdsEventExecutor.ExecuteInUpdate(LoadAd);

    }
    public static AppOpenAdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }

    private bool IsAdAvailable
    {
        get
        {
            // COMPLETE: Consider ad expiration
            return AOA != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd()
    {
        var request = new AdRequest();

        // Load an app open ad for portrait orientation
        AppOpenAd.Load(AD_UNIT_ID, ScreenOrientation.AutoRotation, request, OnAppOpenRequestResponse);

    }
    public void CheckForStartupAd()
    {
        if (m_IsStartupAppOpenAdShown)
            return;
        ShowAdIfAvailable();
        m_IsStartupAppOpenAdShown = true;
    }
    private void OnAppOpenRequestResponse(AppOpenAd appOpenAd, LoadAdError failedToLoadEventArgs)
    {
        
        if (failedToLoadEventArgs != null)
        {
            //AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAdNotLoaded);
            MobileAdsEventExecutor.ExecuteInUpdate(LoadAd);
            return;
        }

        AOA = appOpenAd;
        AOA.OnAdFullScreenContentClosed += HandleAdDidDismissFullScreenContent;
        //AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAdLoaded);
        MobileAdsEventExecutor.ExecuteInUpdate(CheckForStartupAd);
    }

    public void ShowAdIfAvailable()
    {
        if (isShowingAd || AOA==null)
        {
            LoadAd();
            return;
        }

        AOA.Show();
        Debug.Log("App Open Showing");
        AppOpenEvents();
    }


    private void AppOpenEvents() 
    {
        AOA.OnAdFullScreenContentClosed += HandleAdDidDismissFullScreenContent;
        AOA.OnAdFullScreenContentFailed += HandleAdFailedToPresentFullScreenContent;
        AOA.OnAdFullScreenContentOpened += HandleAdDidPresentFullScreenContent;
        AOA.OnAdImpressionRecorded += HandleAdDidRecordImpression;
        AOA.OnAdPaid += HandlePaidEvent;
    }
    private void HandleAdDidDismissFullScreenContent()
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        AOA = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(AdError Error)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", Error.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        AOA = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent()
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression()
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(AdValue Value)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
            Value.CurrencyCode, Value.Value);
    }
}
