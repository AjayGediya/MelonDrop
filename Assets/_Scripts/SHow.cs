using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHow : MonoBehaviour
{
    public static SHow Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; } // stops dups running
        DontDestroyOnLoad(gameObject); // keep me forever
        Instance = this; // set the reference to it
    }

    private void Start()
    {

        if (AdManager.Instance.AdSplashvalue == 1 && AdManager.Instance.AppOpenAcc == 2)
        {
            Debug.Log("Show App opwn");

            AdManager.Instance.ShowAppOpenAd();
        }
        GameManager.instance.ShowUpdateDialog();
    }
}
