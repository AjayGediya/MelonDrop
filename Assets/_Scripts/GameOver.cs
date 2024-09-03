using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool istouch = false;

    public void OnTriggerStay2D(Collider2D collision)
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

    public void OnTriggerExit2D(Collider2D collision)
    {
        istouch = false;
    }

    public void Update()
    {
        if (GameManager.instance.GameOverObject1.GetComponent<GameOver>().istouch == true && GameManager.instance.GameOverObject2.GetComponent<GameOver>().istouch == true && GameManager.instance.GameOverObject3.GetComponent<GameOver>().istouch == true)
        {
            GameManager.instance.timerIsRunning = false;
            //Debug.Log("GameOver");
            if (!GameManager.instance.isGameOver)
            {
                Debug.Log("OverPanelUpdate");
                GameManager.instance.isGameOver = true;
                GameManager.instance.OverPanel.SetActive(true);
            }


            if (GameManager.instance.OverPanel.activeInHierarchy == true)
            {
                GameManager.instance.TimerPopup.SetActive(false);
            }

            GameManager.instance.ScoreValueOver.text = GameManager.instance.ScoreValue.ToString();
            //Debug.Log("SCOREOVER" + GameManager.instance.ScoreValue.ToString());
            //Debug.Log("HIGHSCOREOVER" + GameManager.instance.HighScore.ToString());

            if (GameManager.instance.ScoreValue > GameManager.instance.HighScore)
            {
                GameManager.instance.HighScore = GameManager.instance.ScoreValue;
                GameManager.instance.HighScoreText.text = GameManager.instance.HighScore.ToString();
                PlayerPrefs.SetInt("HighScore", GameManager.instance.HighScore);
            }
        }
    }
}
