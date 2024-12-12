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
    [Header("All FruitPrefab")]
    public GameObject[] fruits;

    public GameObject[] allFruit;


    [Header("All NextFruitImage")]
    public Sprite[] nextImages;


    [Header("All GameObject")]
    public GameObject fruitsParent;

    public GameObject parentObj;

    public GameObject gameOverObject1, gameOverObject2, gameOverObject3;

    public GameObject gamePanel, overPanel, settingPanel, helpPanel, exitPanel;

    public GameObject box;

    public GameObject colliderObject;

    public GameObject notAd;

    public GameObject notFound;

    public GameObject updatePopUp;

    public GameObject timerPopup;

    public GameObject interNetPopup;

    public GameObject howtoPlayPopup;

    GameObject fruit;

    [Header("All Numbers")]
    public int highScore;

    int nextFruit;

    public int scoreValue;

    public float rotateSPeed;

    public float wobbleBoxDuration = 0.1f;

    public float wobbleBoxAngle = 5f;

    public float distanceBox = 1.5f;

    public float durationBox = 0.5f;

    public float timeCountRemaining = 0; // 300 seconds = 5 minutes

    float minutesC;

    float secondsC;

    public int verionCode;  ///VERSION CODE CHAGE HERE  api ma change kre tyare aama same number vdharvo


    [Header("All Strings")]
    public string second5;

    public string timer;


    [Header("All Boolean")]
    public bool isTimerRunning = false;

    private bool isFiveSecondWarningGiven = false;

    public bool isTimeCount = false;

    public bool isAppOpenAdChange = false;

    public bool isOff = false;

    public bool isGameOverCheck = false;

    public bool isBomOption = false;

    public bool isButtonReplce = false;

    public bool isButtonBoxVibrateOption = false;

    public bool isButtonFirst2ObjectDestroy = false;

    public bool isHelpOption = false;

    public bool isFruitObject = false;

    public bool isNetCheck = false;

    public bool isBoxVibrateCheck = false;

    public bool isSettingPanelOff = false;

    public bool isExitPanelCheck = false;

    public bool isLastExitCheck = false;

    public bool isExitCheck = false;

    public bool isChangeOneTimeCheck = false;

    public bool isHowToPlayTouchCheck = false;

    public bool isPanelStartCheck = false;

    public bool isSettingCheck = false;

    public bool isInternetCheck = false;

    public bool isUpdateCheck = false;


    [Header("All Sprite Of Settings")]
    public Image nextImage;

    public Sprite on, off;


    [Header("All Buttons")]
    public Button soundButton;

    public Button musicButton;

    public Button bomButton, changeButton, boxVibrateButton, first2Destroybutton, helpbutton, settingButton;


    [Header("Extra")]
    public Toggle vibrateToggle;

    public Camera mainCamera;

    public ParticleSystem particleEffect;

    public ShareText shareTextObject;


    [Header("All Text")]
    public TextMeshProUGUI timerTxt;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI scoreValueOver;


    [Header("All List")]
    public List<GameObject> first2Object = new List<GameObject>();

    public List<GameObject> imageFruit = new List<GameObject>();

    public static GameManager instance;


    void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

        scoreValue = PlayerPrefs.GetInt("Score", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        scoreText.text = scoreValue.ToString();
        highScoreText.text = highScore.ToString();
    }

    void Start()
    {

        if (PlayerPrefs.GetInt("HowToPlay") == 0)
        {
            howtoPlayPopup.SetActive(true);
            isHowToPlayTouchCheck = true;
            isPanelStartCheck = true;
        }
        else if (PlayerPrefs.GetInt("HowToPlay") == 1)
        {
            howtoPlayPopup.SetActive(false);
            isHowToPlayTouchCheck = false;
            isPanelStartCheck = false;
        }

        isChangeOneTimeCheck = true;

        AdManager.Instance.isShowBool = false;

        if (updatePopUp.activeInHierarchy)
        {
            isTimerRunning = false;
        }
        else
        {
            isTimerRunning = true;
        }

        if (AdManager.Instance.bannerAcc == 2 && AdManager.Instance.adAvailablevalue == 1)
        {
            AdManager.Instance.LoadBannerAd();
            //BANER
        }

        if (AdManager.Instance._InterstitialAd == null)
        {
            if (AdManager.Instance.interstitialAcc == 2 && AdManager.Instance.adAvailablevalue == 1)
            {
                AdManager.Instance.LoadInterstitialsAd();
            }
        }

        if (AdManager.Instance._RewardedAd == null)
        {
            if (AdManager.Instance.rewardAcc == 2 && AdManager.Instance.adAvailablevalue == 1)
            {
                AdManager.Instance.LoadRewardedsAd();
            }
        }

        fruits = Resources.LoadAll<GameObject>("Prefabs");
        nextImages = Resources.LoadAll<Sprite>("Sprite");
        isGameOverCheck = false;

        GenratedFruit();
        NextFruitImageCall();

        SetUpSound();
        SetUpMusic();
        SetUpVibration();

        if (PlayerPrefs.GetInt("FirstOpen", 0) == 0)
        {
            PlayerPrefs.SetInt("Update", AdManager.Instance.number);
            PlayerPrefs.SetInt("FirstOpen", 1);
        }


        Debug.Log(isTimerRunning + " isTimerRunning");
        Debug.Log(isFiveSecondWarningGiven + " isFiveSecondWarningGiven");
        Debug.Log(isTimeCount + " isTimeCount");
        Debug.Log(isAppOpenAdChange + " isAppOpenAdChange");
        Debug.Log(isOff + " isOff");
        Debug.Log(isGameOverCheck + " isGameOverCheck");
        Debug.Log(isBomOption + " isBomOption");
        Debug.Log(isButtonReplce + " isButtonReplce");
        Debug.Log(isButtonBoxVibrateOption + " isButtonBoxVibrateOption");
        Debug.Log(isButtonFirst2ObjectDestroy + " isButtonFirst2ObjectDestroy");
        Debug.Log(isHelpOption + " isHelpOption");
        Debug.Log(isFruitObject + " isFruitObject");
        Debug.Log(isNetCheck + " isNetCheck");
        Debug.Log(isBoxVibrateCheck + " isBoxVibrateCheck");
        Debug.Log(isSettingPanelOff + " isSettingPanelOff");
        Debug.Log(isExitPanelCheck + " isExitPanelCheck");
        Debug.Log(isLastExitCheck + " isLastExitCheck");
        Debug.Log(isExitCheck + " isExitCheck");
        Debug.Log(isChangeOneTimeCheck + " isChangeOneTimeCheck");
        Debug.Log(isHowToPlayTouchCheck + " isHowToPlayTouchCheck");
        Debug.Log(isPanelStartCheck + " isPanelStartCheck");
        Debug.Log(isSettingCheck + " isSettingCheck");
        Debug.Log(isInternetCheck + " isInternetCheck");
        Debug.Log(isUpdateCheck + " isUpdateCheck");

    }

    public void OpenUpdateDialog()
    {
        if (verionCode < AdManager.Instance.number)
        {
            PlayerPrefs.SetInt("Update", AdManager.Instance.number);
            gamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
            updatePopUp.SetActive(true);
            isUpdateCheck = true;
            AllBtnFalse();
        }
    }

    public void AllBtnTrue()
    {
        boxVibrateButton.GetComponent<Button>().interactable = true;
        first2Destroybutton.GetComponent<Button>().interactable = true;
        bomButton.GetComponent<Button>().interactable = true;
        changeButton.GetComponent<Button>().interactable = true;
        helpbutton.GetComponent<Button>().interactable = true;
        settingButton.GetComponent<Button>().interactable = true;
    }

    public void AllBtnFalse()
    {
        boxVibrateButton.GetComponent<Button>().interactable = false;
        first2Destroybutton.GetComponent<Button>().interactable = false;
        bomButton.GetComponent<Button>().interactable = false;
        changeButton.GetComponent<Button>().interactable = false;
        helpbutton.GetComponent<Button>().interactable = false;
        settingButton.GetComponent<Button>().interactable = false;
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
        if (howtoPlayPopup.activeInHierarchy)
        {
            isTimerRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpPanel.activeInHierarchy == true)
            {
                helpPanel.SetActive(false);
                isHelpOption = false;
                isTimerRunning = true;
            }
            else if (updatePopUp.activeInHierarchy)
            {
                updatePopUp.SetActive(false);
                isUpdateCheck = false;
                isTimerRunning = true;
                AllBtnTrue();
            }
            else if (interNetPopup.activeInHierarchy)
            {
                interNetPopup.SetActive(false);
                isInternetCheck = false;
                isTimerRunning = true;
                AllBtnTrue();
            }
            else
            {
                if (updatePopUp.activeInHierarchy == false && interNetPopup.activeInHierarchy == false && overPanel.activeInHierarchy == false && timerPopup.activeInHierarchy == false)
                {
                    isTimerRunning = false;
                    if (isLastExitCheck == false)
                    {
                        isExitCheck = true;

                        exitPanel.SetActive(true);
                        isLastExitCheck = true;
                    }
                    else if (isLastExitCheck == true)
                    {
                        if (exitPanel.activeInHierarchy == false)
                        {
                            settingPanel.SetActive(false);
                            StartCoroutine(SettingPanelTimeforBack());
                        }
                    }
                }
            }
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (isNetCheck == false)
            {
                isNetCheck = true;
                isTimerRunning = false;
                isInternetCheck = true;
                interNetPopup.SetActive(true);
                AllBtnFalse();
                AdManager.Instance.DestroyBannerAd();
            }
        }
        else
        {
            if (isNetCheck)
            {
                StartCoroutine(AdManager.Instance.GetRequestData(AdManager.Instance.urlName));
                StartCoroutine(ShowAdBanner());
                isNetCheck = false;
                timeCountRemaining = 120f;
                isTimerRunning = true;
            }
        }


        if (timerPopup.activeInHierarchy == true && isTimeCount == false)
        {
            isTimeCount = true;
        }

        if (AdManager.Instance.interstitialAcc == 2 && AdManager.Instance.adAvailablevalue == 1)
        {
            if (isTimerRunning)
            {
                if (timeCountRemaining > 0)
                {
                    timeCountRemaining -= Time.deltaTime;
                    UpdateTimerDisplayView(timeCountRemaining);

                    if (AdManager.Instance.isInterstitialLoadBool == true)
                    {
                        if (timeCountRemaining <= 5f && !isFiveSecondWarningGiven)
                        {
                            timerPopup.SetActive(true);
                            AllBtnFalse();
                            second5 = string.Format("{00}", secondsC);
                            timerTxt.text = second5;
                        }
                    }
                }
                else
                {
                    if (overPanel.activeInHierarchy == false && exitPanel.activeInHierarchy == false)
                    {
                        timeCountRemaining = 0;
                        isTimerRunning = false;
                        isFiveSecondWarningGiven = false;
                        AdManager.Instance.ShowInterstitialsAd();
                        gamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                        timerPopup.SetActive(false);
                        StartCoroutine(ChangeBoolForTimerClose());
                    }
                }
            }
        }

        for (int i = imageFruit.Count - 1; i >= 0; i--)
        {
            if (imageFruit[i] != null && !imageFruit[i].GetComponent<SpriteRenderer>().enabled)
            {
                StartCoroutine(DeletFruitImage(imageFruit[i]));
            }
        }
    }


    public void HowToPlayPanelButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        howtoPlayPopup.SetActive(false);
        PlayerPrefs.SetInt("HowToPlay", 1);
        isTimerRunning = true;
        StartCoroutine(ChangesBoolForHowToPlayPanel());
    }

    public IEnumerator ChangesBoolForHowToPlayPanel()
    {
        yield return new WaitForSeconds(0.1f);
        isHowToPlayTouchCheck = false;
        isPanelStartCheck = false;
    }


    void SetUpSound()
    {
        var sound = SoundManager.Instance;
        if (sound != null)
        {
            bool isSoundMuted = sound.soundAudio.volume == 0;
            sound.isSoundPlay = isSoundMuted;
            sound.soundAudio.mute = isSoundMuted;
            soundButton.GetComponent<Image>().sprite = isSoundMuted ? off : on;
        }
    }

    void SetUpMusic()
    {
        var music = MusicManager.instnace;
        if (music != null)
        {
            bool isMusicMuted = music.musicAudio.volume == 0;
            music.isMusicPlay = isMusicMuted;
            music.musicAudio.mute = isMusicMuted;
            musicButton.GetComponent<Image>().sprite = isMusicMuted ? off : on;
        }
    }

    void SetUpVibration()
    {
        vibrateToggle.isOn = PlayerPrefs.GetInt("Vibrate", 0) == 0;
    }

    public void GenratedFruit()
    {
        int numberRandom = UnityEngine.Random.Range(0, fruits.Length);
        GameObject fruit = Instantiate(fruits[numberRandom], fruitsParent.transform.position, Quaternion.identity, fruitsParent.transform);
    }

    public void NextFruitImageCall()
    {
        nextFruit = UnityEngine.Random.Range(0, nextImages.Length);
        nextImage.sprite = nextImages[nextFruit];
    }

    public void AfterNextImage()
    {
        fruit = Instantiate(fruits[nextFruit], fruitsParent.transform.position, Quaternion.identity, fruitsParent.transform);
    }

    public void HelpButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        helpPanel.SetActive(true);
        isHelpOption = true;
        isTimerRunning = false;
    }

    public void HelpBackButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        helpPanel.SetActive(false);
        isHelpOption = false;
        isTimerRunning = true;
    }

    public IEnumerator ShowAdBanner()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Update Banner" + AdManager.Instance.bannerAcc + ":::" + AdManager.Instance.adAvailablevalue);
        if (AdManager.Instance.bannerAcc == 2 && AdManager.Instance.adAvailablevalue > 0)
        {
            AdManager.Instance.LoadBannerAd();
        }
    }

    public void PrivacypolicyButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        Application.OpenURL("https://www.termsfeed.com/live/74303810-e2df-4b7c-b583-762038680d53");

        settingPanel.SetActive(false);
        StartCoroutine(SettingPrivacyTime());
    }

    public IEnumerator SettingPrivacyTime()
    {
        yield return new WaitForSeconds(0.1f);
        isSettingCheck = false;
        isLastExitCheck = false;
        isTimerRunning = true;
    }

    // internetPopUp
    public void OkButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        interNetPopup.SetActive(false);
        isNetCheck = true;
        AllBtnTrue();
        StartCoroutine(ChangeInternetTime());
    }

    public IEnumerator ChangeInternetTime()
    {
        yield return new WaitForSeconds(0.2f);
        isInternetCheck = false;
        isTimerRunning = true;
    }

    IEnumerator ChangeBoolForTimerClose()
    {
        yield return new WaitForSeconds(0.2f);
        timeCountRemaining = 120f;
        isTimerRunning = true;
        isTimeCount = false;
        isExitCheck = false;
        AllBtnTrue();
    }

    void UpdateTimerDisplayView(float timeDisplayView)
    {
        timeDisplayView += 1;
        minutesC = Mathf.FloorToInt(timeDisplayView / 60);
        secondsC = Mathf.FloorToInt(timeDisplayView % 60);
        timer = string.Format("{0:00}:{1:00}", minutesC, secondsC);
    }

    IEnumerator DeletFruitImage(GameObject deletImage)
    {
        yield return new WaitForSeconds(2);
        imageFruit.Remove(deletImage);
        Destroy(deletImage);
    }

    public void RestartButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        overPanel.SetActive(false);
        scoreValue = 0;
        scoreText.text = "0";
        if (AdManager.Instance.interstitialAcc == 2 && AdManager.Instance.adAvailablevalue >= 1)
        {
            AdManager.Instance.ShowInterstitialsAd();
        }
        SceneManager.LoadScene(1);
    }

    //UpdatePanel
    public void UpdatebuttonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.fruit.merge.world.games.drop.puzzle.game");
        PlayerPrefs.SetInt("Update", AdManager.Instance.number);
        updatePopUp.SetActive(false);
        gamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        AllBtnTrue();
        StartCoroutine(ChangesTimeForUpdatePopUp());
    }

    public void NotNowButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        updatePopUp.SetActive(false);
        gamePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        AllBtnTrue();
        StartCoroutine(ChangesTimeForUpdatePopUp());
    }

    public IEnumerator ChangesTimeForUpdatePopUp()
    {
        yield return new WaitForSeconds(0.2f);
        isUpdateCheck = false;
        isTimerRunning = true;
    }

    public void SettingButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        isSettingCheck = true;
        isLastExitCheck = true;
        isTimerRunning = false;
        Debug.Log("Setting Panel Bool " + isLastExitCheck);
        settingPanel.SetActive(true);
    }

    // settingPanel
    public void BackButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        settingPanel.SetActive(false);
        StartCoroutine(SettingPanelTimeforBack());
    }

    IEnumerator SettingPanelTimeforBack()
    {
        yield return new WaitForSeconds(0.2f);
        isSettingCheck = false;
        isLastExitCheck = false;
        isTimerRunning = true;
    }

    public void SoundButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        var sound = SoundManager.Instance;
        if (sound != null)
        {
            sound.isSoundPlay = !sound.isSoundPlay;
            sound.soundAudio.mute = sound.isSoundPlay;
            sound.soundAudio.volume = sound.isSoundPlay ? 0 : 1;
            PlayerPrefs.SetInt("Sound", sound.isSoundPlay ? 0 : 1);
            soundButton.GetComponent<Image>().sprite = sound.isSoundPlay ? off : on;
        }
    }

    public void MusicButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        var music = MusicManager.instnace;
        if (music != null)
        {
            music.isMusicPlay = !music.isMusicPlay;
            music.musicAudio.mute = music.isMusicPlay;
            music.musicAudio.volume = music.isMusicPlay ? 0 : 1;
            PlayerPrefs.SetInt("Music", music.isMusicPlay ? 0 : 1);
            musicButton.GetComponent<Image>().sprite = music.isMusicPlay ? off : on;
        }
    }

    public void VibrateButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        PlayerPrefs.SetInt("Vibrate", vibrateToggle.isOn ? 0 : 1);
        if (vibrateToggle.isOn)
        {
            Vibration.Vibrate(50);
        }
    }

    public void ShareButtonObjectClickd()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        shareTextObject.Share("Fruit Merge Game" + "\n" + "\n" + "Let me recommend you this game" + "\n" + "\n" + "https://play.google.com/store/apps/details?id=com.fruit.merge.world.games.drop.puzzle.game");
    }

    public void BomButtonObjectClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        isChangeOneTimeCheck = false;
        isTimerRunning = false;
        if (imageFruit.Count == 0)
        {
            StartCoroutine(NotFoundTimeChangesAnimation());
            isBomOption = false;
            isChangeOneTimeCheck = true;
            AllBtnTrue();
            isTimerRunning = true;
        }
        else
        {
            if (AdManager.Instance.isRewardShowBool)
            {
                AdManager.Instance.ShowRewardedsAd();

                StartCoroutine(ChangeBomTimeEffect());
            }
            else
            {
                StartCoroutine(AdTimeAllObjectChanges());
            }
        }
    }

    public IEnumerator ChangeBomTimeEffect()
    {
        AllBtnFalse();
        yield return new WaitForSeconds(0.4f);
        isBomOption = true;

        foreach (var item in imageFruit)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            item.gameObject.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.Fast).SetLoops(-2, LoopType.Incremental);
        }
    }

    public IEnumerator AdTimeAllObjectChanges()
    {
        notAd.transform.DOScale(new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1.3f);
        notAd.transform.DOScale(new Vector3(0, 0, 0), 1);
        if (AdManager.Instance._RewardedAd == null)
        {
            if (AdManager.Instance.rewardAcc == 2 && AdManager.Instance.adAvailablevalue >= 1)
            {
                AdManager.Instance.LoadRewardedsAd();
            }
        }
        isBoxVibrateCheck = false;
        isButtonFirst2ObjectDestroy = false;
        isButtonReplce = false;
        isBomOption = false;
        isChangeOneTimeCheck = true;
        AllBtnTrue();
        isTimerRunning = true;
    }

    public void ChangeButtonObjectClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        isChangeOneTimeCheck = false;
        isTimerRunning = false;

        if (imageFruit.Count == 0)
        {
            StartCoroutine(NotFoundTimeChangesAnimation());
            isButtonReplce = false;
            isChangeOneTimeCheck = true;
            AllBtnTrue();
            isTimerRunning = true;
        }
        else
        {
            if (AdManager.Instance.isRewardShowBool)
            {
                AdManager.Instance.ShowRewardedsAd();
                StartCoroutine(ChangeTimeForReplce());
            }
            else
            {
                StartCoroutine(AdTimeAllObjectChanges());
            }
        }
    }

    public IEnumerator ChangeTimeForReplce()
    {
        AllBtnFalse();
        yield return new WaitForSeconds(0.4f);
        isButtonReplce = true;

        foreach (var item in imageFruit)
        {
            item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            item.gameObject.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.Fast).SetLoops(-2, LoopType.Incremental);
        }
    }

    public void BoxVibrateButtonObjectClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        isTimerRunning = false;
        if (isBoxVibrateCheck == false)
        {
            isBoxVibrateCheck = true;

            if (imageFruit.Count == 0)
            {
                isBoxVibrateCheck = false;
                isButtonBoxVibrateOption = false;
                AllBtnTrue();
                isTimerRunning = true;
                StartCoroutine(NotFoundTimeChangesAnimation());
            }
            else
            {
                if (AdManager.Instance.isRewardShowBool)
                {
                    AdManager.Instance.ShowRewardedsAd();
                    StartCoroutine(BoxChangeTimeForVibrate());
                }
                else
                {
                    StartCoroutine(AdTimeAllObjectChanges());
                }
            }
        }
    }

    public IEnumerator BoxChangeTimeForVibrate()
    {
        AllBtnFalse();

        yield return new WaitForSeconds(0.4f);

        gameOverObject1.SetActive(false);
        gameOverObject2.SetActive(false);
        gameOverObject3.SetActive(false);
        isButtonBoxVibrateOption = true;
        mainCamera.DOOrthoSize(7, 0.5f);
        BoxObjectRotate();
    }

    public void BoxObjectRotate()
    {
        StartCoroutine(WobbleObjectAnimation());
    }

    IEnumerator WobbleObjectAnimation()
    {
        yield return new WaitForSeconds(.5f);
        colliderObject.SetActive(true);
        MoveDownBox();
        yield return new WaitForSeconds(1f);
        MoveLeftRightBox();
        Sequence wobbleSequenceObject = DOTween.Sequence();

        wobbleSequenceObject.Append(box.transform.DORotate(new Vector3(0, 0, wobbleBoxAngle), wobbleBoxDuration).SetEase(Ease.InOutSine));

        wobbleSequenceObject.Append(box.transform.DORotate(new Vector3(0, 0, -wobbleBoxAngle), wobbleBoxDuration).SetEase(Ease.InOutSine));

        wobbleSequenceObject.Append(box.transform.DORotate(new Vector3(0, 0, 0), wobbleBoxDuration).SetEase(Ease.InOutSine));

        wobbleSequenceObject.SetLoops(4, LoopType.Restart);

        yield return new WaitForSeconds(1);
        box.transform.eulerAngles = Vector3.zero;
        colliderObject.SetActive(false);
        isButtonBoxVibrateOption = false;
        mainCamera.DOOrthoSize(6, 0.5f);
        colliderObject.transform.DOMoveY(-4.5f, 1);
        StartCoroutine(ChangeBoxVibrate());
    }

    public IEnumerator ChangeBoxVibrate()
    {
        yield return new WaitForSeconds(2f);
        gameOverObject1.SetActive(true);
        gameOverObject2.SetActive(true);
        gameOverObject3.SetActive(true);
        isBoxVibrateCheck = false;
        isTimerRunning = true;
        AllBtnTrue();
    }

    public void MoveDownBox()
    {
        colliderObject.transform.DOMoveY(-3.3f, 1);
    }

    public void MoveLeftRightBox()
    {
        Sequence leftRightSequence = DOTween.Sequence();

        leftRightSequence.Append(colliderObject.transform.DOMoveX(colliderObject.transform.position.x + distanceBox, durationBox).SetEase(Ease.InOutSine));

        leftRightSequence.Append(colliderObject.transform.DOMoveX(colliderObject.transform.position.x - distanceBox, durationBox).SetEase(Ease.InOutSine));

        leftRightSequence.SetLoops(2, LoopType.Yoyo);

        leftRightSequence.Play();
    }

    public void First2DestroyButtonObjectClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        isTimerRunning = false;
        isButtonFirst2ObjectDestroy = true;
        isOff = false;

        if (imageFruit.Count == 0)
        {
            StartCoroutine(NotFoundTimeChangesAnimation());
            isButtonFirst2ObjectDestroy = false;
            AllBtnTrue();
            isTimerRunning = true;
        }
        else
        {
            if (AdManager.Instance.isRewardShowBool)
            {
                AdManager.Instance.ShowRewardedsAd();
                StartCoroutine(ChangeTimeForFirst2Object());
            }
            else
            {
                StartCoroutine(AdTimeAllObjectChanges());
            }
        }
    }

    public IEnumerator ChangeTimeForFirst2Object()
    {
        AllBtnFalse();

        yield return new WaitForSeconds(0.4f);
        StartCoroutine(ResetAllFruits());
    }

    public IEnumerator NotFoundTimeChangesAnimation()
    {
        notFound.transform.DOScale(new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1.3f);
        notFound.transform.DOScale(new Vector3(0, 0, 0), 1);
    }

    public IEnumerator ResetAllFruits()
    {
        if (isButtonFirst2ObjectDestroy == true && isBomOption == false && isButtonReplce == false && isButtonBoxVibrateOption == false)
        {
            for (int i = 0; i < imageFruit.Count; i++)
            {
                if (imageFruit[i].gameObject.CompareTag("Strawberry") || imageFruit[i].gameObject.CompareTag("Apricot"))
                {
                    first2Object.Add(imageFruit[i].gameObject);
                }

            }

            if (first2Object.Count == 0)
            {
                StartCoroutine(NotFoundTimeChangesAnimation());
            }


            foreach (var itemObject in first2Object)
            {
                ParticleSystem particle = Instantiate(particleEffect);
                particle.transform.SetParent(itemObject.gameObject.transform);
                particle.transform.position = itemObject.transform.position;

                Destroy(itemObject, 0.2f);
            }
            Debug.Log(imageFruit.Count);


            imageFruit.Clear();
            first2Object.Clear();

            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < fruitsParent.transform.childCount; i++)
            {
                Rigidbody2D rb = fruitsParent.transform.GetChild(i).GetComponent<Rigidbody2D>();

                if (rb.isKinematic == false)
                {
                    imageFruit.Add(fruitsParent.transform.GetChild(i).gameObject);
                }
            }

            isButtonFirst2ObjectDestroy = false;
            AllBtnTrue();
            isTimerRunning = true;
        }
    }

    public IEnumerator IsFruitCheckBool()
    {
        yield return new WaitForSeconds(0.3f);
        isFruitObject = false;
    }

    // QuitPopup
    public void ExitYesButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        StartCoroutine(ChangeQuitTimeClose());
        Application.Quit();
    }

    public void ExitNoButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SButtonSoundClip();
        }
        exitPanel.SetActive(false);
        isTimerRunning = true;
        StartCoroutine(ChangeQuitTimeClose());
    }

    public IEnumerator ChangeQuitTimeClose()
    {
        yield return new WaitForSeconds(0.2f);
        isExitCheck = false;
        isLastExitCheck = false;
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