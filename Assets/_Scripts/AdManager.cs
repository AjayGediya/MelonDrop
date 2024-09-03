using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using GoogleMobileAds.Common;


public class AdManager : MonoBehaviour
{
    // private string BannerId = "ca-app-pub-3940256099942544/6300978111";
    public string BannerId;

    //private string InterStitleId = "ca-app-pub-3940256099942544/1033173712";
    public string InterStitleId;

    //private string RewardId = "ca-app-pub-3940256099942544/5224354917";
    public string RewardId;

    //private string AppOpenId = "ca-app-pub-3940256099942544/9257395921";
    public string AppOpenId;

    public BannerView _bannerView;

    public InterstitialAd _interstitialAd;

    public RewardedAd _rewardedAd;

    public AppOpenAd _appOpenAd;

    public bool isRewardShow = false;


    public bool isRewardLoad = false;

    public bool isShow = false;

    public bool isAdStop = false;

    public int Number = 0;

    public int AdAvailablevalue = 0;

    public int AdSplashvalue = 0;

    public int InterstitialAcc = 0;

    public int BannerAcc = 0;

    public int RewardAcc = 0;

    public int AppOpenAcc = 0;

    public Root root;

    private DateTime _expireTime;

    private bool isShowingAd = false;

    public static AdManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(GetRequest("https://dev.appkiduniya.in/DigitalMineNetwork/MoreApp/Api/App/getAppAdChange?app_id=2"));
    }


    public void AdManage()
    {
        MobileAds.Initialize(initStatus =>
        {

            if (AppOpenAcc == 2 && AdAvailablevalue == 1)
            {
                LoadAppOpenAd();
            }

            Debug.Log(InterstitialAcc);
            Debug.Log(RewardAcc);
            Debug.Log(AdAvailablevalue);
            Debug.Log(AppOpenAcc);

            if (InterstitialAcc == 2 && AdAvailablevalue == 1)
            {
                Debug.Log("AdLoading");
                LoadInterstitialAd();
            }

            if (RewardAcc == 2 && AdAvailablevalue == 1)
            {
                Debug.Log("AdLoading");
                LoadRewardedAd();
            }

            //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        });
    }


    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            Debug.Log("webRequest.result" + webRequest.result);
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    root = JsonUtility.FromJson<Root>(webRequest.downloadHandler.text);
                    InterStitleId = root.ad_priority.ads[0].id;
                    AppOpenId = root.ad_priority.ads[1].id;
                    BannerId = root.ad_priority.ads[2].id;
                    RewardId = root.ad_priority.ads[3].id;
                    Number = int.Parse(root.data.app_version_code.ToString());
                    AdAvailablevalue = int.Parse(root.data.is_advertise_available.ToString());
                    Debug.Log("AdAvailablevalue" + AdAvailablevalue);
                    AdSplashvalue = int.Parse(root.data.is_splash_available.ToString());
                    Debug.Log("AdSplashvalue" + AdSplashvalue);
                    InterstitialAcc = int.Parse(root.ad_priority.ads[0].acc_type.ToString());
                    Debug.Log(InterstitialAcc + "InterstitialAcc");
                    BannerAcc = int.Parse(root.ad_priority.ads[2].acc_type.ToString());
                    Debug.Log("BannerAcc" + BannerAcc);
                    RewardAcc = int.Parse(root.ad_priority.ads[3].acc_type.ToString());
                    Debug.Log("RewardAcc" + RewardAcc);
                    AppOpenAcc = int.Parse(root.ad_priority.ads[1].acc_type.ToString());
                    break;
            }

            AdManage();
        }
    }




    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
            Debug.Log("Destroy");
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(BannerId, AdSize.Banner, AdPosition.Bottom);
        ListenToAdEvents();
        Debug.Log("Event");
    }

    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
            Debug.Log("A"); //load
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
            LoadAd();
        };
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }




    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(InterStitleId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                RegisterEventHandlers(_interstitialAd);
            });
    }

    public void ShowInterstitialAd()
    {
        isShow = true;
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
            isAdStop = true;
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
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
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            LoadInterstitialAd();
            StartCoroutine(ChangeStopBool());
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialAd();
        };
    }


    IEnumerator ChangeStopBool()
    {
        yield return new WaitForSeconds(0.3f);
        isAdStop = false;
    }

    /// <summary>
    /// Shows the Rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(RewardId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    isRewardShow = false;
                    isRewardLoad = false;
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                isRewardShow = true;
                isRewardLoad = true;
                _rewardedAd = ad;
                RegisterEventHandlers(_rewardedAd);
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                //Debug.Log("View + Pouse");
                isAdStop = true;
            });
        }
        else
        {
            LoadRewardedAd();
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            Debug.Log("A");
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
            Debug.Log("B");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
            Debug.Log("C");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
            Debug.Log("D");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            isRewardShow = false;
            //GameManager.instance.isBom = true;
            LoadRewardedAd();
            StartCoroutine(ChangeStopBool());
            //Debug.Log("E");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            Debug.Log("f");
            LoadRewardedAd();
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
            return _appOpenAd != null
                && DateTime.Now < _expireTime;
        }
    }

    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
            _appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.

        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(AppOpenId, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                _appOpenAd = ad;
                RegisterEventHandlers(ad);
                _expireTime = DateTime.UtcNow;
            });
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
            LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            LoadAppOpenAd();
        };
    }

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            if (IsAdAvailable)
            {
                ShowAppOpenAd();
            }
        }
    }


    public void ShowAppOpenAd()
    {
        if (_appOpenAd != null && _appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            _appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
            LoadAppOpenAd();
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