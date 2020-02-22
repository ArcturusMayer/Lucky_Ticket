/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    public class RewardedAd
    {
        private AdRequestCreator adRequestFactory;
        private IRewardedAdClient client;
        private volatile bool loaded;

        public event EventHandler<EventArgs> OnRewardedAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToLoad;
        public event EventHandler<EventArgs> OnRewardedAdOpened;
        public event EventHandler<EventArgs> OnRewardedAdClosed;
        public event EventHandler<EventArgs> OnRewardedAdLeftApplication;
        public event EventHandler<EventArgs> OnRewardedAdShown;
        public event EventHandler<EventArgs> OnRewardedAdDismissed;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToShow;
        public event EventHandler<Reward> OnRewarded;

        public RewardedAd(string blockId)
        {
            this.adRequestFactory = new AdRequestCreator();
            this.client = YandexMobileAdsClientFactory.BuildRewardedAdClient(blockId);

            ConfigureRewardedAdEvents();
        }
        
        public void SetUserId(string userId)
        {
            client.SetUserId(userId);
        }

        public void LoadAd(AdRequest request)
        {
            this.loaded = false;
            client.LoadAd(adRequestFactory.CreateAdRequest(request));
        }

        public bool IsLoaded()
        {
            return loaded;
        }

        public void Show()
        {
            client.Show();
        }

        public void Destroy()
        {
            client.Destroy();
        }

        private void ConfigureRewardedAdEvents()
        {
            this.client.OnRewardedAdLoaded += (sender, args) =>
            {
                this.loaded = true;
                if (this.OnRewardedAdLoaded != null)
                {
                    this.OnRewardedAdLoaded(this, args);
                }
            };

            this.client.OnRewardedAdFailedToLoad += (sender, args) =>
            {
                if (this.OnRewardedAdFailedToLoad != null)
                {
                    this.OnRewardedAdFailedToLoad(this, args);
                }
            };

            this.client.OnRewardedAdOpened += (sender, args) =>
            {
                if (this.OnRewardedAdOpened != null)
                {
                    this.OnRewardedAdOpened(this, args);
                }
            };

            this.client.OnRewardedAdClosed += (sender, args) =>
            {
                if (this.OnRewardedAdClosed != null)
                {
                    this.OnRewardedAdClosed(this, args);
                }
            };

            this.client.OnRewardedAdLeftApplication += (sender, args) =>
            {
                if (this.OnRewardedAdLeftApplication != null)
                {
                    this.OnRewardedAdLeftApplication(this, args);
                }
            };

            this.client.OnRewardedAdShown += (sender, args) =>
            {
                if (this.OnRewardedAdShown != null)
                {
                    this.OnRewardedAdShown(this, args);
                }
            };

            this.client.OnRewardedAdDismissed += (sender, args) =>
            {
                if (this.OnRewardedAdDismissed != null)
                {
                    this.OnRewardedAdDismissed(this, args);
                }
            };

            this.client.OnRewardedAdFailedToShow += (sender, args) =>
            {
                if (this.OnRewardedAdFailedToShow != null)
                {
                    this.OnRewardedAdFailedToShow(this, args);
                }
            };

            this.client.OnRewarded += (sender, args) =>
            {
                if (this.OnRewarded != null)
                {
                    this.OnRewarded(this, args);
                }
            };
        }
    }
}