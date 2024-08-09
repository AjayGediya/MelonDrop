using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

public class FruitTouch : MonoBehaviour
{
    public Sprite Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

    public ParticleSystem particle;

    GameObject A;

    Vector3 POS;

    public void OnMouseDown()
    {
        if (GameManager.instance.isButtonOption == true && GameManager.instance.isButtonChange == false && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false)
        {
            Debug.Log("Bom");
            Debug.Log(gameObject.name);
            ParticleSystem particleSystem = Instantiate(particle);
            particleSystem.transform.position = gameObject.transform.position;
            particleSystem.transform.SetParent(gameObject.transform);
            Profiler.BeginSample("DestroyFruit");
            Destroy(gameObject,0.5f);
            Profiler.EndSample();
            GameManager.instance.image.Remove(gameObject);

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonOption = false;
            GameManager.instance.isBom = false;
        }


        if (GameManager.instance.isButtonChange == true && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false && GameManager.instance.isButtonOption == false)
        {
            Debug.Log("Change");
            Debug.Log(gameObject.name);

            if (gameObject.name == "Strawberry(Clone)")
            {
                Profiler.BeginSample("FruitChange");
                FruitChange(Apricot, "Apricot", new Vector3(1, 1, 1), 0.228f, new Vector2(0, -0.056f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Apricot(Clone)")
            {
                Debug.Log("Apr");
                Profiler.BeginSample("FruitChange");
                FruitChange(Blueberry, "Blueberry", new Vector3(1.3f, 1.3f, 1.3f), 0.32f, new Vector2(-0.06f, -0.09f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Blueberry(Clone)")
            {
                Debug.Log("B");
                Profiler.BeginSample("FruitChange");
                FruitChange(Guava, "Guava", new Vector3(2, 2, 2), 0.43f, new Vector2(0, -0.05f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Guava(Clone)")
            {
                Debug.Log("G");
                Profiler.BeginSample("FruitChange");
                FruitChange(Apple, "Apple", new Vector3(2.5f, 2.5f, 2.5f), 0.52f, new Vector2(0, -0.09f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Apple(Clone)")
            {
                Debug.Log("App");
                Profiler.BeginSample("FruitChange");
                FruitChange(Grapefruit, "Grapefruit", new Vector3(3.5f, 3.5f, 3.5f), 0.75f, new Vector2(0, -0.16f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Grapefruit(Clone)")
            {
                Debug.Log("Gra");
                Profiler.BeginSample("FruitChange");
                FruitChange(Passionfruit, "Passionfruit", new Vector3(4, 4, 4), 0.88f, new Vector2(0, -0.18f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Passionfruit(Clone)")
            {
                Debug.Log("Pa");
                Profiler.BeginSample("FruitChange");
                FruitChange(Lucuma, "Lucuma", new Vector3(4.5f, 4.5f, 4.5f), 0.96f, new Vector2(0, -0.09f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Lucuma(Clone)")
            {
                Debug.Log("L");
                Profiler.BeginSample("FruitChange");
                FruitChange(Cloudberry, "Cloudberry", new Vector3(4.5f, 4.5f, 4.5f), 1, new Vector2(-0.05f, -0.1f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Cloudberry(Clone)")
            {
                Debug.Log("Clo");
                Profiler.BeginSample("FruitChange");
                FruitChange(Watermelon, "Watermelon", new Vector3(5.2f, 5.2f, 5.2f), 1.15f, new Vector2(0, -0.13f));
                Profiler.EndSample();
            }
            else if (gameObject.name == "Watermelon(Clone)")
            {
                Debug.Log("Wat");
                Destroy(gameObject);
            }

            foreach (var item in GameManager.instance.image)
            {
                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            GameManager.instance.isButtonChange = false;
        }
    }

    public void FruitChange(Sprite sprite, string name, Vector3 Scale, float Number, Vector2 Value)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        var oldCollider = gameObject.GetComponent<Collider2D>();
        Destroy(oldCollider);


        var newCollider = gameObject.AddComponent<CircleCollider2D>();
        newCollider.radius = Number;
        newCollider.offset = Value;


        gameObject.name = name + "(Clone)";
        gameObject.tag = name;
        gameObject.transform.GetChild(0).gameObject.transform.localScale = Scale;
    }

    IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
