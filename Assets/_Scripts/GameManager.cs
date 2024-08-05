using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject[] Fruits;

    public GameObject FruitsParent;

    public GameObject ParentObj;

    public List<GameObject> image = new List<GameObject>();

    public bool isFruit = false;

    public TextMeshProUGUI ScoreText;

    public int ScoreValue;

    public TextMeshProUGUI HighScoreText;

    public TextMeshProUGUI ScoreValueOver;

    public int HighScore;

    public GameObject GameOverObject1, GameOverObject2, GameOverObject3;

    public GameObject GamePanel, OverPanel, SettingPanel;

    public bool isGameOver = false;

    public Sprite[] NextImages;

    public Image NextImage;

    public bool isVibrate = false;

    int NextFruit;

    public static GameManager instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

        if (PlayerPrefs.HasKey("Score") == false)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        else
        {
            ScoreValue = PlayerPrefs.GetInt("Score");
        }

        if (PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        else
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }

        ScoreText.text = "Score" + ":" + ScoreValue.ToString();
        HighScoreText.text = "HighScore" + ":" + HighScore.ToString();
    }

    public void Start()
    {
        Fruits = Resources.LoadAll<GameObject>("Prefabs");
        NextImages = Resources.LoadAll<Sprite>("Sprite");
        isGameOver = false;
        GenratedGrid();
        nextImage();

    }


    public void GenratedGrid()
    {
        int Number = UnityEngine.Random.Range(0, Fruits.Length);
        Debug.Log(Number);
        GameObject fruit = Instantiate(Fruits[Number]);
        Debug.Log(fruit + "::" + "Fruit");
        image.Add(fruit);
        fruit.transform.SetParent(ParentObj.transform);
        fruit.transform.position = FruitsParent.transform.position;
    }

    public void nextImage()
    {
        NextFruit = UnityEngine.Random.Range(0, NextImages.Length);
        NextImage.sprite = NextImages[NextFruit];
    }

    public void AfterNextImageCall()
    {
        GameObject fruit = Instantiate(Fruits[NextFruit]);
        image.Add(fruit);
        fruit.transform.SetParent(ParentObj.transform);
        fruit.transform.position = FruitsParent.transform.position;
    }

    private void Update()
    {
        foreach (var item in image)
        {
            if (item.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                StartCoroutine(DeletImage(item));
            }
        }
    }

    public IEnumerator DeletImage(GameObject deletimage)
    {
        yield return new WaitForSeconds(2);

        Destroy(deletimage);
        image.Remove(deletimage);
    }

    public IEnumerator IsFruit()
    {
        yield return new WaitForSeconds(0.3f);
        isFruit = false;
    }

    public void RestartBtnClick()
    {
        OverPanel.SetActive(false);
        ScoreValue = 0;
        ScoreText.text = "0";
        SceneManager.LoadScene(1);
    }

    public void SettingBtnClick()
    {
        Debug.Log("Setting");
        isGameOver = true;
        SettingPanel.SetActive(true);
    }

    public void BackBtnClick()
    {
        Debug.Log("Back");
        SettingPanel.SetActive(false);
        StartCoroutine(TimeforBack());
    }

    public IEnumerator TimeforBack()
    {
        yield return new WaitForSeconds(0.1f);
        isGameOver = false;
    }

    public void SoundBtnClick()
    {
        Debug.Log("Sound");
        if (SoundManager.Instance.isSound == false)
        {
            SoundManager.Instance.isSound = true;
            PlayerPrefs.SetInt("Sound", 0);
            SoundManager.Instance.SoundAudio.mute = true;
            SoundManager.Instance.SoundAudio.volume = 0;
            Debug.Log("Sound_Off");
        }
        else if (SoundManager.Instance.isSound == true)
        {
            SoundManager.Instance.isSound = false;
            PlayerPrefs.SetInt("Sound", 1);
            SoundManager.Instance.SoundAudio.mute = false;
            SoundManager.Instance.SoundAudio.volume = 1;
            Debug.Log("Sound_On");
        }
    }

    public void MusicBtnClick()
    {
        Debug.Log("Music");
    }

    public void VibrateBtnClick()
    {
        Debug.Log("Vibrate");

        if (isVibrate == false)
        {
            Vibration.Vibrate(50);
            Debug.Log("vibrate");
            isVibrate = true;
        }
        else if(isVibrate == true)
        {
            isVibrate = false;
            Debug.Log("No_vibrate");
        }
    }

    public void RewardButtonClick()
    {
        AdManager.Instance.ShowRewardedAd();
    }
}
