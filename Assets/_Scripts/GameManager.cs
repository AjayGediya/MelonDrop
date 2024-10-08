using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject[] Fruits;

    public Sprite[] NextImages;

    public GameObject FruitsParent;

    public GameObject ParentObj;

    public GameObject GameOverObject1, GameOverObject2, GameOverObject3;

    public GameObject GamePanel, OverPanel, SettingPanel, HelpPanel, ExitPanel;

    public GameObject Box;

    public GameObject ColliderObject;

    public GameObject NotAd;

    public GameObject NotFound;

    public GameObject UpdatePopUp;

    public GameObject TimerPopup;

    public GameObject InterNetPopup;

    public GameObject HowtoPlayPopup;

    public int HighScore;

    int NextFruit;

    public int ScoreValue;

    public float rotateSPeed;

    public float wobbleDuration = 0.1f;

    public float wobbleAngle = 5f;

    public float distance = 1.5f;

    public float duration = 1f;

    //public Text timerText;
    public string Timer;

    public float timeRemaining = 0; // 300 seconds = 5 minutes

    public string Second5;

    float minutes;

    float seconds;

    public bool timerIsRunning = false;

    private bool fiveSecondWarningGiven = false;

    public bool isTime = false;

    public bool isAppOpenChange = false;

    public bool isoff = false;

    //public bool isBom = false;

    public bool isGameOver = false;

    public bool isButtonOption = false;

    public bool isButtonChange = false;

    public bool isButtonBoxVibrate = false;

    public bool isButtonFirst2Destroy = false;

    public bool isHelp = false;

    public bool isFruit = false;

    public bool isNet = false;

    public bool isBoxVibrate = false;

    public bool isSettingPaneloff = false;

    public bool isExitPanel = false;

    public bool isLastExit = false;

    public bool isExit = false;

    public bool isChangeOneTime = false;

    public bool isHowToPlayTouch = false;

    public bool isPanelStart = false;

    public bool isSetting = false;

    public bool isInternet = false;

    public bool isUpdate = false;

    public Sprite On, Off;

    public Button SoundButton, MusicButton;

    public Button BomBtn, ChangeBtn, VibrateBtn, First2Destroybtn, Helpbtn, SettingBtn;

    public Image NextImage;

    public Toggle VibrateToggle;

    public Camera main;

    public ParticleSystem particle;

    public ShareText shareText;

    public TextMeshProUGUI TimerTxt;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI HighScoreText;

    public TextMeshProUGUI ScoreValueOver;

    public List<GameObject> First2Object = new List<GameObject>();

    public List<GameObject> image = new List<GameObject>();

    public static GameManager instance;

    GameObject Fruit;

    public GameObject[] AllFruit;

    public int verionCode;  ///VERSION CODE CHAGE HERE  api ma change kre tyare aama same number vdharvo

    void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

        ScoreValue = PlayerPrefs.GetInt("Score", 0);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        ScoreText.text = ScoreValue.ToString();
        HighScoreText.text = HighScore.ToString();
    }

    void Start()
    {

        if (PlayerPrefs.GetInt("HowToPlay") == 0)
        {
            isHowToPlayTouch = true;
            isPanelStart = true;
        }
        else if (PlayerPrefs.GetInt("HowToPlay") == 1)
        {
            HowtoPlayPopup.SetActive(false);
            isHowToPlayTouch = false;
            isPanelStart = false;
        }

        isChangeOneTime = true;

        AdManager.Instance.isShow = false;

        if (UpdatePopUp.activeInHierarchy)
        {
            timerIsRunning = false;
        }
        else
        {
            timerIsRunning = true;
        }

        if (AdManager.Instance.BannerAcc == 2 && AdManager.Instance.AdAvailablevalue == 1)
        {
            AdManager.Instance.LoadAd();
            //BANER
        }

        if (AdManager.Instance._interstitialAd == null)
        {
            if (AdManager.Instance.InterstitialAcc == 2 && AdManager.Instance.AdAvailablevalue == 1)
            {
                AdManager.Instance.LoadInterstitialAd();
            }
        }

        if (AdManager.Instance._rewardedAd == null)
        {
            if (AdManager.Instance.RewardAcc == 2 && AdManager.Instance.AdAvailablevalue == 1)
            {
                AdManager.Instance.LoadRewardedAd();
            }
        }

        Fruits = Resources.LoadAll<GameObject>("Prefabs");
        NextImages = Resources.LoadAll<Sprite>("Sprite");
        isGameOver = false;

        GenratedGrid();
        nextImage();

        SetupSound();
        SetupMusic();
        SetupVibration();

        if (PlayerPrefs.GetInt("FirstOpen", 0) == 0)
        {
            Debug.Log("AAAAA");
            PlayerPrefs.SetInt("Update", AdManager.Instance.Number);
            PlayerPrefs.SetInt("FirstOpen", 1);
            Debug.Log("Update" + PlayerPrefs.GetInt("Update"));
        }
    }

    public void ShowUpdateDialog()
    {
        Debug.Log(verionCode + " +++ " + AdManager.Instance.Number);
        if (verionCode < AdManager.Instance.Number)
        {
            PlayerPrefs.SetInt("Update", AdManager.Instance.Number);
            GamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
            UpdatePopUp.SetActive(true);
            isUpdate = true;
            VibrateBtn.GetComponent<Button>().interactable = false;
            First2Destroybtn.GetComponent<Button>().interactable = false;
            BomBtn.GetComponent<Button>().interactable = false;
            ChangeBtn.GetComponent<Button>().interactable = false;
            Helpbtn.GetComponent<Button>().interactable = false;
            SettingBtn.GetComponent<Button>().interactable = false;
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        verionCode = PlayerSettings.Android.bundleVersionCode;
        EditorUtility.SetDirty(this); // Mark the object as dirty to ensure the change is saved
#endif
    }

    void Update()
    {
        //Debug.Log(verionCode + " Vc");


        if (HowtoPlayPopup.activeInHierarchy)
        {
            timerIsRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HelpPanel.activeInHierarchy == true)
            {
                HelpPanel.SetActive(false);
                isHelp = false;
                timerIsRunning = true;
            }
            else
            {
                if (UpdatePopUp.activeInHierarchy == false)
                {
                    if (InterNetPopup.activeInHierarchy == false)
                    {
                        if (OverPanel.activeInHierarchy == false)
                        {
                            if (TimerPopup.activeInHierarchy == false)
                            {
                                timerIsRunning = false;
                                if (isLastExit == false)
                                {
                                    isExit = true;
                                    Debug.Log("Exit Panel Bool " + isLastExit);

                                    ExitPanel.SetActive(true);
                                    isLastExit = true;
                                    Debug.Log("Exit Panel Bool " + isLastExit);
                                }
                                else if (isLastExit == true)
                                {
                                    if (ExitPanel.activeInHierarchy == false)
                                    {
                                        Debug.Log("Setting Panel Bool " + isLastExit);
                                        SettingPanel.SetActive(false);
                                        StartCoroutine(TimeforBack());
                                        Debug.Log("Setting Panel Bool " + isLastExit);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (isNet == false)
            {
                isNet = true;
                timerIsRunning = false;
                isInternet = true;
                InterNetPopup.SetActive(true);
                AdManager.Instance.DestroyAd();
            }
        }
        else
        {
            if (isNet)
            {
                // Load ads only when the internet is reconnected
                StartCoroutine(AdManager.Instance.GetRequest(AdManager.Instance.UrlName));
                StartCoroutine(AdBanner());
                isNet = false; // Reset the flag after loading ads
                timeRemaining = 120f;
                timerIsRunning = true;
                Debug.Log("timeRemaining net on " + timeRemaining);
            }
        }


        if (TimerPopup.activeInHierarchy == true && isTime == false)
        {
            isTime = true;
        }

        if (AdManager.Instance.InterstitialAcc == 2 && AdManager.Instance.AdAvailablevalue == 1)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    UpdateTimerDisplay(timeRemaining);

                    if (AdManager.Instance.isinterstitialLoad == true)
                    {
                        if (timeRemaining <= 5f && !fiveSecondWarningGiven)
                        {
                            TimerPopup.SetActive(true);
                            VibrateBtn.GetComponent<Button>().interactable = false;
                            First2Destroybtn.GetComponent<Button>().interactable = false;
                            BomBtn.GetComponent<Button>().interactable = false;
                            ChangeBtn.GetComponent<Button>().interactable = false;
                            Helpbtn.GetComponent<Button>().interactable = false;
                            SettingBtn.GetComponent<Button>().interactable = false;
                            //Debug.Log("5 seconds remaining!");
                            Second5 = string.Format("{00}", seconds);
                            TimerTxt.text = Second5;
                        }
                    }
                }
                else
                {
                    if (OverPanel.activeInHierarchy == false && ExitPanel.activeInHierarchy == false)
                    {
                        timeRemaining = 0;
                        timerIsRunning = false;
                        fiveSecondWarningGiven = false;
                        AdManager.Instance.ShowInterstitialAd();
                        GamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                        TimerPopup.SetActive(false);
                        StartCoroutine(ChangeBoolForTimer());
                        // Timer has run out, you can trigger any event here
                        //Debug.Log("Time's up!");
                    }
                }
            }
        }

        for (int i = image.Count - 1; i >= 0; i--)
        {
            if (image[i] != null && !image[i].GetComponent<SpriteRenderer>().enabled)
            {
                StartCoroutine(DeletImage(image[i]));
            }
        }
    }


    public void HowToPlayPanelBtnClick()
    {
        HowtoPlayPopup.SetActive(false);
        PlayerPrefs.SetInt("HowToPlay", 1);
        timerIsRunning = true;
        StartCoroutine(ChangesBool());
    }

    public IEnumerator ChangesBool()
    {
        yield return new WaitForSeconds(0.1f);
        isHowToPlayTouch = false;
        isPanelStart = false;
    }


    void SetupSound()
    {
        var soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            bool isMuted = soundManager.SoundAudio.volume == 0;
            soundManager.isSound = isMuted;
            soundManager.SoundAudio.mute = isMuted;
            SoundButton.GetComponent<Image>().sprite = isMuted ? Off : On;
        }
    }

    void SetupMusic()
    {
        var musicManager = MusicManager.instnace;
        if (musicManager != null)
        {
            bool isMuted = musicManager.MusicAudio.volume == 0;
            musicManager.isMusic = isMuted;
            musicManager.MusicAudio.mute = isMuted;
            MusicButton.GetComponent<Image>().sprite = isMuted ? Off : On;
        }
    }

    void SetupVibration()
    {
        VibrateToggle.isOn = PlayerPrefs.GetInt("Vibrate", 0) == 0;
    }

    public void GenratedGrid()
    {
        int number = UnityEngine.Random.Range(0, Fruits.Length);
        GameObject fruit = Instantiate(Fruits[number], FruitsParent.transform.position, Quaternion.identity, FruitsParent.transform);
    }

    public void nextImage()
    {
        NextFruit = UnityEngine.Random.Range(0, NextImages.Length);
        NextImage.sprite = NextImages[NextFruit];
    }

    public void AfterNextImageCall()
    {
        Fruit = Instantiate(Fruits[NextFruit], FruitsParent.transform.position, Quaternion.identity, FruitsParent.transform);
    }

    public void HelpBtnClick()
    {
        HelpPanel.SetActive(true);
        isHelp = true;
        timerIsRunning = false;
    }

    public void HelpBackBtnClick()
    {
        HelpPanel.SetActive(false);
        isHelp = false;
        timerIsRunning = true;
    }

    public IEnumerator AdBanner()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Update Banner" + AdManager.Instance.BannerAcc + ":::" + AdManager.Instance.AdAvailablevalue);
        if (AdManager.Instance.BannerAcc == 2 && AdManager.Instance.AdAvailablevalue > 0)
        {
            Debug.Log("");
            AdManager.Instance.LoadAd();
            //BANER
        }
    }

    public void PrivacypolicyBtnClick()
    {
        Application.OpenURL("https://www.termsfeed.com/live/74303810-e2df-4b7c-b583-762038680d53");
    }

    public void OkBtnClick()
    {
        InterNetPopup.SetActive(false);
        isNet = true;
        isInternet = false;
        timerIsRunning = true;
    }

    IEnumerator ChangeBoolForTimer()
    {
        yield return new WaitForSeconds(0.2f);
        timeRemaining = 120f;
        timerIsRunning = true;
        isTime = false;
        isExit = false;
        VibrateBtn.GetComponent<Button>().interactable = true;
        First2Destroybtn.GetComponent<Button>().interactable = true;
        BomBtn.GetComponent<Button>().interactable = true;
        ChangeBtn.GetComponent<Button>().interactable = true;
        Helpbtn.GetComponent<Button>().interactable = true;
        SettingBtn.GetComponent<Button>().interactable = true;
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1; // To ensure the timer ends at 0

        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));

        Timer = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(Timer);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator DeletImage(GameObject deletimage)
    {
        yield return new WaitForSeconds(2);
        image.Remove(deletimage);
        Destroy(deletimage);
    }

    public void RestartBtnClick()
    {
        OverPanel.SetActive(false);
        ScoreValue = 0;
        ScoreText.text = "0";
        if (AdManager.Instance.InterstitialAcc == 2 && AdManager.Instance.AdAvailablevalue >= 1)
        {
            AdManager.Instance.ShowInterstitialAd();
        }
        SceneManager.LoadScene(1);
    }

    public void UpdatebtnClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.fruit.merge.world.games.drop.puzzle.game");
        PlayerPrefs.SetInt("Update", AdManager.Instance.Number);
        UpdatePopUp.SetActive(false);
        isUpdate = false;
        timerIsRunning = true;
        GamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        VibrateBtn.GetComponent<Button>().interactable = true;
        First2Destroybtn.GetComponent<Button>().interactable = true;
        BomBtn.GetComponent<Button>().interactable = true;
        ChangeBtn.GetComponent<Button>().interactable = true;
        Helpbtn.GetComponent<Button>().interactable = true;
        SettingBtn.GetComponent<Button>().interactable = true;
    }

    public void NotNowBtnClick()
    {
        UpdatePopUp.SetActive(false);
        isUpdate = false;
        timerIsRunning = true;
        GamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        VibrateBtn.GetComponent<Button>().interactable = true;
        First2Destroybtn.GetComponent<Button>().interactable = true;
        BomBtn.GetComponent<Button>().interactable = true;
        ChangeBtn.GetComponent<Button>().interactable = true;
        Helpbtn.GetComponent<Button>().interactable = true;
        SettingBtn.GetComponent<Button>().interactable = true;
    }

    public void SettingBtnClick()
    {
        isSetting = true;
        isLastExit = true;
        timerIsRunning = false;
        Debug.Log("Setting Panel Bool " + isLastExit);
        SettingPanel.SetActive(true);
    }

    public void BackBtnClick()
    {
        SettingPanel.SetActive(false);
        StartCoroutine(TimeforBack());
    }

    IEnumerator TimeforBack()
    {
        yield return new WaitForSeconds(0.1f);
        isSetting = false;
        isLastExit = false;
        timerIsRunning = true;
        Debug.Log("Setting Panel Bool " + isLastExit);
    }

    public void SoundBtnClick()
    {
        Debug.Log("Sound");
        var soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            soundManager.isSound = !soundManager.isSound;
            soundManager.SoundAudio.mute = soundManager.isSound;
            soundManager.SoundAudio.volume = soundManager.isSound ? 0 : 1;
            PlayerPrefs.SetInt("Sound", soundManager.isSound ? 0 : 1);
            SoundButton.GetComponent<Image>().sprite = soundManager.isSound ? Off : On;
        }
    }

    public void MusicBtnClick()
    {
        Debug.Log("Music");
        var musicManager = MusicManager.instnace;
        if (musicManager != null)
        {
            musicManager.isMusic = !musicManager.isMusic;
            musicManager.MusicAudio.mute = musicManager.isMusic;
            musicManager.MusicAudio.volume = musicManager.isMusic ? 0 : 1;
            PlayerPrefs.SetInt("Music", musicManager.isMusic ? 0 : 1);
            MusicButton.GetComponent<Image>().sprite = musicManager.isMusic ? Off : On;
        }
    }

    public void VibrateBtnClick()
    {
        Debug.Log("Vibrate");
        PlayerPrefs.SetInt("Vibrate", VibrateToggle.isOn ? 0 : 1);
        if (VibrateToggle.isOn)
        {
            Vibration.Vibrate(50);
        }
    }

    public void ShareButtonClick()
    {
        shareText.Share("Fruit Merge Game" + "\n" + "\n" + "Let me Recommend you this application" + "\n" + "\n" + "https://play.google.com/store/apps/details?id=com.fruit.merge.world.games.drop.puzzle.game");
    }

    public void BomButtonClick()
    {
        isChangeOneTime = false;
        timerIsRunning = false;
        if (image.Count == 0)
        {
            StartCoroutine(NotFoundTimeChanges());
            isButtonOption = false;
            isChangeOneTime = true;
            VibrateBtn.GetComponent<Button>().interactable = true;
            First2Destroybtn.GetComponent<Button>().interactable = true;
            BomBtn.GetComponent<Button>().interactable = true;
            ChangeBtn.GetComponent<Button>().interactable = true;
            Helpbtn.GetComponent<Button>().interactable = true;
            SettingBtn.GetComponent<Button>().interactable = true;
            Debug.Log("isButtonOption" + isButtonOption);
            timerIsRunning = true;
        }
        else
        {
            if (AdManager.Instance.isRewardShow)
            {
                AdManager.Instance.ShowRewardedAd();

                StartCoroutine(ChangeBomTime());
            }
            else
            {
                //Profiler.BeginSample("AdTimeChanges");
                StartCoroutine(AdTimeChanges());
                // Profiler.EndSample();
            }
        }
    }

    public IEnumerator ChangeBomTime()
    {
        VibrateBtn.GetComponent<Button>().interactable = false;
        First2Destroybtn.GetComponent<Button>().interactable = false;
        BomBtn.GetComponent<Button>().interactable = false;
        ChangeBtn.GetComponent<Button>().interactable = false;
        Helpbtn.GetComponent<Button>().interactable = false;
        SettingBtn.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.4f);
        isButtonOption = true;
        Debug.Log("isButtonOption" + isButtonOption);

        foreach (var item in image)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            item.gameObject.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.Fast).SetLoops(-2, LoopType.Incremental);
        }
    }

    public IEnumerator AdTimeChanges()
    {
        NotAd.transform.DOScale(new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1.3f);
        NotAd.transform.DOScale(new Vector3(0, 0, 0), 1);
        if (AdManager.Instance._rewardedAd == null)
        {
            if (AdManager.Instance.RewardAcc == 2 && AdManager.Instance.AdAvailablevalue >= 1)
            {
                AdManager.Instance.LoadRewardedAd();
            }
        }
        isBoxVibrate = false;
        isButtonFirst2Destroy = false;
        isButtonChange = false;
        isButtonOption = false;
        isChangeOneTime = true;
        VibrateBtn.GetComponent<Button>().interactable = true;
        First2Destroybtn.GetComponent<Button>().interactable = true;
        BomBtn.GetComponent<Button>().interactable = true;
        ChangeBtn.GetComponent<Button>().interactable = true;
        Helpbtn.GetComponent<Button>().interactable = true;
        SettingBtn.GetComponent<Button>().interactable = true;
        timerIsRunning = true;
    }

    public void ChangeButtonClick()
    {
        isChangeOneTime = false;
        timerIsRunning = false;

        if (image.Count == 0)
        {
            StartCoroutine(NotFoundTimeChanges());
            isButtonChange = false;
            isChangeOneTime = true;
            VibrateBtn.GetComponent<Button>().interactable = true;
            First2Destroybtn.GetComponent<Button>().interactable = true;
            BomBtn.GetComponent<Button>().interactable = true;
            ChangeBtn.GetComponent<Button>().interactable = true;
            Helpbtn.GetComponent<Button>().interactable = true;
            SettingBtn.GetComponent<Button>().interactable = true;
            Debug.Log("isButtonChange" + isButtonChange);
            timerIsRunning = true;
        }
        else
        {
            Debug.Log("Click With Bool Click ChangeBtn" + isChangeOneTime);
            if (AdManager.Instance.isRewardShow)
            {
                AdManager.Instance.ShowRewardedAd();
                StartCoroutine(ChangeTimeForChanges());
            }
            else
            {
                // Profiler.BeginSample("AdTimeChanges");
                StartCoroutine(AdTimeChanges());
                // Profiler.EndSample();
            }
        }
    }

    public IEnumerator ChangeTimeForChanges()
    {
        VibrateBtn.GetComponent<Button>().interactable = false;
        First2Destroybtn.GetComponent<Button>().interactable = false;
        BomBtn.GetComponent<Button>().interactable = false;
        ChangeBtn.GetComponent<Button>().interactable = false;
        Helpbtn.GetComponent<Button>().interactable = false;
        SettingBtn.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.4f);
        isButtonChange = true;
        Debug.Log("isButtonChange" + isButtonChange);

        foreach (var item in image)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            item.gameObject.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.Fast).SetLoops(-2, LoopType.Incremental);
        }
    }

    public void BoxVibrateButtonClick()
    {
        timerIsRunning = false;
        if (isBoxVibrate == false)
        {
            isBoxVibrate = true;

            if (image.Count == 0)
            {
                isBoxVibrate = false;
                isButtonBoxVibrate = false;
                VibrateBtn.GetComponent<Button>().interactable = true;
                First2Destroybtn.GetComponent<Button>().interactable = true;
                BomBtn.GetComponent<Button>().interactable = true;
                ChangeBtn.GetComponent<Button>().interactable = true;
                Helpbtn.GetComponent<Button>().interactable = true;
                SettingBtn.GetComponent<Button>().interactable = true;
                timerIsRunning = true;
                StartCoroutine(NotFoundTimeChanges());
            }
            else
            {
                Debug.Log("isBoxVibrate" + isBoxVibrate);
                if (AdManager.Instance.isRewardShow)
                {
                    AdManager.Instance.ShowRewardedAd();
                    StartCoroutine(BoxVibrateChangeTime());
                }
                else
                {
                    // Profiler.BeginSample("AdTimeChanges");
                    StartCoroutine(AdTimeChanges());
                    // Profiler.EndSample();
                }
            }
        }
    }

    public IEnumerator BoxVibrateChangeTime()
    {
        VibrateBtn.GetComponent<Button>().interactable = false;
        First2Destroybtn.GetComponent<Button>().interactable = false;
        BomBtn.GetComponent<Button>().interactable = false;
        ChangeBtn.GetComponent<Button>().interactable = false;
        Helpbtn.GetComponent<Button>().interactable = false;
        SettingBtn.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(0.4f);

        GameOverObject1.SetActive(false);
        GameOverObject2.SetActive(false);
        GameOverObject3.SetActive(false);
        isButtonBoxVibrate = true;
        Debug.Log("BoxVibrate");
        main.DOOrthoSize(7, 0.5f);
        BoxRotate();
    }

    public void BoxRotate()
    {
        StartCoroutine(WobbleObject());
    }

    IEnumerator WobbleObject()
    {
        yield return new WaitForSeconds(.5f);
        ColliderObject.SetActive(true);
        MoveDown();
        yield return new WaitForSeconds(1f);
        MoveLeftRight();
        //Box.transform.DOShakePosition(1, new Vector3(1, 0, 0));
        // Create a sequence for the wobble animation
        Sequence wobbleSequence = DOTween.Sequence();

        // Add a rotation to the positive angle on the Z-axis
        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, wobbleAngle), wobbleDuration)
            .SetEase(Ease.InOutSine));

        // Add a rotation back to the negative angle on the Z-axis
        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, -wobbleAngle), wobbleDuration)
            .SetEase(Ease.InOutSine));

        // Add a rotation back to the starting position
        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, 0), wobbleDuration)
            .SetEase(Ease.InOutSine));

        // Set the sequence to loop indefinitely
        wobbleSequence.SetLoops(4, LoopType.Restart);

        yield return new WaitForSeconds(1);
        Box.transform.eulerAngles = Vector3.zero;
        ColliderObject.SetActive(false);
        isButtonBoxVibrate = false;
        main.DOOrthoSize(6, 0.5f);
        ColliderObject.transform.DOMoveY(-4.5f, 1);
        //Profiler.BeginSample("Change");
        StartCoroutine(Change());
        //Profiler.EndSample();
    }

    public IEnumerator Change()
    {
        yield return new WaitForSeconds(2f);
        GameOverObject1.SetActive(true);
        GameOverObject2.SetActive(true);
        GameOverObject3.SetActive(true);
        isBoxVibrate = false;
        timerIsRunning = true;
        VibrateBtn.GetComponent<Button>().interactable = true;
        First2Destroybtn.GetComponent<Button>().interactable = true;
        BomBtn.GetComponent<Button>().interactable = true;
        ChangeBtn.GetComponent<Button>().interactable = true;
        Helpbtn.GetComponent<Button>().interactable = true;
        SettingBtn.GetComponent<Button>().interactable = true;
    }

    public void MoveDown()
    {
        ColliderObject.transform.DOMoveY(-3.3f, 1);
    }

    public void MoveLeftRight()
    {
        // Create a sequence for the left-right movement
        Sequence leftRightSequence = DOTween.Sequence();

        // Move to the right
        leftRightSequence.Append(ColliderObject.transform.DOMoveX(ColliderObject.transform.position.x + distance, duration)
            .SetEase(Ease.InOutSine));

        // Move to the left
        leftRightSequence.Append(ColliderObject.transform.DOMoveX(ColliderObject.transform.position.x - distance, duration)
            .SetEase(Ease.InOutSine));

        // Set the sequence to loop indefinitely
        leftRightSequence.SetLoops(2, LoopType.Yoyo);

        // Play the sequence
        leftRightSequence.Play();
    }

    public void First2DestroyButtonCLick()
    {
        timerIsRunning = false;
        isButtonFirst2Destroy = true;
        isoff = false;

        if (image.Count == 0)
        {
            StartCoroutine(NotFoundTimeChanges());
            isButtonFirst2Destroy = false;
            VibrateBtn.GetComponent<Button>().interactable = true;
            First2Destroybtn.GetComponent<Button>().interactable = true;
            BomBtn.GetComponent<Button>().interactable = true;
            ChangeBtn.GetComponent<Button>().interactable = true;
            Helpbtn.GetComponent<Button>().interactable = true;
            SettingBtn.GetComponent<Button>().interactable = true;
            timerIsRunning = true;
            Debug.Log("isButtonFirst2Destroy" + isButtonFirst2Destroy);
        }
        else
        {
            if (AdManager.Instance.isRewardShow)
            {
                AdManager.Instance.ShowRewardedAd();
                StartCoroutine(ChangesTimeForFirst2Object());
            }
            else
            {
                // Profiler.BeginSample("AdTimeChanges");
                StartCoroutine(AdTimeChanges());
                // Profiler.EndSample();
            }
        }
    }

    public IEnumerator ChangesTimeForFirst2Object()
    {
        VibrateBtn.GetComponent<Button>().interactable = false;
        First2Destroybtn.GetComponent<Button>().interactable = false;
        BomBtn.GetComponent<Button>().interactable = false;
        ChangeBtn.GetComponent<Button>().interactable = false;
        Helpbtn.GetComponent<Button>().interactable = false;
        SettingBtn.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(0.4f);
        Debug.Log("isButtonFirst2Destroy" + isButtonFirst2Destroy);
        StartCoroutine(ResetFruits());
    }

    public IEnumerator NotFoundTimeChanges()
    {
        NotFound.transform.DOScale(new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1.3f);
        NotFound.transform.DOScale(new Vector3(0, 0, 0), 1);
    }

    public IEnumerator ResetFruits()
    {
        Debug.Log("First2Destroy");

        if (isButtonFirst2Destroy == true && isButtonOption == false && isButtonChange == false && isButtonBoxVibrate == false)
        {
            for (int i = 0; i < image.Count; i++)
            {
                if (image[i].gameObject.CompareTag("Strawberry") || image[i].gameObject.CompareTag("Apricot"))
                {
                    First2Object.Add(image[i].gameObject);
                }

            }

            if (First2Object.Count == 0)
            {
                //Profiler.BeginSample("NotFoundTimeChanges");
                StartCoroutine(NotFoundTimeChanges());
                // Profiler.EndSample();
            }


            foreach (var itemObject in First2Object)
            {
                ParticleSystem particleSystem = Instantiate(particle);
                particleSystem.transform.SetParent(itemObject.gameObject.transform);
                particleSystem.transform.position = itemObject.transform.position;

                Destroy(itemObject, 0.2f);
            }
            Debug.Log(image.Count);


            image.Clear();
            First2Object.Clear();

            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < FruitsParent.transform.childCount; i++)
            {
                Rigidbody2D rb = FruitsParent.transform.GetChild(i).GetComponent<Rigidbody2D>();

                if (rb.isKinematic == false)
                {
                    image.Add(FruitsParent.transform.GetChild(i).gameObject);
                }
            }

            isButtonFirst2Destroy = false;
            VibrateBtn.GetComponent<Button>().interactable = true;
            First2Destroybtn.GetComponent<Button>().interactable = true;
            BomBtn.GetComponent<Button>().interactable = true;
            ChangeBtn.GetComponent<Button>().interactable = true;
            Helpbtn.GetComponent<Button>().interactable = true;
            SettingBtn.GetComponent<Button>().interactable = true;
            timerIsRunning = true;
        }
    }

    //public void GameOver()
    //{
    //    GamePanel.SetActive(false);

    //    OverPanel.SetActive(true);
    //    ScoreValueOver.text = ScoreValue.ToString();

    //    if (ScoreValue > HighScore)
    //    {
    //        HighScore = ScoreValue;
    //        HighScoreText.text = HighScore.ToString();
    //        PlayerPrefs.SetInt("HighScore", HighScore);
    //    }

    //    AdManager.Instance.ShowInterstitialAd();
    //}

    public IEnumerator IsFruit()
    {
        yield return new WaitForSeconds(0.3f);
        isFruit = false;
    }

    public void ExitYesBtnClick()
    {
        isExit = false;
        isLastExit = false;
        Debug.Log("Exit Panel Bool " + isLastExit);
        Application.Quit();
    }

    public void ExitNoBtnClick()
    {
        isExit = false;
        isLastExit = false;
        ExitPanel.SetActive(false);
        Debug.Log("Exit Panel Bool " + isLastExit);
        timerIsRunning = true;
    }
}





//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;
//using UnityEngine.Profiling;
//using DG.Tweening;

//public class GameManager : MonoBehaviour
//{
//    public GameObject[] Fruits;

//    public GameObject FruitsParent;

//    public GameObject ParentObj;

//    public List<GameObject> image = new List<GameObject>();

//    public bool isFruit = false;

//    public TextMeshProUGUI ScoreText;

//    public int ScoreValue;

//    public TextMeshProUGUI HighScoreText;

//    public TextMeshProUGUI ScoreValueOver;

//    public int HighScore;

//    public GameObject GameOverObject1, GameOverObject2, GameOverObject3;

//    public GameObject GamePanel, OverPanel, SettingPanel;

//    public bool isGameOver = false;

//    public Sprite[] NextImages;

//    public Image NextImage;

//    public Toggle VibrateToggle;

//    int NextFruit;

//    public Sprite On, Off;

//    public Button SoundButton, MusicButton;

//    public List<GameObject> First2Object = new List<GameObject>();

//    public bool isButtonOption = false;

//    public bool isButtonChange = false;

//    public bool isButtonBoxVibrate = false;

//    public bool isButtonFirst2Destroy = false;

//    public GameObject Box;

//    public float rotateSPeed;

//    public GameObject ColliderObject;

//    public Camera main;

//    public GameObject NotAd;

//    public GameObject NotFound;

//    public ParticleSystem particle;

//    public ShareText shareText;

//    public bool isBom = false;

//    public static GameManager instance;

//    public void Awake()
//    {
//        Application.targetFrameRate = 60;
//        instance = this;

//        if (PlayerPrefs.HasKey("Score") == false)
//        {
//            PlayerPrefs.SetInt("Score", 0);
//        }
//        else
//        {
//            ScoreValue = PlayerPrefs.GetInt("Score");
//        }

//        if (PlayerPrefs.HasKey("HighScore") == false)
//        {
//            PlayerPrefs.SetInt("HighScore", 0);
//        }
//        else
//        {
//            HighScore = PlayerPrefs.GetInt("HighScore");
//        }

//        ScoreText.text = ScoreValue.ToString();
//        Debug.Log("Score" + ScoreValue.ToString());
//        HighScoreText.text = HighScore.ToString();
//        Debug.Log("HighScore" + HighScore.ToString());
//    }

//    public void Start()
//    {
//        Fruits = Resources.LoadAll<GameObject>("Prefabs");
//        NextImages = Resources.LoadAll<Sprite>("Sprite");
//        isGameOver = false;

//        Profiler.BeginSample("GenratedGrid");
//        GenratedGrid();
//        Profiler.EndSample();

//        Profiler.BeginSample("nextImage");
//        nextImage();
//        Profiler.EndSample();

//        if (SoundManager.Instance != null)
//        {
//            if (SoundManager.Instance.SoundAudio.volume == 0)
//            {
//                SoundManager.Instance.isSound = true;
//                SoundManager.Instance.SoundAudio.mute = true;
//                SoundButton.GetComponent<Image>().sprite = Off;
//            }
//            else if (SoundManager.Instance.SoundAudio.volume == 1)
//            {
//                SoundManager.Instance.isSound = false;
//                SoundManager.Instance.SoundAudio.mute = false;
//                SoundButton.GetComponent<Image>().sprite = On;
//            }
//        }

//        if (MusicManager.instnace != null)
//        {
//            if (MusicManager.instnace.MusicAudio.volume == 0)
//            {
//                MusicManager.instnace.isMusic = true;
//                MusicManager.instnace.MusicAudio.mute = true;
//                MusicButton.GetComponent<Image>().sprite = Off;
//            }
//            else if (MusicManager.instnace.MusicAudio.volume == 1)
//            {
//                MusicManager.instnace.isMusic = false;
//                MusicManager.instnace.MusicAudio.mute = false;
//                MusicButton.GetComponent<Image>().sprite = On;
//            }
//        }


//        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
//        {
//            VibrateToggle.isOn = true;
//        }
//        else if (PlayerPrefs.GetInt("Vibrate", 0) == 1)
//        {
//            VibrateToggle.isOn = false;
//        }
//    }


//    public void GenratedGrid()
//    {
//        int Number = UnityEngine.Random.Range(0, Fruits.Length);
//        // Debug.Log(Number);
//        GameObject fruit = Instantiate(Fruits[Number]);
//        fruit.transform.SetParent(FruitsParent.transform);
//        fruit.transform.position = FruitsParent.transform.position;
//    }

//    public void nextImage()
//    {
//        NextFruit = UnityEngine.Random.Range(0, NextImages.Length);
//        NextImage.sprite = NextImages[NextFruit];
//    }

//    public void AfterNextImageCall()
//    {
//        GameObject fruit = Instantiate(Fruits[NextFruit]);
//        fruit.transform.SetParent(FruitsParent.transform);
//        fruit.transform.position = FruitsParent.transform.position;
//    }

//    public void Update()
//    {
//        foreach (var item in image)
//        {
//            if (item != null)
//            {
//                if (item.gameObject.GetComponent<SpriteRenderer>().enabled == false)
//                {
//                    Profiler.BeginSample("DeletImage");
//                    StartCoroutine(DeletImage(item));
//                    Profiler.EndSample();
//                }
//            }
//        }
//    }

//    public IEnumerator DeletImage(GameObject deletimage)
//    {
//        yield return new WaitForSeconds(2);
//        Destroy(deletimage);
//        image.Remove(deletimage);
//    }

//    public IEnumerator IsFruit()
//    {
//        yield return new WaitForSeconds(0.3f);
//        isFruit = false;
//    }

//    public void RestartBtnClick()
//    {
//        OverPanel.SetActive(false);
//        ScoreValue = 0;
//        ScoreText.text = "0";
//        SceneManager.LoadScene(1);
//        AdManager.Instance.isShow = false;
//    }

//    public void SettingBtnClick()
//    {
//        Debug.Log("Setting");
//        isGameOver = true;
//        SettingPanel.SetActive(true);
//    }

//    public void BackBtnClick()
//    {
//        Debug.Log("Back");
//        SettingPanel.SetActive(false);
//        Profiler.BeginSample("TimeforBack");
//        StartCoroutine(TimeforBack());
//        Profiler.EndSample();
//    }

//    public IEnumerator TimeforBack()
//    {
//        yield return new WaitForSeconds(0.1f);
//        isGameOver = false;
//    }

//    public void SoundBtnClick()
//    {
//        Debug.Log("Sound");
//        if (SoundManager.Instance.isSound == false)
//        {
//            SoundManager.Instance.isSound = true;
//            PlayerPrefs.SetInt("Sound", 0);
//            SoundManager.Instance.SoundAudio.mute = true;
//            SoundManager.Instance.SoundAudio.volume = 0;
//            SoundButton.GetComponent<Image>().sprite = Off;
//            Debug.Log("Sound_Off");
//        }
//        else if (SoundManager.Instance.isSound == true)
//        {
//            SoundManager.Instance.isSound = false;
//            PlayerPrefs.SetInt("Sound", 1);
//            SoundManager.Instance.SoundAudio.mute = false;
//            SoundManager.Instance.SoundAudio.volume = 1;
//            SoundButton.GetComponent<Image>().sprite = On;
//            Debug.Log("Sound_On");
//        }
//    }

//    public void MusicBtnClick()
//    {
//        Debug.Log("Music");

//        if (MusicManager.instnace.isMusic == false)
//        {
//            MusicManager.instnace.isMusic = true;
//            PlayerPrefs.SetInt("Music", 0);
//            MusicManager.instnace.MusicAudio.mute = true;
//            MusicManager.instnace.MusicAudio.volume = 0;
//            MusicButton.GetComponent<Image>().sprite = Off;
//            Debug.Log("Music_Off");
//        }
//        else
//        {
//            MusicManager.instnace.isMusic = false;
//            PlayerPrefs.SetInt("Music", 1);
//            MusicManager.instnace.MusicAudio.mute = false;
//            MusicManager.instnace.MusicAudio.volume = 1;
//            MusicButton.GetComponent<Image>().sprite = On;
//            Debug.Log("Music_On");
//        }
//    }

//    public void VibrateBtnClick()
//    {

//        if (VibrateToggle.isOn == true)
//        {
//            Debug.Log("isOn");
//            Vibration.Vibrate(50);
//            PlayerPrefs.SetInt("Vibrate", 0);
//        }
//        else if (VibrateToggle.isOn == false)
//        {
//            Debug.Log("isOff");
//            PlayerPrefs.SetInt("Vibrate", 1);
//        }
//    }

//    public void ShareButtonClick()
//    {
//        shareText.Share("This is the text I want to share!");
//    }

//    public void BomButtonClick()
//    {
//        if (AdManager.Instance.isRewardShow)
//        {
//            AdManager.Instance.ShowRewardedAd();

//            StartCoroutine(ChangeBomTime());
//        }
//        else
//        {
//            Profiler.BeginSample("AdTimeChanges");
//            StartCoroutine(AdTimeChanges());
//            Profiler.EndSample();
//        }
//    }

//    public IEnumerator ChangeBomTime()
//    {
//        yield return new WaitForSeconds(0.4f);
//        isButtonOption = true;
//        Debug.Log("Bom");
//        foreach (var item in image)
//        {
//            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//            item.gameObject.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, 0, 180), 1, RotateMode.Fast).SetLoops(-1, LoopType.Incremental);
//        }
//    }

//    public IEnumerator AdTimeChanges()
//    {
//        NotAd.transform.DOScale(new Vector3(1, 1, 1), 1);
//        yield return new WaitForSeconds(1.3f);
//        NotAd.transform.DOScale(new Vector3(0, 0, 0), 1);
//    }

//    public void ChangeButtonClick()
//    {
//        if (AdManager.Instance.isRewardShow)
//        {
//            AdManager.Instance.ShowRewardedAd();
//            StartCoroutine(ChangeTimeForChanges());
//        }
//        else
//        {
//            Profiler.BeginSample("AdTimeChanges");
//            StartCoroutine(AdTimeChanges());
//            Profiler.EndSample();
//        }
//    }

//    public IEnumerator ChangeTimeForChanges()
//    {
//        yield return new WaitForSeconds(0.4f);
//        isButtonChange = true;
//        Debug.Log("Changes");
//        foreach (var item in image)
//        {
//            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//        }
//    }

//    public void BoxVibrateButtonClick()
//    {
//        if (AdManager.Instance.isRewardShow)
//        {
//            AdManager.Instance.ShowRewardedAd();
//            StartCoroutine(BoxVibrateChangeTime());
//        }
//        else
//        {
//            Profiler.BeginSample("AdTimeChanges");
//            StartCoroutine(AdTimeChanges());
//            Profiler.EndSample();
//        }
//    }

//    public IEnumerator BoxVibrateChangeTime()
//    {
//        yield return new WaitForSeconds(0.4f);
//        GameOverObject1.SetActive(false);
//        GameOverObject2.SetActive(false);
//        GameOverObject3.SetActive(false);
//        isButtonBoxVibrate = true;
//        Debug.Log("BoxVibrate");
//        main.DOOrthoSize(7, 0.5f);
//        BoxRotate();
//    }

//    public void BoxRotate()
//    {
//        Profiler.BeginSample("WobbleObject");
//        StartCoroutine(WobbleObject());
//        Profiler.EndSample();
//    }

//    public float wobbleDuration = 0.1f;
//    public float wobbleAngle = 5f;

//    public float distance = 1.5f;  // Distance to move left and right
//    public float duration = 1f;

//    IEnumerator WobbleObject()
//    {
//        yield return new WaitForSeconds(.5f);
//        ColliderObject.SetActive(true);
//        MoveDown();
//        yield return new WaitForSeconds(1f);
//        MoveLeftRight();
//        //Box.transform.DOShakePosition(1, new Vector3(1, 0, 0));
//        // Create a sequence for the wobble animation
//        Sequence wobbleSequence = DOTween.Sequence();

//        // Add a rotation to the positive angle on the Z-axis
//        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, wobbleAngle), wobbleDuration)
//            .SetEase(Ease.InOutSine));

//        // Add a rotation back to the negative angle on the Z-axis
//        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, -wobbleAngle), wobbleDuration)
//            .SetEase(Ease.InOutSine));

//        // Add a rotation back to the starting position
//        wobbleSequence.Append(Box.transform.DORotate(new Vector3(0, 0, 0), wobbleDuration)
//            .SetEase(Ease.InOutSine));

//        // Set the sequence to loop indefinitely
//        wobbleSequence.SetLoops(4, LoopType.Restart);

//        yield return new WaitForSeconds(1);
//        Box.transform.eulerAngles = Vector3.zero;
//        ColliderObject.SetActive(false);
//        isButtonBoxVibrate = false;
//        main.DOOrthoSize(6, 0.5f);
//        ColliderObject.transform.DOMoveY(-4.5f, 1);
//        Profiler.BeginSample("Change");
//        StartCoroutine(Change());
//        Profiler.EndSample();
//    }

//    public IEnumerator Change()
//    {
//        yield return new WaitForSeconds(2);
//        GameOverObject1.SetActive(true);
//        GameOverObject2.SetActive(true);
//        GameOverObject3.SetActive(true);
//    }

//    public void MoveDown()
//    {
//        ColliderObject.transform.DOMoveY(-3.3f, 1);
//    }

//    public void MoveLeftRight()
//    {
//        // Create a sequence for the left-right movement
//        Sequence leftRightSequence = DOTween.Sequence();

//        // Move to the right
//        leftRightSequence.Append(ColliderObject.transform.DOMoveX(ColliderObject.transform.position.x + distance, duration)
//            .SetEase(Ease.InOutSine));

//        // Move to the left
//        leftRightSequence.Append(ColliderObject.transform.DOMoveX(ColliderObject.transform.position.x - distance, duration)
//            .SetEase(Ease.InOutSine));

//        // Set the sequence to loop indefinitely
//        leftRightSequence.SetLoops(2, LoopType.Yoyo);

//        // Play the sequence
//        leftRightSequence.Play();
//    }

//    public void First2DestroyButtonCLick()
//    {
//        isoff = false;
//        if (AdManager.Instance.isRewardShow)
//        {
//            AdManager.Instance.ShowRewardedAd();
//            StartCoroutine(ChangesTimeForFirst2Object());
//        }
//        else
//        {
//            Profiler.BeginSample("AdTimeChanges");
//            StartCoroutine(AdTimeChanges());
//            Profiler.EndSample();
//        }
//    }

//    public IEnumerator ChangesTimeForFirst2Object()
//    {
//        yield return new WaitForSeconds(0.4f);
//        isButtonFirst2Destroy = true;
//        Profiler.BeginSample("ResetFruits");
//        StartCoroutine(ResetFruits());
//        Profiler.EndSample();
//    }

//    public IEnumerator NotFoundTimeChanges()
//    {
//        NotFound.transform.DOScale(new Vector3(1, 1, 1), 1);
//        yield return new WaitForSeconds(1.3f);
//        NotFound.transform.DOScale(new Vector3(0, 0, 0), 1);
//    }

//    public bool isoff = false;

//    public IEnumerator ResetFruits()
//    {
//        Debug.Log("First2Destroy");

//        if (isButtonFirst2Destroy == true && isButtonOption == false && isButtonChange == false && isButtonBoxVibrate == false)
//        {
//            for (int i = 0; i < image.Count; i++)
//            {
//                if (image[i].gameObject.CompareTag("Strawberry") || image[i].gameObject.CompareTag("Apricot"))
//                {
//                    First2Object.Add(image[i].gameObject);
//                }

//            }

//            if (First2Object.Count == 0)
//            {
//                Profiler.BeginSample("NotFoundTimeChanges");
//                StartCoroutine(NotFoundTimeChanges());
//                Profiler.EndSample();
//            }


//            foreach (var itemObject in First2Object)
//            {
//                ParticleSystem particleSystem = Instantiate(particle);
//                particleSystem.transform.SetParent(itemObject.gameObject.transform);
//                particleSystem.transform.position = itemObject.transform.position;

//                Destroy(itemObject, 0.2f);
//            }
//            Debug.Log(image.Count);


//            image.Clear();
//            First2Object.Clear();

//            yield return new WaitForSeconds(0.2f);

//            for (int i = 0; i < FruitsParent.transform.childCount; i++)
//            {
//                Rigidbody2D rb = FruitsParent.transform.GetChild(i).GetComponent<Rigidbody2D>();

//                if (rb.isKinematic == false)
//                {
//                    isButtonFirst2Destroy = false;
//                    image.Add(FruitsParent.transform.GetChild(i).gameObject);
//                }
//            }
//        }
//    }
//}