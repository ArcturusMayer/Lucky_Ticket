/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Reflection;
using YandexMobileAds.Base;
using YandexMobileAds.Platforms;
using UnityEngine;

namespace YandexMobileAds.Common
{
    public class DummyRewardedAdClient : IRewardedAdClient
    {
        private static string TAG = "Dummy RewardedAd ";

        public event EventHandler<EventArgs> OnRewardedAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToLoad;
        public event EventHandler<EventArgs> OnRewardedAdOpened;
        public event EventHandler<EventArgs> OnRewardedAdClosed;
        public event EventHandler<EventArgs> OnRewardedAdLeftApplication;
        public event EventHandler<EventArgs> OnRewardedAdShown;
        public event EventHandler<EventArgs> OnRewardedAdDismissed;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToShow;
        public event EventHandler<Reward> OnRewarded;

        public DummyRewardedAdClient()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }
        
        public void SetUserId(string userId)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void LoadAd(AdRequest request)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public bool IsLoaded()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
            return false;
        }

        public void Show()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void Destroy()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }
    }
}