﻿using System;
using UnityEngine;
using AnyThinkAds.Api;
using AnyThinkAds.Common;

using System.Collections;
using System.Collections.Generic;
#pragma warning disable 0067
namespace AnyThinkAds
{
    public class ATAdsClientFactory
    {
        public static IATBannerAdClient BuildBannerAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATBannerAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new AnyThinkAds.iOS.ATBannerAdClient();
            #else
                
            #endif
            return new UnityBannerClient();
        }

        public static IATInterstitialAdClient BuildInterstitialAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATInterstitialAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new AnyThinkAds.iOS.ATInterstitialAdClient();
            #else

            #endif
            return new UnityInterstitialClient();
        }

        public static IATNativeAdClient BuildNativeAdClient()
        {
           #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATNativeAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new AnyThinkAds.iOS.ATNativeAdClient();
            #else

            #endif
            return new UnityNativeAdClient();
        }

        public static IATNativeBannerAdClient BuildNativeBannerAdClient()
        {
           #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATNativeBannerAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new AnyThinkAds.iOS.ATNativeBannerAdClient();
            #else

            #endif
            return new UnityNativeBannerAdClient();
        }

        public static IATRewardedVideoAdClient BuildRewardedVideoAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.

            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATRewardedVideoAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new AnyThinkAds.iOS.ATRewardedVideoAdClient();            
            #else
                            
            #endif
            return new UnityRewardedVideoAdClient();
        }

        public static IATSDKAPIClient BuildSDKAPIClient()
        {
            Debug.Log("BuildSDKAPIClient");
            #if UNITY_EDITOR
                Debug.Log("Unity Editor");
                        // Testing UNITY_EDITOR first because the editor also responds to the currently
                        // selected platform.

            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATSDKAPIClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                 Debug.Log("Unity:ATAdsClientFactory::Build iOS Client");
                return new AnyThinkAds.iOS.ATSDKAPIClient();         
            #else

            #endif
            return new UnitySDKAPIClient();
        }

        public static IATDownloadClient BuildDownloadClient()
        {
            Debug.Log("BuildDownloadClient");
            #if UNITY_EDITOR
                Debug.Log("Unity Editor");
                        // Testing UNITY_EDITOR first because the editor also responds to the currently
                        // selected platform.

            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATDownloadClient();
               
            #else

            #endif
                return new UnityDownloadClient();
        }

        public static IATSplashAdClient BuildSplashAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new AnyThinkAds.Android.ATSplashAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                //TODO iOS返回开屏client
                return new AnyThinkAds.iOS.ATSplashAdClient();
            #else
            #endif
            return new UnitySplashClient();
        }

    }

    class UnitySDKAPIClient:IATSDKAPIClient
    {
        public void initSDK(string appId, string appkey){}
        public void initSDK(string appId, string appkey, ATSDKInitListener listener){ }
        public void getUserLocation(ATGetUserLocationListener listener){ }
        public void setGDPRLevel(int level){ }
        public void showGDPRAuth(){ }
        public void showGDPRConsentDialog(ATConsentDismissListener listener){ }
        public void addNetworkGDPRInfo(int networkType, string mapJson){ }
        public void setChannel(string channel){ }
        public void setSubChannel(string subchannel){ }
        public void initCustomMap(string cutomMap){ }
        public void setCustomDataForPlacementID(string customData, string placementID){ }
        public void setLogDebug(bool isDebug){ }
        public int getGDPRLevel(){ return ATSDKAPI.PERSONALIZED; }
        public bool isEUTraffic() { return false; }
        public void deniedUploadDeviceInfo(string deniedInfo) { }
        public void setExcludeBundleIdArray(string bundleIds) { }
        public void setExcludeAdSourceIdArrayForPlacementID(string placementID, string adsourceIds) { }
        public void setSDKArea(int area) { }
        public void getArea(ATGetAreaListener listener) { }
        public void setWXStatus(bool install) { }
        public void setLocation(double longitude, double latitude) { }
        public void showDebuggerUI() {}
    }

    class UnityBannerClient:IATBannerAdClient
    {
        public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdImpressEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdAutoRefreshFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseButtonTappedEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;
       ATBannerAdListener listener;
       public void loadBannerAd(string unitId, string mapJson){
            if(listener != null)
            {
                listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }
     
       public void setListener(ATBannerAdListener listener)
       {
            this.listener = listener;
       }

       public string checkAdStatus(string unitId) { return ""; }
       
       public void showBannerAd(string unitId, string position){ }

       public void showBannerAd(string unitId, string position, string mapJson){ }
       
       public void showBannerAd(string unitId, ATRect rect){ }

       public void showBannerAd(string unitId, ATRect rect, string mapJson){ }

       public  void cleanBannerAd(string unitId){ }
      
       public void hideBannerAd(string unitId){ }
    
       public void showBannerAd(string unitId){ }
      
       public void cleanCache(string unitId){}

        public string getValidAdCaches(string unitId) { return ""; }
    }

    class UnityInterstitialClient : IATInterstitialAdClient
    {
       ATInterstitialAdListener listener;
        #pragma warning disable 220

        public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdShowEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdShowFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdVideoFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;

       public void loadInterstitialAd(string unitId, string mapJson){
            if (listener != null)
            {
               listener.onInterstitialAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }
       
       public void setListener(ATInterstitialAdListener listener){
            this.listener = listener;
       }

       public bool hasInterstitialAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public void showInterstitialAd(string unitId, string mapJson){}
        
        public void cleanCache(string unitId){}

        public string getValidAdCaches(string unitId) { return ""; }

        public void entryScenarioWithPlacementID(string placementId, string scenarioID){}

        
		public void addAutoLoadAdPlacementID(string[] placementIDList) {}

        public void removeAutoLoadAdPlacementID(string placementId){}

		public bool autoLoadInterstitialAdReadyForPlacementID(string placementId){return false;}

		public string getAutoValidAdCaches(string placementId){return "";}
        public string checkAutoAdStatus(string unitId) { return ""; }


        public void setAutoLocalExtra(string placementId, string mapJson){}

        public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID){}

		public void showAutoAd(string placementId, string mapJson){}

    }

    class UnityNativeAdClient : IATNativeAdClient
    {

        public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdImpressEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<ATAdProgressEventArgs> onAdVideoProgressEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;

        ATNativeAdListener listener;
       public void loadNativeAd(string unitId, string mapJson){
            if(listener != null)
            {
                listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

       public bool hasAdReady(string unitId) { return false; }

       public string checkAdStatus(string unitId) { return ""; }

       public string getValidAdCaches(string unitId) { return ""; }

       public void entryScenarioWithPlacementID(string placementId, string scenarioID){}


        public void setListener(ATNativeAdListener listener){
            this.listener = listener;
       }
        
       public void renderAdToScene(string unitId, ATNativeAdView anyThinkNativeAdView){}

       public void renderAdToScene(string unitId, ATNativeAdView anyThinkNativeAdView, string mapJson){}

       public void cleanAdView(string unitId, ATNativeAdView anyThinkNativeAdView){}
       
       public void onApplicationForces(string unitId, ATNativeAdView anyThinkNativeAdView){}
        
       public void onApplicationPasue(string unitId, ATNativeAdView anyThinkNativeAdView){}
        
       public void cleanCache(string unitId){}
        
       public void setLocalExtra(string unitid, string mapJson){}
    }

    class UnityNativeBannerAdClient : IATNativeBannerAdClient
    {

         public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdImpressEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<ATAdProgressEventArgs> onAdVideoProgressEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;
        ATNativeBannerAdListener listener;
       public void loadAd(string unitId, string mapJson){
            if(listener != null)
            {
                 listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

       public bool adReady(string unitId) { return false; }
        
       public void setListener(ATNativeBannerAdListener listener){
            this.listener = listener;
       }
       
       public void showAd(string unitId, ATRect rect, Dictionary<string, string> pairs){}
        
       public void removeAd(string unitId){}
    }

    class UnityRewardedVideoAdClient : IATRewardedVideoAdClient
    {
         public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<ATAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdVideoFailureEvent;
        public event EventHandler<ATAdRewardEventArgs> onAdVideoCloseEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onRewardEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;
        public event EventHandler<ATAdEventArgs> onPlayAgainStart;
        public event EventHandler<ATAdEventArgs> onPlayAgainEnd;
        public event EventHandler<ATAdErrorEventArgs> onPlayAgainFailure;
        public event EventHandler<ATAdEventArgs> onPlayAgainClick;
        public event EventHandler<ATAdEventArgs> onPlayAgainReward;

        ATRewardedVideoListener listener;
        public void loadVideoAd(string unitId, string mapJson){
            if (listener != null)
            {
                listener.onRewardedVideoAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

        public void setListener(ATRewardedVideoListener listener){
            this.listener = listener;
       }

        public bool hasAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public string getValidAdCaches(string unitId) { return ""; }

        public void entryScenarioWithPlacementID(string placementId, string scenarioID){}

        public void showAd(string unitId, string mapJson){}

		public void addAutoLoadAdPlacementID(string[] placementIDList) {}

        public void removeAutoLoadAdPlacementID(string placementId){}

		public bool autoLoadRewardedVideoReadyForPlacementID(string placementId){return false;}

		public string getAutoValidAdCaches(string placementId){return "";}
        
        public string checkAutoAdStatus(string unitId) { return ""; }

        public void setAutoLocalExtra(string placementId, string mapJson){}

        public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID){}

		public void showAutoAd(string placementId, string mapJson){}




    }


    class UnityDownloadClient : IATDownloadClient
    {
        public void setListener(ATDownloadAdListener listener)
        {
            Debug.Log("Must run on Android platform");
        }
    }

    class UnitySplashClient : IATSplashAdClient
    {
        public event EventHandler<ATAdEventArgs>    onAdLoadTimeoutEvent;
        public event EventHandler<ATAdEventArgs>    onDeeplinkEvent;
        public event EventHandler<ATAdEventArgs>    onDownloadConfirmEvent;  
        public event EventHandler<ATAdEventArgs> onAdShowEvent;
        public event EventHandler<ATAdEventArgs> onAdCloseEvent;
        // called if the ad has failed to be shown
        public event EventHandler<ATAdErrorEventArgs> onAdShowFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdLoadEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdClickEvent;
        public event EventHandler<ATAdEventArgs> onRewardEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceLoadFailureEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingAttemptEvent;
        public event EventHandler<ATAdEventArgs> onAdSourceBiddingFilledEvent;
        public event EventHandler<ATAdErrorEventArgs> onAdSourceBiddingFailureEvent;
        public event EventHandler<ATAdEventArgs> onPlayAgainStart;
        public event EventHandler<ATAdEventArgs> onPlayAgainEnd;
        public event EventHandler<ATAdErrorEventArgs> onPlayAgainFailure;
        public event EventHandler<ATAdEventArgs> onPlayAgainClick;
        public event EventHandler<ATAdEventArgs> onPlayAgainReward;
        // public void loadSplashAd(string placementId, string mapJson) {}
        public void loadSplashAd(string placementId, int fetchAdTimeout, string defaultAdSourceConfig, string mapJson) {}
        public void setListener(ATSplashAdListener listener) {}

        public bool hasSplashAdReady(string placementId) {
            return false;
        }

        public string checkAdStatus(string placementId) {
            return "";
        }

        public void showSplashAd(string placementId, string mapJson) {}

        /***
		 * 获取所有可用缓存广告
		 */
		public string getValidAdCaches(string placementId) {
            return "";
        }

        public void entryScenarioWithPlacementID(string placementId, string scenarioID) {}
    }
}