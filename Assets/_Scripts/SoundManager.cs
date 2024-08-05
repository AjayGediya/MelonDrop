using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundAudio;

    public bool isSound = false;

    public AudioClip FruitMergeClip;

    public AudioClip FruitClip;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
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