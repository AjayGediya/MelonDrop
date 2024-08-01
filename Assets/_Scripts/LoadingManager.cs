using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider LoadingSlider;

    public TextMeshProUGUI LoadTxt;

    public bool isLoading = false;

    private void Update()
    {
        if (isLoading == false)
        {
            LoadingSlider.value += Time.deltaTime;

            //Debug.Log((int)(LoadingSlider.value * 20));
            //Debug.Log((LoadingSlider.value * 20).ToString("00"));
            LoadTxt.text = "Loading..." + ((int)(LoadingSlider.value * 20)) + "%";

            if (LoadingSlider.value >= 5)
            {
                isLoading = true;
                SceneManager.LoadScene(1);
                Debug.Log("LoadingDone");
            }
        }
    }
}
