using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    public GameObject grape, redGrape, lime, peer, pineApple, orange, watermelonp;

    public ParticleSystem green, red, kthai, yellow, pink;

    public TextMeshPro textNumber;

    TextMeshPro newtext;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChekFruits(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ChekFruits(collision);
    }

    public void TextCreate(int value)
    {
        newtext = Instantiate(textNumber);
        newtext.text = "+" + value.ToString();
        newtext.transform.position = gameObject.transform.position;
        StartCoroutine(TextColorChange());
    }

    public IEnumerator TextColorChange()
    {
        newtext.DOFade(0, 1f);
        yield return new WaitForSeconds(1);

    }

    public void ChekFruits(Collision2D newcollisiton)
    {
        if (newcollisiton.gameObject.tag == "Apple" && gameObject.tag == "Apple")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Apple + Grape");
                ParticalesEffect(green);
                GameManager.instance.isFruit = true;
                FruitChanges(grape);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 2;
                TextCreate(2);
            }
        }
        else if (newcollisiton.gameObject.tag == "Grape" && gameObject.tag == "Grape")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Grape + Red");
                ParticalesEffect(pink);
                GameManager.instance.isFruit = true;
                FruitChanges(redGrape);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 4;
                TextCreate(4);
            }
        }
        else if (newcollisiton.gameObject.tag == "Red_Grape" && gameObject.tag == "Red_Grape")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Red + Lime");
                ParticalesEffect(green);
                GameManager.instance.isFruit = true;
                FruitChanges(lime);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 8;
                TextCreate(8);
            }
        }
        else if (newcollisiton.gameObject.tag == "Lime" && gameObject.tag == "Lime")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Lime + peer");
                ParticalesEffect(green);
                GameManager.instance.isFruit = true;
                FruitChanges(peer);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 16;
                TextCreate(16);
            }
        }
        else if (newcollisiton.gameObject.tag == "Peer" && gameObject.tag == "Peer")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(kthai);
                GameManager.instance.isFruit = true;
                FruitChanges(pineApple);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 32;
                TextCreate(32);
            }
        }
        else if (newcollisiton.gameObject.tag == "PineApple" && gameObject.tag == "PineApple")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(yellow);
                GameManager.instance.isFruit = true;
                FruitChanges(orange);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 64;
                TextCreate(64);
            }
        }
        else if (newcollisiton.gameObject.tag == "Orange" && gameObject.tag == "Orange")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(red);
                GameManager.instance.isFruit = true;
                FruitChanges(watermelonp);
                DestroyObject(newcollisiton);
                GameManager.instance.ScoreValue += 128;
                TextCreate(128);
            }
        }
        else if (newcollisiton.gameObject.tag == "Watermelon" && gameObject.tag == "Watermelon")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Watermelon");
                ParticalesEffect(red);
                GameManager.instance.isFruit = true;
                GameManager.instance.ScoreValue += 256;
                TextCreate(256);
            }
        }

        GameManager.instance.ScoreText.text = "Score" + ":" + GameManager.instance.ScoreValue.ToString();
        //PlayerPrefs.SetInt("Score", GameManager.instance.ScoreValue);
    }

    public void ParticalesEffect(ParticleSystem newparticle)
    {
        ParticleSystem particleSystem = Instantiate(newparticle);
        particleSystem.transform.position = gameObject.transform.position;
    }

    public void DestroyObject(Collision2D newcollision)
    {
        StartCoroutine(GameManager.instance.IsFruit());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        newcollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        newcollision.gameObject.GetComponent<Collider2D>().enabled = false;
        newcollision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void FruitChanges(GameObject fruits)
    {
        Vibration.Vibrate(50);
        GameObject a = Instantiate(fruits);
        GameManager.instance.image.Add(a);
        Movement.instance.isSelect = true;
        a.transform.position = gameObject.transform.position;
        a.transform.SetParent(GameManager.instance.ParentObj.transform);
        a.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}