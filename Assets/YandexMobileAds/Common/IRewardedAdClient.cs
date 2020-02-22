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

namespace YandexMobileAds.Common
{
    public interface IRewardedAdClient
    {
        // Event fired when rewarded ad has been received.
        event EventHandler<EventArgs> OnRewardedAdLoaded;

        // Event fired when rewarded ad has failed to load.
        event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToLoad;

        // Event fired when rewarded ad is opened.
        event EventHandler<EventArgs> OnRewardedAdOpened;

        // Event fired when rewarded ad is closed.
        event EventHandler<EventArgs> OnRewardedAdClosed;

        // Event fired when rewarded ad is leaving application.
        event EventHandler<EventArgs> OnRewardedAdLeftApplication;

        // Event fired when rewarded ad is shown.
        event EventHandler<EventArgs> OnRewardedAdShown;

        // Event fired when rewarded ad is dismissed.
        event EventHandler<EventArgs> OnRewardedAdDismissed;

        // Event fired when rewarded ad has failed to show.
        event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToShow;

        // Event fired when the rewarded ad has rewarded the user.
        event EventHandler<Reward> OnRewarded;

        // Set user id.
        void SetUserId(string userId);
        
        // Loads new rewarded ad.
        void LoadAd(AdRequest request);

        // Determines whether rewarded ad has loaded.
        bool IsLoaded();

        // Shows RewardedAd.
        void Show();

        // Destroys RewardedAd.
        void Destroy();
    }
}