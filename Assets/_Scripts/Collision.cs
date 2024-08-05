using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    //public GameObject apricot, redGrape, lime, peer, pineApple, orange, watermelonp;

    public GameObject Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

    public ParticleSystem Blue, Green, Green1, GreenDark, NewyBlue, Orange, Orange1, Red, Yellow;

    public Transform ParticalParent;

    public TextMeshPro textNumber;

    public Transform TextParent;

    TextMeshPro newtext;

    private void Start()
    {
        ParticalParent = GameObject.Find("ParticalObject").GetComponent<Transform>();
        TextParent = GameObject.Find("TextObjects").GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChekFruits(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ChekFruits(collision);
    }

    public void ChekFruits(Collision2D newcollisiton)
    {
        if (newcollisiton.gameObject.tag == "Strawberry" && gameObject.tag == "Strawberry")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Strawberry + Apricot");
                ParticalesEffect(Orange);
                GameManager.instance.isFruit = true;
                FruitChanges(Apricot);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 1;
                TextCreate(1);
            }
        }
        else if (newcollisiton.gameObject.tag == "Apricot" && gameObject.tag == "Apricot")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Apricot + Blueberry");
                ParticalesEffect(Blue);
                GameManager.instance.isFruit = true;
                FruitChanges(Blueberry);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 2;
                TextCreate(2);
            }
        }
        else if (newcollisiton.gameObject.tag == "Blueberry" && gameObject.tag == "Blueberry")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Blueberry + Guava");
                ParticalesEffect(Green);
                GameManager.instance.isFruit = true;
                FruitChanges(Guava);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 5;
                TextCreate(5);
            }
        }
        else if (newcollisiton.gameObject.tag == "Guava" && gameObject.tag == "Guava")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Guava + Apple");
                ParticalesEffect(Red);
                GameManager.instance.isFruit = true;
                FruitChanges(Apple);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 10;
                TextCreate(10);
            }
        }
        else if (newcollisiton.gameObject.tag == "Apple" && gameObject.tag == "Apple")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Apple + Grapefruit");
                ParticalesEffect(Orange1);
                GameManager.instance.isFruit = true;
                FruitChanges(Grapefruit);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 15;
                TextCreate(15);
            }
        }
        else if (newcollisiton.gameObject.tag == "Grapefruit" && gameObject.tag == "Grapefruit")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Grapefruit + Passionfruit");
                ParticalesEffect(NewyBlue);
                GameManager.instance.isFruit = true;
                FruitChanges(Passionfruit);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 20;
                TextCreate(20);
            }
        }
        else if (newcollisiton.gameObject.tag == "Passionfruit" && gameObject.tag == "Passionfruit")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Passionfruit + Lucuma");
                ParticalesEffect(GreenDark);
                GameManager.instance.isFruit = true;
                FruitChanges(Lucuma);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 25;
                TextCreate(25);
            }
        }

        else if (newcollisiton.gameObject.tag == "Lucuma" && gameObject.tag == "Lucuma")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Lucuma + Cloudberry");
                ParticalesEffect(Yellow);
                GameManager.instance.isFruit = true;
                FruitChanges(Cloudberry);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 35;
                TextCreate(35);
            }
        }
        else if (newcollisiton.gameObject.tag == "Cloudberry" && gameObject.tag == "Cloudberry")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Cloudberry + Watermelon");
                ParticalesEffect(Green1);
                GameManager.instance.isFruit = true;
                FruitChanges(Watermelon);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 40;
                TextCreate(40);
            }
        }
        else if (newcollisiton.gameObject.tag == "Watermelon" && gameObject.tag == "Watermelon")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Watermelon");
                ParticalesEffect(Green1);
                GameManager.instance.isFruit = true;
                GameManager.instance.ScoreValue += 50;
                TextCreate(50);
            }
        }

        GameManager.instance.ScoreText.text = GameManager.instance.ScoreValue.ToString();
        //PlayerPrefs.SetInt("Score", GameManager.instance.ScoreValue);
    }

    public void TextCreate(int value)
    {
        newtext = Instantiate(textNumber);
        newtext.transform.SetParent(TextParent);
        newtext.text = "+" + value.ToString();
        newtext.transform.position = gameObject.transform.position;
        StartCoroutine(TextColorChange());
    }

    public IEnumerator TextColorChange()
    {
        newtext.DOFade(0, 1);
        yield return new WaitForSeconds(1);
    }

    public void ParticalesEffect(ParticleSystem newparticle)
    {
        ParticleSystem particleSystem = Instantiate(newparticle);
        particleSystem.transform.SetParent(ParticalParent);
        particleSystem.transform.position = gameObject.transform.position;
    }

    public void DestroyObject(Collision2D newcollision)
    {
        StartCoroutine(GameManager.instance.IsFruit());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        newcollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        newcollision.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void FruitChanges(GameObject fruits)
    {

        Debug.Log("vibrate_Fruit");
        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            Vibration.Vibrate(50);
        }
        GameObject a = Instantiate(fruits);
        SoundManager.Instance.SoundFruitMergePlay();
        a.transform.GetComponent<Collider2D>().enabled = true;
        GameManager.instance.image.Add(a);
        Movement.instance.isSelect = true;
        a.transform.position = gameObject.transform.position;
        a.transform.SetParent(GameManager.instance.ParentObj.transform);
        a.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}