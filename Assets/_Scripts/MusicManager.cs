using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource MusicAudio;

    public bool isMusic = false;

    public static MusicManager instnace;

    public void Awake()
    {
        instnace = this;
    }

    private void Start()
    {
        MusicAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Music") == false)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            MusicAudio.volume = PlayerPrefs.GetInt("Music");
        }
    }
}
