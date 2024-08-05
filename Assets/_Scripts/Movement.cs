using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 Move;

    public float xMin = -1.9f;

    public float xMax = 1.9f;

    public GameObject Line;

    public bool isSelect = false;

    public static Movement instance;

    GameManager A = GameManager.instance;

    public Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Line = GameObject.Find("Line");

        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelect == false && A.isGameOver == false)
        {
            // Debug.Log("Click");
            Move = transform.position - GetMouseWorldPos();
            Line.SetActive(true);
            A.GameOverObject1.SetActive(false);
            A.GameOverObject2.SetActive(false);
            A.GameOverObject3.SetActive(false);
        }

        if (Input.GetMouseButton(0) && isSelect == false && A.isGameOver == false)
        {
            // Debug.Log("Drag");
            A.GameOverObject1.SetActive(false);
            A.GameOverObject2.SetActive(false);
            A.GameOverObject3.SetActive(false);
            Vector3 mousePos = GetMouseWorldPos() + Move;

            float clampedX = Mathf.Clamp(mousePos.x, xMin, xMax);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            Line.transform.position = new Vector3(clampedX, Line.transform.position.y, Line.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && isSelect == false && A.isGameOver == false)
        {
            // Debug.Log("Up");
            SoundManager.Instance.FruitSoundPlay();
            gameObject.transform.GetComponent<PolygonCollider2D>().enabled = true;
            isSelect = true;
            Line.SetActive(false);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.freezeRotation = false;
            rb.angularVelocity = UnityEngine.Random.Range(-360, 360);
            StartCoroutine(valueChange());
            StartCoroutine(ChangeOver());
        }
    }


    public IEnumerator ChangeOver()
    {
        yield return new WaitForSeconds(0.5f);
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

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
