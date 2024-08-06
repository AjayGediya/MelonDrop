using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 StartPos, EndPos;

    public Vector3 ScreenSize;

    public GameObject Line;

    public bool isSelect = false;

    public static Movement instance;

    GameManager A = GameManager.instance;

    public Rigidbody2D rb;

   // Vector3 newpos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Line = GameObject.Find("Line");

        rb = GetComponent<Rigidbody2D>();

        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    public void Update()
    {
        Vector3 pos;

        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameManager.instance.FruitsParent.transform.position.y > pos.y)
        {
            if (Input.GetMouseButtonDown(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
            {
                Line.SetActive(true);
                A.GameOverObject1.SetActive(false);
                A.GameOverObject2.SetActive(false);
                A.GameOverObject3.SetActive(false);
                StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
            {
                A.GameOverObject1.SetActive(false);
                A.GameOverObject2.SetActive(false);
                A.GameOverObject3.SetActive(false);

                EndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 diff = EndPos - StartPos;

                diff.x = Mathf.Clamp(diff.x, -1.9f, 1.9f);

                transform.position = new Vector3(diff.x, transform.position.y, transform.position.z);

                Line.transform.position = new Vector3(diff.x, Line.transform.position.y, Line.transform.position.z);
            }

            if (Input.GetMouseButtonUp(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
            {
                // Debug.Log("Up");
                SoundManager.Instance.FruitSoundPlay();
                gameObject.transform.GetComponent<PolygonCollider2D>().enabled = true;
                GameManager.instance.image.Add(gameObject);
                isSelect = true;
                Line.SetActive(false);
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.freezeRotation = false;
                rb.angularVelocity = UnityEngine.Random.Range(-360, 360);
                StartCoroutine(valueChange());
                StartCoroutine(ChangeOver());
            }
        }
    }


    public IEnumerator ChangeOver()
    {
        yield return new WaitForSeconds(1f);
        A.GameOverObject1.SetActive(true);
        A.GameOverObject2.SetActive(true);
        A.GameOverObject3.SetActive(true);
    }

    public IEnumerator valueChange()
    {
        yield return new WaitForSeconds(1);
        Line.SetActive(true);
        Line.transform.position = new Vector3(0, 0, 0);
        A.AfterNextImageCall();
        A.nextImage();
    }
}
