using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundAudio;

    public AudioClip FruitMergeClip;

    public AudioClip FruitClip;

    public bool isSound = false;

    public static SoundManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SoundAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Sound") == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            SoundAudio.volume = PlayerPrefs.GetInt("Sound");
        }
    }

    public void SoundFruitMergePlay()
    {
        SoundAudio.PlayOneShot(FruitMergeClip);
    }

    public void FruitSoundPlay()
    {
        SoundAudio.PlayOneShot(FruitClip);
    }
}
