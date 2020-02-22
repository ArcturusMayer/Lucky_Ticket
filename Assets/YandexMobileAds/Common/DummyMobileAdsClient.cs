/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Reflection;
using UnityEngine;

namespace YandexMobileAds.Common
{
    public class DummyMobileAdsClient : IMobileAdsClient
    {
        private static string TAG = "Dummy Mobile Ads";

        public void SetUserConsent(bool consent)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }
    }
}