using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private TextMeshProUGUI loadTxt;

    private float maxLoadingValue = 20f;

    private int nextSceneIndex = 1;

    private bool isLoading = false;

    public bool isAppOpenAdTest = false;

    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        if (!isLoading)
        {
            loadingSlider.value += Time.deltaTime;
            loadTxt.text = $"Loading... {Mathf.Clamp((int)(loadingSlider.value * 5), 0, 100)}%";

            if (loadingSlider.value >= maxLoadingValue)
            {
                isLoading = true;
                LoadNextScene();
            }
        }

        //if (AdManager.Instance._appOpenAd == null && isAppOpenAdTest == false)
        //{
        //    Debug.Log("NOT FOUND APPOPEN AD");
        //    AdManager.Instance.LoadAppOpenAd();
        //    Debug.Log("isAppOpenAdTest" + isAppOpenAdTest);
        //}
    }

    IEnumerator AdTime()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Ad Start with load");
        if (AdManager.Instance._appOpenAd == null && isAppOpenAdTest == false)
        {
            Debug.Log("NOT FOUND APPOPEN AD");
            AdManager.Instance.LoadAppOpenAd();
            Debug.Log("isAppOpenAdTest" + isAppOpenAdTest);
            isAppOpenAdTest = true;
        }
        else if (AdManager.Instance._appOpenAd != null)
        {
            Debug.Log("FOUND APPOPEN AD");
            isAppOpenAdTest = true;
            Debug.Log("isAppOpenAdTest" + isAppOpenAdTest + "::::" + "AdFound");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Scene changed");
    }
}
