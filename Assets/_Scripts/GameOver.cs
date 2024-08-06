using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool istouch = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Movement movement = collision.gameObject.GetComponent<Movement>();

        if (movement != null)
        {
            istouch = true;
        }
        else
        {
            istouch = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        istouch = false;
    }

    private void Update()
    {
        if (GameManager.instance.GameOverObject1.GetComponent<GameOver>().istouch == true && GameManager.instance.GameOverObject2.GetComponent<GameOver>().istouch == true && GameManager.instance.GameOverObject3.GetComponent<GameOver>().istouch == true)
        {
            // Debug.Log("GameOver");
            AdManager.Instance.ShowInterstitialAd();
            GameManager.instance.isGameOver = true;
            GameManager.instance.OverPanel.SetActive(true);
            GameManager.instance.ScoreValueOver.text = GameManager.instance.ScoreValue.ToString();
            Debug.Log("SCOREOVER" + GameManager.instance.ScoreValue.ToString());
            Debug.Log("HIGHSCOREOVER" + GameManager.instance.HighScore.ToString());

            if (GameManager.instance.ScoreValue > GameManager.instance.HighScore)
            {
                GameManager.instance.HighScore = GameManager.instance.ScoreValue;
                GameManager.instance.HighScoreText.text = GameManager.instance.HighScore.ToString();
                PlayerPrefs.SetInt("HighScore", GameManager.instance.HighScore);
            }
        }
    }
}
