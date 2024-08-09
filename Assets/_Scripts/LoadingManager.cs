using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider LoadingSlider;

    public TextMeshProUGUI LoadTxt;

    public bool isLoading = false;

    //private void Start()
    //{
    //    SceneManager.LoadScene(1);
    //}
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        if (isLoading == false)
        {
            LoadingSlider.value += Time.deltaTime;
            LoadTxt.text = "Loading..." + ((int)(LoadingSlider.value * 20)) + "%";

            if (LoadingSlider.value >= 5)
            {
                isLoading = true;
                SceneManager.LoadScene(1);
                Debug.Log("ScneChange");
            }
        }
    }
}
