using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadTxt;

    private bool isLoading = false;
    private float maxLoadingValue = 20f;
    private int nextSceneIndex = 1;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        AppOpen.Instance.LoadAppOpenAd();
    }

    private void Update()
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
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Scene changed");
    }
}
