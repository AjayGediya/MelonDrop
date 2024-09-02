using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class FruitTouch : MonoBehaviour
{
    [SerializeField] private Sprite blueberry, apricot, apple, cloudberry, grapefruit, guava, lucuma, passionfruit, watermelon;

    [SerializeField] private ParticleSystem particle;

    public string Fruit;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isChangeOneTime == false)
        {
            GameManager.instance.isChangeOneTime = true;
            Debug.Log("Click With Bool " + GameManager.instance.isChangeOneTime);

            if (gameManager.isButtonOption && !gameManager.isButtonChange && !gameManager.isButtonFirst2Destroy && !gameManager.isButtonBoxVibrate)
            {
                HandleBomAction();
            }
            else if (gameManager.isButtonChange && !gameManager.isButtonFirst2Destroy && !gameManager.isButtonBoxVibrate && !gameManager.isButtonOption)
            {
                HandleChangeAction();
            }
        }
        else
        {
            Debug.Log("NotClick With Bool " + GameManager.instance.isChangeOneTime);
        }
    }

    private void HandleBomAction()
    {
        Debug.Log("Bom");
        Debug.Log(gameObject.name);

        InstantiateAndAttachParticle();

        Profiler.BeginSample("DestroyFruit");
        Destroy(gameObject, 0.5f);
        Profiler.EndSample();

        gameManager.image.Remove(gameObject);

        DeactivateAllChildObjects();

        gameManager.isButtonOption = false;
       // gameManager.isBom = false;

        gameManager.VibrateBtn.GetComponent<Button>().interactable = true;
        gameManager.First2Destroybtn.GetComponent<Button>().interactable = true;
        gameManager.BomBtn.GetComponent<Button>().interactable = true;
        gameManager.ChangeBtn.GetComponent<Button>().interactable = true;
    }

    private void HandleChangeAction()
    {
        //Debug.Log("Change");
        Fruit = gameObject.name;
        //Debug.Log(Fruit + "FruitsChange");

        Profiler.BeginSample("FruitChange");
        switch (Fruit)
        {
            case "Strawberry(Clone)":
                FruitChange(apricot, "Apricot", new Vector3(0.15f, 0.15f, 0.15f), 0.27f, new Vector2(0, -0.03f));//done
                Debug.Log("apricot");
                break;
            case "Apricot(Clone)":
                FruitChange(blueberry, "Blueberry", new Vector3(0.14f, 0.14f, 0.14f), 0.3f, new Vector2(-0.03f, -0.04f));//done
                Debug.Log("blueberry");
                break;
            case "Blueberry(Clone)":
                FruitChange(guava, "Guava", new Vector3(0.2f, 0.2f, 0.2f), 0.34f, new Vector2(0, -0.01f));//done
                Debug.Log("guava");
                break;
            case "Guava(Clone)":
                FruitChange(apple, "Apple", new Vector3(0.2f, 0.2f, 0.2f), 0.34f, new Vector2(0, -0.08f));//done
                Debug.Log("apple");
                break;
            case "Apple(Clone)":
                FruitChange(grapefruit, "Grapefruit", new Vector3(0.3f, 0.3f, 0.3f), 0.5f, new Vector2(0, 0));//done
                Debug.Log("grapefruit");
                break;
            case "Grapefruit(Clone)":
                FruitChange(passionfruit, "Passionfruit", new Vector3(0.35f, 0.35f, 0.35f), 0.57f, new Vector2(0, -0.06f));//done
                Debug.Log("passionfruit");
                break;
            case "Passionfruit(Clone)":
                FruitChange(lucuma, "Lucuma", new Vector3(0.35f, 0.35f, 0.35f), 0.72f, new Vector2(0, 0));//done
                Debug.Log("lucuma");
                break;
            case "Lucuma(Clone)":
                FruitChange(cloudberry, "Cloudberry", new Vector3(0.55f, 0.55f, 0.55f), 0.95f, new Vector2(-0.04f, -0.15f));//done
                Debug.Log("cloudberry");
                break;
            case "Cloudberry(Clone)":
                FruitChange(watermelon, "Watermelon", new Vector3(0.7f, 0.7f, 0.7f), 1.18f, new Vector2(0, -0.05f));
                Debug.Log("watermelon");
                break;
            case "Watermelon(Clone)":
                gameManager.image.Remove(gameObject);
                Destroy(gameObject);
                gameManager.isBoxVibrate = false;
                gameManager.isButtonFirst2Destroy = false;
                gameManager.isButtonChange = false;
                gameManager.isButtonOption = false;
                gameManager.VibrateBtn.GetComponent<Button>().interactable = true;
                gameManager.First2Destroybtn.GetComponent<Button>().interactable = true;
                gameManager.BomBtn.GetComponent<Button>().interactable = true;
                gameManager.ChangeBtn.GetComponent<Button>().interactable = true;
                break;
        }
        Profiler.EndSample();

        DeactivateAllChildObjects();

        StartCoroutine(ChangeBool());
    }

    public IEnumerator ChangeBool()
    {
        yield return new WaitForSeconds(0.2f);
        gameManager.isButtonChange = false;
    }

    private void InstantiateAndAttachParticle()
    {
        ParticleSystem particleSystem = Instantiate(particle);
        particleSystem.transform.position = transform.position;
        particleSystem.transform.SetParent(transform);
    }

    private void DeactivateAllChildObjects()
    {
        foreach (var item in gameManager.image)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void FruitChange(Sprite newSprite, string newName, Vector3 newScale, float newRadius, Vector2 newOffset)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
        Debug.Log("Sprite Change");

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        var oldCollider = GetComponent<Collider2D>();
        Destroy(oldCollider);
        Debug.Log("old collider distroy");

        var newCollider = gameObject.AddComponent<CircleCollider2D>();
        newCollider.radius = newRadius;
        newCollider.offset = newOffset;
        Debug.Log("new collider ad");

        gameObject.name = $"{newName}(Clone)";
        gameObject.tag = newName;
        Debug.Log("name And Tag Change");

        var childTransform = transform.GetChild(0).transform;
        childTransform.localScale = newScale;
        Debug.Log("Child No Scle Bdle");

        gameObject.GetComponent<Collision>().Apricot = GameManager.instance.AllFruit[1];
        gameObject.GetComponent<Collision>().Blueberry = GameManager.instance.AllFruit[2];
        gameObject.GetComponent<Collision>().Guava = GameManager.instance.AllFruit[3];
        gameObject.GetComponent<Collision>().Apple = GameManager.instance.AllFruit[4];
        gameObject.GetComponent<Collision>().Grapefruit = GameManager.instance.AllFruit[5];
        gameObject.GetComponent<Collision>().Passionfruit = GameManager.instance.AllFruit[6];
        gameObject.GetComponent<Collision>().Lucuma = GameManager.instance.AllFruit[7];
        gameObject.GetComponent<Collision>().Cloudberry = GameManager.instance.AllFruit[8];
        gameObject.GetComponent<Collision>().Watermelon = GameManager.instance.AllFruit[9];

        gameManager.VibrateBtn.GetComponent<Button>().interactable = true;
        gameManager.First2Destroybtn.GetComponent<Button>().interactable = true;
        gameManager.BomBtn.GetComponent<Button>().interactable = true;
        gameManager.ChangeBtn.GetComponent<Button>().interactable = true;

        StartCoroutine(ChangeBoolforChnages());
    }

    IEnumerator ChangeBoolforChnages()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.isChangeOneTime = false;
        Debug.Log("Click With Bool false" + GameManager.instance.isChangeOneTime);
    }
}








//using System.Collections;
//using UnityEngine;
//using UnityEngine.Profiling;

//public class FruitTouch : MonoBehaviour
//{
//    public Sprite Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

//    public ParticleSystem particle;

//    GameObject A;

//    Vector3 POS;

//    public void OnMouseDown()
//    {
//        if (GameManager.instance.isButtonOption == true && GameManager.instance.isButtonChange == false && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false)
//        {
//            Debug.Log("Bom");
//            Debug.Log(gameObject.name);
//            ParticleSystem particleSystem = Instantiate(particle);
//            particleSystem.transform.position = gameObject.transform.position;
//            particleSystem.transform.SetParent(gameObject.transform);
//            Profiler.BeginSample("DestroyFruit");
//            Destroy(gameObject,0.5f);
//            Profiler.EndSample();
//            GameManager.instance.image.Remove(gameObject);

//            foreach (var item in GameManager.instance.image)
//            {
//                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
//            }
//            GameManager.instance.isButtonOption = false;
//            GameManager.instance.isBom = false;
//        }


//        if (GameManager.instance.isButtonChange == true && GameManager.instance.isButtonFirst2Destroy == false && GameManager.instance.isButtonBoxVibrate == false && GameManager.instance.isButtonOption == false)
//        {
//            Debug.Log("Change");
//            Debug.Log(gameObject.name);

//            if (gameObject.name == "Strawberry(Clone)")
//            {
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Apricot, "Apricot", new Vector3(1, 1, 1), 0.228f, new Vector2(0, -0.056f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Apricot(Clone)")
//            {
//                Debug.Log("Apr");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Blueberry, "Blueberry", new Vector3(1.3f, 1.3f, 1.3f), 0.32f, new Vector2(-0.06f, -0.09f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Blueberry(Clone)")
//            {
//                Debug.Log("B");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Guava, "Guava", new Vector3(2, 2, 2), 0.43f, new Vector2(0, -0.05f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Guava(Clone)")
//            {
//                Debug.Log("G");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Apple, "Apple", new Vector3(2.5f, 2.5f, 2.5f), 0.52f, new Vector2(0, -0.09f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Apple(Clone)")
//            {
//                Debug.Log("App");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Grapefruit, "Grapefruit", new Vector3(3.5f, 3.5f, 3.5f), 0.75f, new Vector2(0, -0.16f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Grapefruit(Clone)")
//            {
//                Debug.Log("Gra");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Passionfruit, "Passionfruit", new Vector3(4, 4, 4), 0.88f, new Vector2(0, -0.18f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Passionfruit(Clone)")
//            {
//                Debug.Log("Pa");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Lucuma, "Lucuma", new Vector3(4.5f, 4.5f, 4.5f), 0.96f, new Vector2(0, -0.09f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Lucuma(Clone)")
//            {
//                Debug.Log("L");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Cloudberry, "Cloudberry", new Vector3(4.5f, 4.5f, 4.5f), 1, new Vector2(-0.05f, -0.1f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Cloudberry(Clone)")
//            {
//                Debug.Log("Clo");
//                Profiler.BeginSample("FruitChange");
//                FruitChange(Watermelon, "Watermelon", new Vector3(5.2f, 5.2f, 5.2f), 1.15f, new Vector2(0, -0.13f));
//                Profiler.EndSample();
//            }
//            else if (gameObject.name == "Watermelon(Clone)")
//            {
//                Debug.Log("Wat");
//                Destroy(gameObject);
//            }

//            foreach (var item in GameManager.instance.image)
//            {
//                item.gameObject.transform.GetChild(0).gameObject.SetActive(false);
//            }
//            GameManager.instance.isButtonChange = false;
//        }
//    }

//    public void FruitChange(Sprite sprite, string name, Vector3 Scale, float Number, Vector2 Value)
//    {
//        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

//        var oldCollider = gameObject.GetComponent<Collider2D>();
//        Destroy(oldCollider);


//        var newCollider = gameObject.AddComponent<CircleCollider2D>();
//        newCollider.radius = Number;
//        newCollider.offset = Value;


//        gameObject.name = name + "(Clone)";
//        gameObject.tag = name;
//        gameObject.transform.GetChild(0).gameObject.transform.localScale = Scale;
//    }

//    IEnumerator DestroyFruit()
//    {
//        yield return new WaitForSeconds(0.3f);
//        Destroy(gameObject);
//    }
//}
