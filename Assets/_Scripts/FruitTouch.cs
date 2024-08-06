using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTouch : MonoBehaviour
{
    public GameObject Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

    GameObject A;

    private void OnMouseDown()
    {
        if (GameManager.instance.isButtonOption == true)
        {
            Debug.Log("Bom");
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            GameManager.instance.image.Remove(gameObject);

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonOption = false;
        }
        else if (GameManager.instance.isButtonChange == true)
        {
            Debug.Log("Change");
            Debug.Log(gameObject.name);

            if (gameObject.name == "Strawberry(Clone)")
            {
                A = Instantiate(Apricot);
                A.transform.position = gameObject.transform.position;
                A.transform.SetParent(GameManager.instance.ParentObj.transform);
                Rigidbody2D rb = A.gameObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                A.transform.GetComponent<PolygonCollider2D>().enabled = true;
                GameManager.instance.image.Add(A);
                Destroy(gameObject);
                Debug.Log("S");
            }
            else if (gameObject.name == "Apricot(Clone)")
            {
                Debug.Log("Apr");
            }
            else if (gameObject.name == "Blueberry(Clone)")
            {
                Debug.Log("B");
            }
            else if (gameObject.name == "Guava(Clone)")
            {
                Debug.Log("G");
            }
            else if (gameObject.name == "Apple(Clone)")
            {
                Debug.Log("App");
            }
            else if (gameObject.name == "Grapefruit(Clone)")
            {
                Debug.Log("Gra");
            }
            else if (gameObject.name == "passion-fruit(Clone)")
            {
                Debug.Log("Pa");
            }
            else if (gameObject.name == "Lucuma(Clone)")
            {
                Debug.Log("L");
            }
            else if (gameObject.name == "Cloudberry(Clone)")
            {
                Debug.Log("Clo");
            }
            else if (gameObject.name == "Watermelon(Clone)")
            {
                Debug.Log("Wat");
            }

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonChange = false;
        }
    }
}
