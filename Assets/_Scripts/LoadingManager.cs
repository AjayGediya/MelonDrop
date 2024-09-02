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
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
        //Debug.Log("Scene changed");
    }
}
