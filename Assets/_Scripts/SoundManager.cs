using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundAudio;

    public AudioClip fruitMergeClip;

    public AudioClip fruitClip;

    public AudioClip buttonClip;

    public bool isSoundPlay = false;

    public static SoundManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        soundAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Sound") == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            soundAudio.volume = PlayerPrefs.GetInt("Sound");
        }
    }

    public void SFruitMergePlay()
    {
        soundAudio.PlayOneShot(fruitMergeClip);
    }

    public void SFruitSoundPlay()
    {
        soundAudio.PlayOneShot(fruitClip);
    }

    public void SButtonSoundClip()
    {
        soundAudio.PlayOneShot(buttonClip);
    }
}