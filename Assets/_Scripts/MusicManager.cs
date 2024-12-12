using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicAudio;

    public bool isMusicPlay = false;

    public static MusicManager instnace;

    public void Awake()
    {
        instnace = this;
    }

    public void Start()
    {
        musicAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Music") == false)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            musicAudio.volume = PlayerPrefs.GetInt("Music");
        }
    }
}
