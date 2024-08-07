using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTouch : MonoBehaviour
{
    public Sprite Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

    public ParticleSystem particle;

    GameObject A;

    Vector3 POS;

    private void OnMouseDown()
    {
        if (GameManager.instance.isButtonOption == true && GameManager.instance.isButtonChange == false && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false)
        {
            Debug.Log("Bom");
            Debug.Log(gameObject.name);
            ParticleSystem particleSystem = Instantiate(particle);
            particleSystem.transform.position = gameObject.transform.position;
            particleSystem.transform.SetParent(gameObject.transform);
            // Destroy(gameObject,0.2f);
            StartCoroutine(DestroyFruit());
            GameManager.instance.image.Remove(gameObject);

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonOption = false;
        }


        if (GameManager.instance.isButtonChange == true && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false && GameManager.instance.isButtonOption == false)
        {
            Debug.Log("Change");
            Debug.Log(gameObject.name);

            if (gameObject.name == "Strawberry(Clone)")
            {
                FruitChange(Apricot, "Apricot");
            }
            else if (gameObject.name == "Apricot(Clone)")
            {
                Debug.Log("Apr");
                FruitChange(Blueberry, "Blueberry");
            }
            else if (gameObject.name == "Blueberry(Clone)")
            {
                Debug.Log("B");
                FruitChange(Guava, "Guava");
            }
            else if (gameObject.name == "Guava(Clone)")
            {
                Debug.Log("G");
                FruitChange(Apple, "Apple");
            }
            else if (gameObject.name == "Apple(Clone)")
            {
                Debug.Log("App");
                FruitChange(Grapefruit, "Grapefruit");
            }
            else if (gameObject.name == "Grapefruit(Clone)")
            {
                Debug.Log("Gra");
                FruitChange(Passionfruit, "Passionfruit");
            }
            else if (gameObject.name == "Passionfruit(Clone)")
            {
                Debug.Log("Pa");
                FruitChange(Lucuma, "Lucuma");
            }
            else if (gameObject.name == "Lucuma(Clone)")
            {
                Debug.Log("L");
                FruitChange(Cloudberry, "Cloudberry");
            }
            else if (gameObject.name == "Cloudberry(Clone)")
            {
                Debug.Log("Clo");
                FruitChange(Watermelon, "Watermelon");
            }
            else if (gameObject.name == "Watermelon(Clone)")
            {
                Debug.Log("Wat");
               // FruitChange(Apricot, "Apricot");
            }

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonChange = false;
        }
    }

    public void FruitChange(Sprite sprite, string name)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.name = name + "(Clone)";
        gameObject.tag = name;
    }

    IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
