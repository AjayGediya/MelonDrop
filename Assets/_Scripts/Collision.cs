using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject grape, redGrape, lime, peer, pineApple, orange, watermelonp;

    public ParticleSystem green, red, kthai, yellow, pink;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Apple" && gameObject.tag == "Apple")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Apple + Grape");
                ParticalesEffect(green, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(grape, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 2;
            }
        }
        else if (collision.gameObject.tag == "Grape" && gameObject.tag == "Grape")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Grape + Red");
                ParticalesEffect(pink, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(redGrape, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 4;
            }
        }
        else if (collision.gameObject.tag == "Red_Grape" && gameObject.tag == "Red_Grape")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Red + Lime");
                ParticalesEffect(green, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(lime, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 8;
            }
        }
        else if (collision.gameObject.tag == "Lime" && gameObject.tag == "Lime")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Lime + peer");
                ParticalesEffect(green, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(peer, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 16;
            }
        }
        else if (collision.gameObject.tag == "Peer" && gameObject.tag == "Peer")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(kthai, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(pineApple, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 32;
            }
        }
        else if (collision.gameObject.tag == "PineApple" && gameObject.tag == "PineApple")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(yellow, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(orange, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 64;
            }
        }
        else if (collision.gameObject.tag == "Orange" && gameObject.tag == "Orange")
        {
            if (!GameManager.instance.isFruit)
            {
                ParticalesEffect(red, gameObject);
                GameManager.instance.isFruit = true;
                FruitChanges(watermelonp, transform);
                DestroyObject(gameObject, collision);
                GameManager.instance.ScoreValue += 128;
            }
        }
        else if (collision.gameObject.tag == "Watermelon" && gameObject.tag == "Watermelon")
        {
            if (!GameManager.instance.isFruit)
            {
                Debug.Log("Watermelon");
                ParticalesEffect(red, gameObject);
                GameManager.instance.isFruit = true;
                GameManager.instance.ScoreValue += 256;
            }
        }

        GameManager.instance.ScoreText.text = "Score" + ":" + GameManager.instance.ScoreValue.ToString();
        //PlayerPrefs.SetInt("Score", GameManager.instance.ScoreValue);
    }

    public void ParticalesEffect(ParticleSystem newparticle, GameObject game)
    {
        ParticleSystem particleSystem = Instantiate(newparticle);
        particleSystem.transform.position = game.transform.position;
    }

    public void DestroyObject(GameObject newObject, Collision2D newcollision)
    {
        StartCoroutine(GameManager.instance.IsFruit());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
      //  gameObject.transform.GetChild(0).gameObject.SetActive(false);
        newcollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        newcollision.gameObject.GetComponent<Collider2D>().enabled = false;
       // newcollision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void FruitChanges(GameObject fruits, Transform main)
    {
        GameObject a = Instantiate(fruits);
        GameManager.instance.image.Add(a);
        Movement.instance.isSelect = true;
        a.transform.position = main.transform.position;
        a.transform.SetParent(GameManager.instance.ParentObj.transform);
        a.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}