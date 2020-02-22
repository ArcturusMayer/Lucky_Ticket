/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Runtime.InteropServices;
using YandexMobileAds.Base;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
    
    public class BannerClient : IBannerClient, IDisposable
    {
        private IntPtr selfPointer;

        public string ObjectId { get; private set; }

        internal delegate void YMAUnityAdViewDidReceiveAdCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error);

        internal delegate void YMAUnityAdViewWillPresentScreenCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidDismissScreenCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewWillLeaveApplicationCallback(
            IntPtr bannerClient);

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdOpened;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<EventArgs> OnAdLeftApplication;

        public BannerClient(string blockId, AdSize adSize, AdPosition position)
        {
            this.selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = BannerBridge.YMAUnityCreateBannerView(
                this.selfPointer, blockId, adSize.Width, adSize.Height, 
                (int)position);
            BannerBridge.YMAUnitySetBannerCallbacks(
                this.ObjectId,
                    AdViewDidReceiveAdCallback,
                    AdViewDidFailToReceiveAdWithErrorCallback,
                    AdViewWillPresentScreenCallback,
                    AdViewDidDismissScreenCallback,
                    AdViewWillLeaveApplicationCallback);
        }

        public void LoadAd(AdRequest request)
        {
            AdRequestClient adRequest = null; 
            if (request != null)
            {
                adRequest = new AdRequestClient(request);   
            }
            BannerBridge.YMAUnityLoadBannerView(
                this.ObjectId, adRequest.ObjectId);
        }

        public void Show()
        {
            BannerBridge.YMAUnityShowBannerView(this.ObjectId);
        }

        public void Hide()
        {
            BannerBridge.YMAUnityHideBannerView(this.ObjectId);
        }

        public void Destroy()
        {
            this.Hide();
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~BannerClient()
        {
            this.Destroy();
        }

        private static BannerClient IntPtrToBannerClient(IntPtr bannerClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(bannerClient);
            return handle.Target as BannerClient;
        }

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidReceiveAdCallback))]
        private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidReceiveAdCallback))]
        private static void AdViewDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewWillPresentScreenCallback))]
        private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdOpened != null)
            {
                client.OnAdOpened(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidDismissScreenCallback))]
        private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewWillLeaveApplicationCallback))]
        private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdLeftApplication != null)
            {
                client.OnAdLeftApplication(client, EventArgs.Empty);
            }
        }

        #endregion
    }

    #endif
}