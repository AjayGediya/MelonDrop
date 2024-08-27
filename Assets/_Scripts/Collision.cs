using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Profiling;

public class Collision : MonoBehaviour
{
    public GameObject Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

    public ParticleSystem Blue, GreenLitedark, GreenLite, GreenDark, Purple, OrangeDark, OrangeLite, Red, DarkYellow;

    public Transform ParticalParent;

    public Transform TextParent;

    public TextMeshPro textNumber;

    private TextMeshPro newtext;

    private SpriteRenderer spriteRenderer;

    private Collider2D Collide2D;

    public void Start()
    {
        ParticalParent = GameObject.Find("ParticalObject").transform;
        TextParent = GameObject.Find("TextObjects").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collide2D = GetComponent<Collider2D>();

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    public void HandleCollision(Collision2D collision)
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isFruit)
        {
            Profiler.BeginSample("ChekFruits");
            ChekFruits(collision);
            Profiler.EndSample();
        }
    }

    public void ChekFruits(Collision2D newcollision)
    {
        string currentTag = gameObject.tag;
        string collisionTag = newcollision.gameObject.tag;

        if (currentTag == collisionTag)
        {
            GameManager.instance.isFruit = true;
            switch (currentTag)
            {
                case "Strawberry":
                    PerformFruitAction("Strawberry + Apricot", OrangeLite, Apricot, 1, newcollision);
                    break;
                case "Apricot":
                    PerformFruitAction("Apricot + Blueberry", Blue, Blueberry, 2, newcollision);
                    break;
                case "Blueberry":
                    PerformFruitAction("Blueberry + Guava", GreenLite, Guava, 5, newcollision);
                    break;
                case "Guava":
                    PerformFruitAction("Guava + Apple", Red, Apple, 10, newcollision);
                    break;
                case "Apple":
                    PerformFruitAction("Apple + Grapefruit", OrangeDark, Grapefruit, 15, newcollision);
                    break;
                case "Grapefruit":
                    PerformFruitAction("Grapefruit + Passionfruit", Purple, Passionfruit, 20, newcollision);
                    break;
                case "Passionfruit":
                    PerformFruitAction("Passionfruit + Lucuma", GreenDark, Lucuma, 25, newcollision);
                    break;
                case "Lucuma":
                    PerformFruitAction("Lucuma + Cloudberry", DarkYellow, Cloudberry, 35, newcollision);
                    break;
                case "Cloudberry":
                    PerformFruitAction("Cloudberry + Watermelon", GreenLitedark, Watermelon, 40, newcollision);
                    break;
                case "Watermelon":
                    HandleFinalFruit(newcollision, GreenLitedark, 50);
                    break;
            }
        }
        GameManager.instance.ScoreText.text = GameManager.instance.ScoreValue.ToString();
    }

    public void PerformFruitAction(string logMessage, ParticleSystem particleEffect, GameObject nextFruit, int scoreValue, Collision2D newcollision)
    {
        Debug.Log(logMessage);
        Profiler.BeginSample("ParticalesEffect");
        ParticalesEffect(particleEffect, newcollision);
        Profiler.EndSample();

        Profiler.BeginSample("FruitChanges");
        FruitChanges(nextFruit, newcollision);
        Profiler.EndSample();

        Profiler.BeginSample("DestroyObject");
        DestroyObject(newcollision);
        Profiler.EndSample();

        GameManager.instance.ScoreValue += scoreValue;
        TextCreate(scoreValue,newcollision);
    }

    public void HandleFinalFruit(Collision2D newcollision, ParticleSystem particleEffect, int scoreValue)
    {
        Debug.Log("Watermelon");
        Profiler.BeginSample("ParticalesEffect");
        ParticalesEffect(particleEffect, newcollision);
        Profiler.EndSample();

        GameManager.instance.ScoreValue += scoreValue;
        TextCreate(scoreValue,newcollision);

        GameManager.instance.image.Remove(gameObject);
        GameManager.instance.image.Remove(newcollision.gameObject);

        Destroy(gameObject);
        Destroy(newcollision.gameObject);

        GameManager.instance.isFruit = false;
    }

    public void TextCreate(int value, Collision2D collision2DNew)
    {
        newtext = Instantiate(textNumber, TextParent);
        newtext.text = "+" + value;
        newtext.transform.position = collision2DNew.transform.position;

        Profiler.BeginSample("TextColorChange");
        StartCoroutine(TextColorChange());
        Profiler.EndSample();
    }

    public IEnumerator TextColorChange()
    {
        yield return new WaitForSeconds(1);
        newtext.DOFade(0, 1);
    }

    public void ParticalesEffect(ParticleSystem newparticle, Collision2D newcollision)
    {
        ParticleSystem particleSystem = Instantiate(newparticle, ParticalParent);
        particleSystem.transform.position = newcollision.transform.position;
    }

    public void DestroyObject(Collision2D newcollision)
    {
        Profiler.BeginSample("IsFruit");
        StartCoroutine(GameManager.instance.IsFruit());
        Profiler.EndSample();

        Debug.Log("GameObject Sprite And Collider False");
        spriteRenderer.enabled = false;
        newcollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("newCollision Sprite And Collider False");
        gameObject.GetComponent<Collider2D>().enabled = false;
        newcollision.gameObject.GetComponent<Collider2D>().enabled = false;

    }

    public void FruitChanges(GameObject fruits, Collision2D collsionnew)
    {
        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            Vibration.Vibrate(50);
        }

        GameObject newFruit = Instantiate(fruits);
        newFruit.transform.position = collsionnew.transform.position;
        newFruit.transform.SetParent(GameManager.instance.FruitsParent.transform);
        newFruit.GetComponent<Collider2D>().enabled = true;
        newFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SoundFruitMergePlay();
        }

        GameManager.instance.image.Add(newFruit);
        Movement.Instance.isSelect = true;

    }
}











//using System.Collections;
//using UnityEngine;
//using TMPro;
//using DG.Tweening;
//using UnityEngine.Profiling;

//public class Collision : MonoBehaviour
//{
//    public GameObject Blueberry, Apricot, Apple, Cloudberry, Grapefruit, Guava, Lucuma, Passionfruit, Watermelon;

//    public ParticleSystem Blue, Green, Green1, GreenDark, NewyBlue, Orange, Orange1, Red, Yellow;

//    public Transform ParticalParent;

//    public TextMeshPro textNumber;

//    public Transform TextParent;

//    TextMeshPro newtext;

//    private void Start()
//    {
//        ParticalParent = GameObject.Find("ParticalObject").GetComponent<Transform>();
//        TextParent = GameObject.Find("TextObjects").GetComponent<Transform>();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Profiler.BeginSample("ChekFruits");
//        ChekFruits(collision);
//        Profiler.EndSample();
//    }

//    private void OnCollisionStay2D(Collision2D collision)
//    {
//        Profiler.BeginSample("ChekFruits");ChekFruits
//        ChekFruits(collision);
//        Profiler.EndSample();
//    }

//    public void ChekFruits(Collision2D newcollisiton)
//    {
//        if (GameManager.instance.isGameOver == false)
//        {
//            if (newcollisiton.gameObject.tag == "Strawberry" && gameObject.tag == "Strawberry")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Strawberry + Apricot");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Orange);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Apricot);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 1;
//                    TextCreate(1);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Apricot" && gameObject.tag == "Apricot")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Apricot + Blueberry");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Blue);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Blueberry);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 2;
//                    TextCreate(2);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Blueberry" && gameObject.tag == "Blueberry")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Blueberry + Guava");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Green);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Guava);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 5;
//                    TextCreate(5);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Guava" && gameObject.tag == "Guava")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Guava + Apple");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Red);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Apple);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 10;
//                    TextCreate(10);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Apple" && gameObject.tag == "Apple")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Apple + Grapefruit");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Orange1);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Grapefruit);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 15;
//                    TextCreate(15);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Grapefruit" && gameObject.tag == "Grapefruit")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Grapefruit + Passionfruit");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(NewyBlue);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Passionfruit);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 20;
//                    TextCreate(20);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Passionfruit" && gameObject.tag == "Passionfruit")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Passionfruit + Lucuma");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(GreenDark);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Lucuma);
//                    Profiler.EndSample();
//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 25;
//                    TextCreate(25);
//                }
//            }

//            else if (newcollisiton.gameObject.tag == "Lucuma" && gameObject.tag == "Lucuma")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Lucuma + Cloudberry");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Yellow);
//                    Profiler.EndSample();

//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Cloudberry);
//                    Profiler.EndSample();

//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 35;
//                    TextCreate(35);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Cloudberry" && gameObject.tag == "Cloudberry")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Cloudberry + Watermelon");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Green1);
//                    Profiler.EndSample();

//                    GameManager.instance.isFruit = true;
//                    Profiler.BeginSample("FruitChanges");
//                    FruitChanges(Watermelon);
//                    Profiler.EndSample();

//                    Profiler.BeginSample("DestroyObject");
//                    DestroyObject(newcollisiton);
//                    Profiler.EndSample();
//                    GameManager.instance.ScoreValue += 40;
//                    TextCreate(40);
//                }
//            }
//            else if (newcollisiton.gameObject.tag == "Watermelon" && gameObject.tag == "Watermelon")
//            {
//                if (!GameManager.instance.isFruit)
//                {
//                    Debug.Log("Watermelon");
//                    Profiler.BeginSample("ParticalesEffect");
//                    ParticalesEffect(Green1);
//                    Profiler.EndSample();
//                    GameManager.instance.isFruit = true;
//                    GameManager.instance.ScoreValue += 50;
//                    TextCreate(50);
//                    GameManager.instance.image.Remove(gameObject);
//                    GameManager.instance.image.Remove(newcollisiton.gameObject);
//                    Destroy(gameObject);
//                    Destroy(newcollisiton.gameObject);
//                    GameManager.instance.isFruit = false;
//                }
//            }
//            GameManager.instance.ScoreText.text = GameManager.instance.ScoreValue.ToString();
//        }
//    }

//    public void TextCreate(int value)
//    {
//        newtext = Instantiate(textNumber);
//        newtext.transform.SetParent(TextParent);
//        newtext.text = "+" + value.ToString();
//        newtext.transform.position = gameObject.transform.position;
//        Profiler.BeginSample("TextColorChange");
//        StartCoroutine(TextColorChange());
//        Profiler.EndSample();
//    }

//    public IEnumerator TextColorChange()
//    {
//        newtext.DOFade(0, 1);
//        yield return new WaitForSeconds(1);
//    }

//    public void ParticalesEffect(ParticleSystem newparticle)
//    {
//        ParticleSystem particleSystem = Instantiate(newparticle);
//        particleSystem.transform.SetParent(ParticalParent);
//        particleSystem.transform.position = gameObject.transform.position;
//    }

//    public void DestroyObject(Collision2D newcollision)
//    {
//        Profiler.BeginSample("IsFruit");
//        StartCoroutine(GameManager.instance.IsFruit());
//        Profiler.EndSample();
//        gameObject.GetComponent<SpriteRenderer>().enabled = false;
//        gameObject.GetComponent<Collider2D>().enabled = false;
//        newcollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
//        newcollision.gameObject.GetComponent<Collider2D>().enabled = false;
//    }

//    public void FruitChanges(GameObject fruits)
//    {

//        // Debug.Log("vibrate_Fruit");
//        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
//        {
//            Vibration.Vibrate(50);
//        }
//        GameObject a = Instantiate(fruits);
//        if (SoundManager.Instance != null)
//            SoundManager.Instance.SoundFruitMergePlay();
//        a.transform.GetComponent<Collider2D>().enabled = true;
//        GameManager.instance.image.Add(a);
//        Movement.instance.isSelect = true;
//        a.transform.position = gameObject.transform.position;
//        a.transform.SetParent(GameManager.instance.FruitsParent.transform);
//        a.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
//    }
//}