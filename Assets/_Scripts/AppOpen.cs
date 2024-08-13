using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;


public class AppOpen : MonoBehaviour
{
    public static AppOpen Instance;

    public string _adUnitId;

    private AppOpenAd appOpenAd;

    private DateTime _expireTime;

    public bool IsAdAvailable
    {
        get
        {
            return appOpenAd != null
                   && DateTime.Now < _expireTime;
        }
    }

    private void Awake()
    {
        Instance = this;

        //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    public void Start()
    {
        //    MobileAds.Initialize((InitializationStatus initStatus) =>
        //    {
        //        Debug.Log("===========>LOAD");
        //        //if(GetAlldata.Instance.is_splash_available == 1)
        //        LoadAppOpenAd();
        //    });
        //}

        //public void LoadAfterGetData(string adID)
        //{
        //    _adUnitId = adID;

        if (_adUnitId != null)
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                LoadAppOpenAd();
            });
        }
    }


    public void LoadAppOpenAd()
    {
        if (AdManager.Instance.root.ad_priority.ads[7].acc_type == "0")
            return;

        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(_adUnitId, adRequest,
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

                _expireTime = DateTime.Now + TimeSpan.FromHours(4);

                appOpenAd = ad;
                RegisterEventHandlers(ad);
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

    public void ShowAppOpenAd()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        if (AdManager.Instance.root.data.is_advertise_available != "1")
            return;

        if (AdManager.Instance.root.data.is_splash_available != "1")
            return;

        if (AdManager.Instance.root.ad_priority.ads[7].acc_type == "0")
            return;

        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
        //#endif
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

    private void OnDestroy()
    {
        //AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }
}













//old shit
/*public static AppOPen Instance;
    AppOpenAd _appOpenAd;
    public string AppOpenUnitId;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //StartCoroutine(ForWaitAppOpenAds());
        });
    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }



    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
            _appOpenAd = null;
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(AppOpenUnitId,adRequest,
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
            });

        ShowAppOpenAd();
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
            //LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);

        };
    }

    IEnumerator ForWaitAppOpenAds()
    {
        while(!GetAlldata.Instance.AppOpenShow)
        {
            yield return null;
        }
        LoadAppOpenAd();
    }

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            StartCoroutine(ShowAppOpenAdWithDelay());
        }
    }

    IEnumerator ShowAppOpenAdWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ShowAppOpenAd();
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
        }
    }*/