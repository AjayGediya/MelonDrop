using System.Collections;
using UnityEngine;

public class FruitTouch : MonoBehaviour
{
    [SerializeField] private Sprite blueBerry, apricotFruit, appleFruit, cloudBerry, grapeFruit, guavaFruit, lucumaFruit, passionFruit, watermelonFruit;

    [SerializeField] private ParticleSystem particleSystem;

    public string fruit;

    private GameManager gameManagerInstance;

    private void Start()
    {
        gameManagerInstance = GameManager.instance;
    }

    private void OnMouseDown()
    {
        if (gameManagerInstance.isChangeOneTimeCheck == false)
        {
            gameManagerInstance.isChangeOneTimeCheck = true;

            if (gameManagerInstance.isBomOption && !gameManagerInstance.isButtonReplce && !gameManagerInstance.isButtonFirst2ObjectDestroy && !gameManagerInstance.isBoxVibrateCheck)
            {
                HandleBomActionEffect();
            }
            else if (gameManagerInstance.isButtonReplce && !gameManagerInstance.isButtonFirst2ObjectDestroy && !gameManagerInstance.isBoxVibrateCheck && !gameManagerInstance.isBomOption)
            {
                HandleChangeActionEffect();
            }
        }
        else
        {
            Debug.Log("NotClick With Bool " + gameManagerInstance.isChangeOneTimeCheck);
        }
    }

    private void HandleBomActionEffect()
    {
        Debug.Log("Bom");
        Debug.Log(gameObject.name);

        InstantiateParticle();

        Destroy(gameObject, 0.5f);

        gameManagerInstance.imageFruit.Remove(gameObject);

        DeactivateFruitAllChildObjects();

        gameManagerInstance.isChangeOneTimeCheck = true;

        gameManagerInstance.AllBtnTrue();

        StartCoroutine(ChangeTimeforBomAply());
    }

    public IEnumerator ChangeTimeforBomAply()
    {
        yield return new WaitForSeconds(0.2f);
        gameManagerInstance.isBomOption = false;
        gameManagerInstance.isTimerRunning = true;
    }

    private void HandleChangeActionEffect()
    {
        fruit = gameObject.name;

        switch (fruit)
        {
            case "Strawberry(Clone)":
                FruitReplce(apricotFruit, "Apricot", new Vector3(0.15f, 0.15f, 0.15f), 0.27f, new Vector2(0, -0.03f));//done
                Debug.Log("apricot");
                break;
            case "Apricot(Clone)":
                FruitReplce(blueBerry, "Blueberry", new Vector3(0.14f, 0.14f, 0.14f), 0.3f, new Vector2(-0.03f, -0.04f));//done
                Debug.Log("blueberry");
                break;
            case "Blueberry(Clone)":
                FruitReplce(guavaFruit, "Guava", new Vector3(0.2f, 0.2f, 0.2f), 0.34f, new Vector2(0, -0.01f));//done
                Debug.Log("guava");
                break;
            case "Guava(Clone)":
                FruitReplce(appleFruit, "Apple", new Vector3(0.2f, 0.2f, 0.2f), 0.34f, new Vector2(0, -0.08f));//done
                Debug.Log("apple");
                break;
            case "Apple(Clone)":
                FruitReplce(grapeFruit, "Grapefruit", new Vector3(0.3f, 0.3f, 0.3f), 0.5f, new Vector2(0, 0));//done
                Debug.Log("grapefruit");
                break;
            case "Grapefruit(Clone)":
                FruitReplce(passionFruit, "Passionfruit", new Vector3(0.35f, 0.35f, 0.35f), 0.57f, new Vector2(0, -0.06f));//done
                Debug.Log("passionfruit");
                break;
            case "Passionfruit(Clone)":
                FruitReplce(lucumaFruit, "Lucuma", new Vector3(0.35f, 0.35f, 0.35f), 0.72f, new Vector2(0, 0));//done
                Debug.Log("lucuma");
                break;
            case "Lucuma(Clone)":
                FruitReplce(cloudBerry, "Cloudberry", new Vector3(0.55f, 0.55f, 0.55f), 0.95f, new Vector2(-0.04f, -0.15f));//done
                Debug.Log("cloudberry");
                break;
            case "Cloudberry(Clone)":
                FruitReplce(watermelonFruit, "Watermelon", new Vector3(0.7f, 0.7f, 0.7f), 1.18f, new Vector2(0, -0.05f));
                Debug.Log("watermelon");
                break;
            case "Watermelon(Clone)":
                gameManagerInstance.imageFruit.Remove(gameObject);
                Destroy(gameObject);
                gameManagerInstance.isBoxVibrateCheck = false;
                gameManagerInstance.isButtonFirst2ObjectDestroy = false;
                gameManagerInstance.isButtonReplce = false;
                gameManagerInstance.isBomOption = false;
                gameManagerInstance.AllBtnTrue();
                gameManagerInstance.isChangeOneTimeCheck = true;
                gameManagerInstance.isTimerRunning = true;
                break;
        }

        DeactivateFruitAllChildObjects();

        StartCoroutine(ChangeButtonBool());
    }

    public IEnumerator ChangeButtonBool()
    {
        yield return new WaitForSeconds(0.2f);
        gameManagerInstance.isButtonReplce = false;
    }

    private void InstantiateParticle()
    {
        ParticleSystem particle_System = Instantiate(particleSystem);
        particle_System.transform.position = transform.position;
        particle_System.transform.SetParent(transform);
    }

    private void DeactivateFruitAllChildObjects()
    {
        foreach (var item in gameManagerInstance.imageFruit)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void FruitReplce(Sprite newFruitSprite, string newFruitName, Vector3 fruitScale, float fruitRadius, Vector2 fruitOffset)
    {
        var fruitspriteRenderer = GetComponent<SpriteRenderer>();
        fruitspriteRenderer.sprite = newFruitSprite;

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        var fruitoldCollider = GetComponent<Collider2D>();
        Destroy(fruitoldCollider);

        var newFruitCollider = gameObject.AddComponent<CircleCollider2D>();
        newFruitCollider.radius = fruitRadius;
        newFruitCollider.offset = fruitOffset;

        gameObject.name = $"{newFruitName}(Clone)";
        gameObject.tag = newFruitName;

        var fruitchildTransform = transform.GetChild(0).transform;
        fruitchildTransform.localScale = fruitScale;

        gameObject.GetComponent<Collision>().apricot = gameManagerInstance.allFruit[1];
        gameObject.GetComponent<Collision>().blueberry = gameManagerInstance.allFruit[2];
        gameObject.GetComponent<Collision>().guava = gameManagerInstance.allFruit[3];
        gameObject.GetComponent<Collision>().apple = gameManagerInstance.allFruit[4];
        gameObject.GetComponent<Collision>().grapefruit = gameManagerInstance.allFruit[5];
        gameObject.GetComponent<Collision>().passionfruit = gameManagerInstance.allFruit[6];
        gameObject.GetComponent<Collision>().lucuma = gameManagerInstance.allFruit[7];
        gameObject.GetComponent<Collision>().cloudberry = gameManagerInstance.allFruit[8];
        gameObject.GetComponent<Collision>().watermelon = gameManagerInstance.allFruit[9];

        gameManagerInstance.AllBtnTrue();
        gameManagerInstance.isTimerRunning = true;

        StartCoroutine(ChangeBoolforFruitChnages());
    }

    IEnumerator ChangeBoolforFruitChnages()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.isChangeOneTimeCheck = true;
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
