/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using UnityEngine;

namespace YandexMobileAds.Platforms.Android
{
    public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
    {
        private AndroidJavaObject interstitial;

        public event EventHandler<EventArgs> OnInterstitialLoaded;
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToLoad;
        public event EventHandler<EventArgs> OnInterstitialOpened;
        public event EventHandler<EventArgs> OnInterstitialClosed;
        public event EventHandler<EventArgs> OnInterstitialLeftApplication;
        public event EventHandler<EventArgs> OnInterstitialShown;
        public event EventHandler<EventArgs> OnInterstitialDismissed;
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToShow;

        public InterstitialClient(string blockId) : base(Utils.UnityInterstitialAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);

            AndroidJavaObject activity =
                playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            this.interstitial = new AndroidJavaObject(
                Utils.InterstitialClassName,
                activity,
                blockId);
            this.interstitial.Call("setUnityInterstitialListener", this);
        }

        public void LoadAd(AdRequest request)
        {
            this.interstitial.Call("loadAd", Utils.GetAdRequestJavaObject(request));
        }

        public bool IsLoaded()
        {
            return this.interstitial.Call<bool>("isInterstitialLoaded");
        }

        public void Show()
        {
            this.interstitial.Call("showInterstitial");
        }

        public void Destroy()
        {
            this.interstitial.Call("setUnityInterstitialListener", null);
            this.interstitial.Call("destroyInterstitial");
        }

        public void onInterstitialLoaded()
        {
            if (this.OnInterstitialLoaded != null)
            {
                this.OnInterstitialLoaded(this, EventArgs.Empty);
            }
        }

        public void onInterstitialFailedToLoad(string errorReason)
        {
            if (this.OnInterstitialFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                this.OnInterstitialFailedToLoad(this, args);
            }
        }

        public void onInterstitialOpened()
        {
            if (this.OnInterstitialOpened != null)
            {
                this.OnInterstitialOpened(this, EventArgs.Empty);
            }
        }

        public void onInterstitialClosed()
        {
            if (this.OnInterstitialClosed != null)
            {
                this.OnInterstitialClosed(this, EventArgs.Empty);
            }
        }

        public void onInterstitialLeftApplication()
        {
            if (this.OnInterstitialLeftApplication != null)
            {
                this.OnInterstitialLeftApplication(this, EventArgs.Empty);
            }
        }

        public void onInterstitialShown()
        {
            if (this.OnInterstitialShown != null)
            {
                this.OnInterstitialShown(this, EventArgs.Empty);
            }
        }

        public void onInterstitialDismissed()
        {
            if (this.OnInterstitialDismissed != null)
            {
                this.OnInterstitialDismissed(this, EventArgs.Empty);
            }
        }

        public void onInterstitialFailedToShow(string errorReason)
        {
            if (this.OnInterstitialFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                this.OnInterstitialFailedToShow(this, args);
            }
        }
    }
}