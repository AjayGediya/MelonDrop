using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using GoogleMobileAds.Common;


public class AdManager : MonoBehaviour
{
    [Header("All Ad_Id")]
    // private string BannerId = "ca-app-pub-3940256099942544/6300978111";
    public string bannerId;

    //private string InterStitleId = "ca-app-pub-3940256099942544/1033173712";
    public string interStitleId;

    //private string RewardId = "ca-app-pub-3940256099942544/5224354917";
    public string rewardId;

    //private string AppOpenId = "ca-app-pub-3940256099942544/9257395921";
    public string appOpenId;

    public BannerView _BannerView;

    public InterstitialAd _InterstitialAd;

    public RewardedAd _RewardedAd;

    public AppOpenAd _AppOpenAd;

    [Header("AllBool")]
    public bool isRewardShowBool = false;

    public bool isCanAppOpenShowBool = false;

    public bool isRewardLoadBool = false;

    public bool isInterstitialLoadBool = false;

    public bool isShowBool = false;

    public bool isAdStopBool = false;

    public bool isShowAppOpenTime = false;

    [Header("AllInt")]
    public int number = 0;

    public int adAvailablevalue = 0;

    public int adSplashvalue = 0;

    public int interstitialAcc = 0;

    public int bannerAcc = 0;

    public int rewardAcc = 0;

    public int appOpenAcc = 0;

    [Header("All_Key")]
    public List<string> all_ad_key = new List<string>();

    [Header("All Ads_Id")]
    public List<string> all_ad_id = new List<string>();

    [Header("All Ad_AccType")]
    public List<string> all_ad_acctype = new List<string>();

    public Root roots;

    private DateTime _expiresTime;

    public string urlName;

    public static AdManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        isCanAppOpenShowBool = true;

        StartCoroutine(GetRequestData(urlName));
    }


    public void AllAdManage()
    {
        MobileAds.Initialize(initStatus =>
        {
            if (appOpenAcc == 2 && adAvailablevalue == 1)
            {
                LoadAppOpensAd();
            }

            Debug.Log(interstitialAcc);
            Debug.Log(rewardAcc);
            Debug.Log(adAvailablevalue);
            Debug.Log(appOpenAcc);

            if (interstitialAcc == 2 && adAvailablevalue == 1)
            {
                LoadInterstitialsAd();
            }

            if (rewardAcc == 2 && adAvailablevalue == 1)
            {
                LoadRewardedsAd();
            }

            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        });
    }

    private void OnDestroy()
    {
        // Always unlisten to events when complete.
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    public IEnumerator GetRequestData(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] Allpages = uri.Split('/');
            int Allpage = Allpages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(Allpages[Allpage] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(Allpages[Allpage] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(Allpages[Allpage] + ":\nReceived: " + webRequest.downloadHandler.text);
                    roots = JsonUtility.FromJson<Root>(webRequest.downloadHandler.text);

                    for (int i = 0; i < roots.ad_priority.ads.Count; i++)
                    {
                        Debug.Log(roots.ad_priority.ads[i].key);
                        all_ad_key.Add(roots.ad_priority.ads[i].key);
                        all_ad_id.Add(roots.ad_priority.ads[i].id);
                        all_ad_acctype.Add(roots.ad_priority.ads[i].acc_type);
                    }

                    interStitleId = FindId("interstitial_ad");
                    appOpenId = FindId("app_open_ad");
                    bannerId = FindId("banner_ad");
                    rewardId = FindId("reward_ad");

                    number = int.Parse(roots.data.app_version_code.ToString());
                    adAvailablevalue = int.Parse(roots.data.is_advertise_available.ToString());
                    adSplashvalue = int.Parse(roots.data.is_splash_available.ToString());

                    interstitialAcc = FindAcctype("interstitial_ad");
                    bannerAcc = FindAcctype("banner_ad");
                    rewardAcc = FindAcctype("reward_ad");
                    appOpenAcc = FindAcctype("app_open_ad");
                    break;
            }

            AllAdManage();
        }
    }

    private string FindId(string key)
    {
        for (int i = 0; i < all_ad_key.Count; i++)
        {
            if (key == all_ad_key[i])
            {
                return all_ad_id[i];
            }
        }
        return null;
    }

    private int FindAcctype(string key)
    {
        for (int i = 0; i < all_ad_key.Count; i++)
        {
            if (key == all_ad_key[i])
            {
                return int.Parse(all_ad_acctype[i]);
            }
        }
        return 0;
    }


    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void CreateBannerAdView()
    {
        if (_BannerView != null)
        {
            DestroyBannerAd();
        }

        // Create a 320x50 banner at top of the screen
        _BannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        ListenToBannerAdEvents();
    }

    public void LoadBannerAd()
    {
        if (_BannerView == null)
        {
            CreateBannerAdView();
        }

        var adRequest = new AdRequest();

        _BannerView.LoadAd(adRequest);
    }

    private void ListenToBannerAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _BannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _BannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _BannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _BannerView.OnAdPaid += (AdValue adValues) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValues.Value,
                adValues.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _BannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _BannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _BannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _BannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
            LoadBannerAd();
        };
    }

    public void DestroyBannerAd()
    {
        if (_BannerView != null)
        {
            _BannerView.Destroy();
            _BannerView = null;
        }
    }




    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void LoadInterstitialsAd()
    {
        if (_InterstitialAd != null)
        {
            _InterstitialAd.Destroy();
            _InterstitialAd = null;
        }

        var adInterstitalRequest = new AdRequest();

        InterstitialAd.Load(interStitleId, adInterstitalRequest,
            (InterstitialAd adInterstital, LoadAdError error) =>
            {
                if (error != null || adInterstital == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    isInterstitialLoadBool = false;
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + adInterstital.GetResponseInfo());

                isInterstitialLoadBool = true;
                _InterstitialAd = adInterstital;
                RegisterInterstitalEventHandlers(_InterstitialAd);
            });
    }

    public void ShowInterstitialsAd()
    {
        isShowBool = true;
        if (_InterstitialAd != null && _InterstitialAd.CanShowAd())
        {
            _InterstitialAd.Show();
            isAdStopBool = true;
        }
        else
        {
            LoadInterstitialsAd();
        }
    }

    private void RegisterInterstitalEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            isCanAppOpenShowBool = false;
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            isCanAppOpenShowBool = true;
            isInterstitialLoadBool = false;
            LoadInterstitialsAd();
            StartCoroutine(ChangeAdStopBool());
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialsAd();
        };
    }


    IEnumerator ChangeAdStopBool()
    {
        yield return new WaitForSeconds(0.3f);
        isAdStopBool = false;
    }



    /// <summary>
    /// Shows the Rewarded ad.
    /// </summary>
    public void LoadRewardedsAd()
    {
        // Clean up the old ad before loading a new one.
        if (_RewardedAd != null)
        {
            _RewardedAd.Destroy();
            _RewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequested = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(rewardId, adRequested,
            (RewardedAd adReward, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || adReward == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    isRewardShowBool = false;
                    isRewardLoadBool = false;
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + adReward.GetResponseInfo());

                isRewardShowBool = true;
                isRewardLoadBool = true;
                _RewardedAd = adReward;
                RegisterRewardedEventHandlers(_RewardedAd);
            });
    }

    public void ShowRewardedsAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_RewardedAd != null && _RewardedAd.CanShowAd())
        {
            _RewardedAd.Show((Reward rewardad) =>
            {
                // TODO: Reward the user.
                //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                //Debug.Log("View + Pouse");
                isAdStopBool = true;
            });
        }
        else
        {
            LoadRewardedsAd();
        }
    }

    private void RegisterRewardedEventHandlers(RewardedAd adReward)
    {
        // Raised when the ad is estimated to have earned money.
        adReward.OnAdPaid += (AdValue adValues) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValues.Value,
                adValues.CurrencyCode));
            Debug.Log("A");
        };
        // Raised when an impression is recorded for an ad.
        adReward.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
            Debug.Log("B");
        };
        // Raised when a click is recorded for an ad.
        adReward.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
            Debug.Log("C");
        };
        // Raised when an ad opened full screen content.
        adReward.OnAdFullScreenContentOpened += () =>
        {
            isCanAppOpenShowBool = false;
            Debug.Log("Rewarded ad full screen content opened.");
            Debug.Log("D");
        };
        // Raised when the ad closed full screen content.
        adReward.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            isRewardShowBool = false;
            isCanAppOpenShowBool = true;
            //GameManager.instance.isBom = true;
            LoadRewardedsAd();
            StartCoroutine(ChangeAdStopBool());
            isRewardLoadBool = false;
            //Debug.Log("E");
        };
        // Raised when the ad failed to open full screen content.
        adReward.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            Debug.Log("f");
            LoadRewardedsAd();
        };
    }




    /// <summary>
    /// Loads the app open ad.
    /// </summary>
    ///

    public bool IsAdAvailable
    {
        get
        {
            return _AppOpenAd != null;

        }
    }

    public void LoadAppOpensAd()
    {
        // Clean up the old ad before loading a new one.
        if (_AppOpenAd != null)
        {
            _AppOpenAd.Destroy();
            _AppOpenAd = null;
        }

        var adAppOpenRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(appOpenId, adAppOpenRequest,
            (AppOpenAd adAppOpen, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || adAppOpen == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : " + adAppOpen.GetResponseInfo());

                _AppOpenAd = adAppOpen;
                RegisterAppOpenEventHandlers(adAppOpen);
                _expiresTime = DateTime.UtcNow;

                if (!isShowAppOpenTime)
                {
                    isShowAppOpenTime = true;

                    if (adSplashvalue == 1 && appOpenAcc == 2)
                    {
                        ShowAppOpensAd();
                    }
                }
            });
    }

    private void RegisterAppOpenEventHandlers(AppOpenAd adAppOpen)
    {
        // Raised when the ad is estimated to have earned money.
        adAppOpen.OnAdPaid += (AdValue adValues) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValues.Value,
                adValues.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        adAppOpen.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        adAppOpen.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        adAppOpen.OnAdFullScreenContentOpened += () =>
        {
            isCanAppOpenShowBool = false;
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        adAppOpen.OnAdFullScreenContentClosed += () =>
        {
            isCanAppOpenShowBool = true;
            Debug.Log("App open ad full screen content closed.");
            LoadAppOpensAd();
        };
        // Raised when the ad failed to open full screen content.
        adAppOpen.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            LoadAppOpensAd();
        };
    }

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        if (state == AppState.Foreground)
        {
            if (IsAdAvailable && isCanAppOpenShowBool && !GameManager.instance.timerPopup.activeInHierarchy)
            {
                ShowAppOpensAd();
            }
        }
    }

    public void ShowAppOpensAd()
    {
        if (adAvailablevalue == 1)
        {
            if (_AppOpenAd != null && _AppOpenAd.CanShowAd())
            {
                Debug.Log("Showing app open ad.");
                _AppOpenAd.Show();
            }
            else
            {
                Debug.LogError("App open ad is not ready yet.");
                LoadAppOpensAd();
            }
        }
    }
}



[System.Serializable]
public class Ad
{
    public string acc_type;
    public string add_type;
    public string key;
    public string id;
    public string add_size;
    public string acc_id;
}

[System.Serializable]
public class AdPriority
{
    public string priority;
    public List<Ad> ads = new List<Ad>();
}

[System.Serializable]
public class Data
{
    public string developer_name;
    public bool is_rate_dialog;
    public bool is_user_dialog;
    public string user_dialog_text;
    public string user_dialog_url;
    public string app_version_code;
    public string app_version_code_ios;
    public string user_dialog_update_text;
    public string google_id;
    public string bitcoin_url;
    public string predchamp_url;
    public string game_url;
    public string is_back_press_count;
    public string is_normal_ad_count;
    public string is_advertise_available;
    public string app_url;
    public string app_node_url;
    public string testFlag;
    public string is_login_enable;
    public string is_splash_available;
    public string is_withdrawal;
    public string is_info_available;
    public string is_qureka;
    public bool is_show_second;
    public string inter_mill_sec;
    public string is_loader;
    public string is_back_button_show;
    public string start_button_inter;
    public string how_it_works;
    public string is_extra_rewards;
    public bool is_on_resume;
    public bool is_stripe_payment;
    public string privacy_url;
    public string terms_url;
    public string usr_dialog_btn;
    public string rate_text;
    public string user_dialog_img;
}

[System.Serializable]
public class Root
{
    public Data data;
    public AdPriority ad_priority;
    public int ResponseCode;
    public string ResponseMsg;
    public string Result;
    public string ServerTime;
}