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
    public class DummyBannerClient : IBannerClient
    {
        private static string TAG = "Dummy Banner ";

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdOpened;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<EventArgs> OnAdLeftApplication;

        public DummyBannerClient()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void LoadAd(AdRequest request)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void Show()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void Hide()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void Destroy()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }
    }
}