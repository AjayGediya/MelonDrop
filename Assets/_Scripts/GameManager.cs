using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public GameObject[] Fruits;

    public GameObject FruitsParent;

    public GameObject ParentObj;

    public List<GameObject> image = new List<GameObject>();

    public bool isFruit = false;

    public TextMeshProUGUI ScoreText;

    public int ScoreValue;

    public TextMeshProUGUI HighScoreText;

    public TextMeshProUGUI ScoreValueOver;

    public int HighScore;

    public GameObject GameOverObject1, GameOverObject2, GameOverObject3;

    public GameObject GamePanel, OverPanel;

    public bool isGameOver = false;

    public Sprite[] NextImages;

    public Image NextImage;

    public static GameManager instance;

    int NextFruit;

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("Score") == false)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        else
        {
            ScoreValue = PlayerPrefs.GetInt("Score");
        }

        if (PlayerPrefs.HasKey("HighScore") == false)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        else
        {
           HighScore = PlayerPrefs.GetInt("HighScore");
        }
        HighScoreText.text = "HighScore" + ":" + HighScore.ToString();
    }

    public void Start()
    {
        Fruits = Resources.LoadAll<GameObject>("Prefabs");
        NextImages = Resources.LoadAll<Sprite>("Sprite");
        isGameOver = false;
        GenratedGrid();
        nextImage();
        
    }


    public void GenratedGrid()
    {
        int Number = UnityEngine.Random.Range(0, Fruits.Length);
        Debug.Log(Number);
        GameObject fruit = Instantiate(Fruits[Number]);
        Debug.Log(fruit + "::" + "Fruit");
        image.Add(fruit);
        fruit.transform.SetParent(ParentObj.transform);
        fruit.transform.position = FruitsParent.transform.position;
    }

    public void nextImage()
    {
        NextFruit = UnityEngine.Random.Range(0,NextImages.Length);
        NextImage.sprite = NextImages[NextFruit];
    }

    public void AfterNextImageCall()
    {
        GameObject fruit = Instantiate(Fruits[NextFruit]);
        Debug.Log(fruit + "::" + "Fruit");
        image.Add(fruit);
        fruit.transform.SetParent(ParentObj.transform);
        fruit.transform.position = FruitsParent.transform.position;
    }

    private void Update()
    {
        foreach (var item in image)
        {
            if (item.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                StartCoroutine(DeletImage(item));
            }
        }
    }

    public IEnumerator DeletImage(GameObject deletimage)
    {
        yield return new WaitForSeconds(2);

        Destroy(deletimage);
        image.Remove(deletimage);
    }

    public IEnumerator IsFruit()
    {
        yield return new WaitForSeconds(0.3f);
        isFruit = false;
    }

    public void RestartBtnClick()
    {
        OverPanel.SetActive(false);
        ScoreValue = 0;
        ScoreText.text = "0";
        SceneManager.LoadScene(1);
    }
}
