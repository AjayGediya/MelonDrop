using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


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

    public Toggle VibrateToggle;

    int NextFruit;

    public Sprite On, Off;

    public Button SoundButton, MusicButton;

    public List<GameObject> First2Object = new List<GameObject>();

    public bool isButtonOption = false;

    public bool isButtonChange = false;

    public bool isButtonBoxVibrate = false;

    public bool isButtonFirst2Destroy = false;

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

        ScoreText.text = ScoreValue.ToString();
        Debug.Log("Score" + ScoreValue.ToString());
        HighScoreText.text = HighScore.ToString();
        Debug.Log("HighScore" + HighScore.ToString());
    }

    public void Start()
    {
        Fruits = Resources.LoadAll<GameObject>("Prefabs");
        NextImages = Resources.LoadAll<Sprite>("Sprite");
        isGameOver = false;
        GenratedGrid();
        nextImage();
        if (SoundManager.Instance.SoundAudio.volume == 0)
        {
            SoundManager.Instance.isSound = true;
            SoundManager.Instance.SoundAudio.mute = true;
            SoundButton.GetComponent<Image>().sprite = Off;
        }
        else if (SoundManager.Instance.SoundAudio.volume == 1)
        {
            SoundManager.Instance.isSound = false;
            SoundManager.Instance.SoundAudio.mute = false;
            SoundButton.GetComponent<Image>().sprite = On;
        }


        if (MusicManager.instnace.MusicAudio.volume == 0)
        {
            MusicManager.instnace.isMusic = true;
            MusicManager.instnace.MusicAudio.mute = true;
            MusicButton.GetComponent<Image>().sprite = Off;
        }
        else if (MusicManager.instnace.MusicAudio.volume == 1)
        {
            MusicManager.instnace.isMusic = false;
            MusicManager.instnace.MusicAudio.mute = false;
            MusicButton.GetComponent<Image>().sprite = On;
        }

        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            VibrateToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("Vibrate", 0) == 1)
        {
            VibrateToggle.isOn = false;
        }
    }


    public void GenratedGrid()
    {
        int Number = UnityEngine.Random.Range(0, Fruits.Length);
        // Debug.Log(Number);
        GameObject fruit = Instantiate(Fruits[Number]);
        // Debug.Log(fruit + "::" + "Fruit");
        // image.Add(fruit);
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
        //  image.Add(fruit);
        fruit.transform.SetParent(ParentObj.transform);
        fruit.transform.position = FruitsParent.transform.position;
    }

    private void Update()
    {
        foreach (var item in image)
        {
            if (item != null)
            {
                if (item.gameObject.GetComponent<SpriteRenderer>().enabled == false)
                {
                    StartCoroutine(DeletImage(item));
                }
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
            SoundButton.GetComponent<Image>().sprite = Off;
            Debug.Log("Sound_Off");
        }
        else if (SoundManager.Instance.isSound == true)
        {
            SoundManager.Instance.isSound = false;
            PlayerPrefs.SetInt("Sound", 1);
            SoundManager.Instance.SoundAudio.mute = false;
            SoundManager.Instance.SoundAudio.volume = 1;
            SoundButton.GetComponent<Image>().sprite = On;
            Debug.Log("Sound_On");
        }
    }

    public void MusicBtnClick()
    {
        Debug.Log("Music");

        if (MusicManager.instnace.isMusic == false)
        {
            MusicManager.instnace.isMusic = true;
            PlayerPrefs.SetInt("Music", 0);
            MusicManager.instnace.MusicAudio.mute = true;
            MusicManager.instnace.MusicAudio.volume = 0;
            MusicButton.GetComponent<Image>().sprite = Off;
            Debug.Log("Music_Off");
        }
        else
        {
            MusicManager.instnace.isMusic = false;
            PlayerPrefs.SetInt("Music", 1);
            MusicManager.instnace.MusicAudio.mute = false;
            MusicManager.instnace.MusicAudio.volume = 1;
            MusicButton.GetComponent<Image>().sprite = On;
            Debug.Log("Music_On");
        }
    }

    public void VibrateBtnClick()
    {

        if (VibrateToggle.isOn == true)
        {
            Debug.Log("isOn");
            Vibration.Vibrate(50);
            PlayerPrefs.SetInt("Vibrate", 0);
        }
        else if (VibrateToggle.isOn == false)
        {
            Debug.Log("isOff");
            PlayerPrefs.SetInt("Vibrate", 1);
        }
    }

    public void RewardButtonClick()
    {
        AdManager.Instance.ShowRewardedAd();
    }

    //public void ShareButtonClick()
    //{
    //    //StartCoroutine(TakeScreenshotAndShare());
    //}

    ////private IEnumerator TakeScreenshotAndShare()
    ////{
    ////    yield return new WaitForEndOfFrame();

    ////    Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    ////    ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    ////    ss.Apply();

    ////    string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
    ////    File.WriteAllBytes(filePath, ss.EncodeToPNG());

    ////    // To avoid memory leaks
    ////    Destroy(ss);

    ////    new NativeShare().AddFile(filePath)
    ////        .SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
    ////        .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
    ////        .Share();

    ////    // Share on WhatsApp only, if installed (Android only)
    ////    //if( NativeShare.TargetExists( "com.whatsapp" ) )
    ////    //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    ////}

    public void BomButtonClick()
    {
        Debug.Log("Bom");
        isButtonOption = true;
        foreach (var item in image)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ChangeButtonClick()
    {
        isButtonChange = true;
        Debug.Log("Changes");
        foreach (var item in image)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void BoxVibrateButtonClick()
    {
        isButtonBoxVibrate = true;
        Debug.Log("BoxVibrate");
    }

    public void First2DestroyButtonCLick()
    {
        isButtonFirst2Destroy = true;
        StartCoroutine(ResetFruits());
    }

    IEnumerator ResetFruits()
    {
        Debug.Log("First2Destroy");

        for (int i = 0; i < image.Count; i++)
        {

            if (image[i].gameObject.name == "Strawberry(Clone)")
            {
                Debug.Log(image[i]);
                First2Object.Add(image[i]);
            }

            if (image[i].gameObject.name == "Apricot(Clone)")
            {
                Debug.Log(image[i]);
                First2Object.Add(image[i]);
            }
        }

        foreach (var itemObject in First2Object)
        {
            Destroy(itemObject);
        }
        Debug.Log(image.Count);


        image.Clear();
        First2Object.Clear();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < ParentObj.transform.childCount; i++)
        {
            Rigidbody2D rb = ParentObj.transform.GetChild(i).GetComponent<Rigidbody2D>();

            if (rb.isKinematic == false)
            {
                image.Add(ParentObj.transform.GetChild(i).gameObject);
            }
        }
        isButtonFirst2Destroy = false;
    }
}