using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingSlider;

    public TextMeshProUGUI loadTxt;

    public bool isLoading = false;

    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        if (!isLoading)
        {
            loadingSlider.value += Time.deltaTime;
            loadTxt.text = $"Loading..." + ((int)(loadingSlider.value * 20)) + "%";

            if (loadingSlider.value >= loadingSlider.maxValue)
            {
                isLoading = true;
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
