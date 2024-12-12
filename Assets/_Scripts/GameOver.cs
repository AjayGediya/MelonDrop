using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool isTouch = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        Movement movementObject = collision.gameObject.GetComponent<Movement>();

        if (movementObject != null)
        {
            isTouch = true;
        }
        else
        {
            isTouch = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    public void Update()
    {
        if (GameManager.instance.gameOverObject1.GetComponent<GameOver>().isTouch == true && GameManager.instance.gameOverObject2.GetComponent<GameOver>().isTouch == true && GameManager.instance.gameOverObject3.GetComponent<GameOver>().isTouch == true)
        {
            GameManager.instance.isTimerRunning = false;
            if (!GameManager.instance.isGameOverCheck)
            {
                GameManager.instance.isGameOverCheck = true;
                GameManager.instance.overPanel.SetActive(true);
            }


            if (GameManager.instance.overPanel.activeInHierarchy == true)
            {
                GameManager.instance.timerPopup.SetActive(false);
            }

            GameManager.instance.scoreValueOver.text = GameManager.instance.scoreValue.ToString();

            if (GameManager.instance.scoreValue > GameManager.instance.highScore)
            {
                GameManager.instance.highScore = GameManager.instance.scoreValue;
                GameManager.instance.highScoreText.text = GameManager.instance.highScore.ToString();
                PlayerPrefs.SetInt("HighScore", GameManager.instance.highScore);
            }
        }
    }
}
