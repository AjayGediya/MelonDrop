using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadTxt;

    private bool isLoading = false;
    private const float maxLoadingValue = 5f;
    private const int nextSceneIndex = 1;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (!isLoading)
        {
            loadingSlider.value += Time.deltaTime;
            loadTxt.text = $"Loading... {Mathf.Clamp((int)(loadingSlider.value * 20), 0, 100)}%";

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
