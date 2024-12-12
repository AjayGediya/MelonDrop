using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    public GameObject blueberry, apricot, apple, cloudberry, grapefruit, guava, lucuma, passionfruit, watermelon;

    public ParticleSystem blue, greenLitedark, greenLite, greenDark, purple, orangeDark, orangeLite, red, darkYellow;

    public Transform particalParent;

    public Transform textParent;

    public TextMeshPro textCount;

    private TextMeshPro newText;

    private SpriteRenderer _SpriteRenderer;

    private Collider2D collide2D;

    public void Start()
    {
        particalParent = GameObject.Find("ParticalObject").transform;

        textParent = GameObject.Find("TextObjects").transform;

        _SpriteRenderer = GetComponent<SpriteRenderer>();

        collide2D = GetComponent<Collider2D>();

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleMergeCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        HandleMergeCollision(collision);
    }

    public void HandleMergeCollision(Collision2D collision)
    {
        if (!GameManager.instance.isGameOverCheck && !GameManager.instance.isFruitObject)
        {
            ChekFruitsMergeCollide(collision);
        }
    }

    public void ChekFruitsMergeCollide(Collision2D newCollision)
    {
        string currentFruitTag = gameObject.tag;

        string collisionFruitTag = newCollision.gameObject.tag;

        if (currentFruitTag == collisionFruitTag)
        {
            GameManager.instance.isFruitObject = true;

            switch (currentFruitTag)
            {
                case "Strawberry":
                    FruitAction("Strawberry + Apricot", orangeLite, apricot, 1, newCollision);
                    break;
                case "Apricot":
                    FruitAction("Apricot + Blueberry", blue, blueberry, 2, newCollision);
                    break;
                case "Blueberry":
                    FruitAction("Blueberry + Guava", greenLite, guava, 5, newCollision);
                    break;
                case "Guava":
                    FruitAction("Guava + Apple", red, apple, 10, newCollision);
                    break;
                case "Apple":
                    FruitAction("Apple + Grapefruit", orangeDark, grapefruit, 15, newCollision);
                    break;
                case "Grapefruit":
                    FruitAction("Grapefruit + Passionfruit", purple, passionfruit, 20, newCollision);
                    break;
                case "Passionfruit":
                    FruitAction("Passionfruit + Lucuma", greenDark, lucuma, 25, newCollision);
                    break;
                case "Lucuma":
                    FruitAction("Lucuma + Cloudberry", darkYellow, cloudberry, 35, newCollision);
                    break;
                case "Cloudberry":
                    FruitAction("Cloudberry + Watermelon", greenLitedark, watermelon, 40, newCollision);
                    break;
                case "Watermelon":
                    FinalFruit(newCollision, greenLitedark, 50);
                    break;
            }
        }
        GameManager.instance.scoreText.text = GameManager.instance.scoreValue.ToString();
    }

    public void FruitAction(string message, ParticleSystem particlesEffect, GameObject nextCollideFruit, int score, Collision2D newCollision)
    {
        ParticalesEffectCreate(particlesEffect, newCollision);

        FruitObjectChanges(nextCollideFruit, newCollision);

        DestroyFruitObject(newCollision);

        GameManager.instance.scoreValue += score;

        TextObjectCreate(score, newCollision);

        gameObject.GetComponent<Movement>().enabled = false;
    }

    public void FinalFruit(Collision2D newCollision, ParticleSystem particlesEffect, int score)
    {
        ParticalesEffectCreate(particlesEffect, newCollision);

        GameManager.instance.scoreValue += score;

        TextObjectCreate(score, newCollision);

        GameManager.instance.imageFruit.Remove(gameObject);

        GameManager.instance.imageFruit.Remove(newCollision.gameObject);

        Destroy(gameObject);

        Destroy(newCollision.gameObject);

        GameManager.instance.isBoxVibrateCheck = false;
        GameManager.instance.isButtonFirst2ObjectDestroy = false;
        GameManager.instance.isButtonReplce = false;
        GameManager.instance.isBomOption = false;
        GameManager.instance.isFruitObject = false;

        GameManager.instance.AllBtnTrue();
    }

    public void TextObjectCreate(int values, Collision2D collision2D)
    {
        newText = Instantiate(textCount, textParent);

        newText.text = "+" + values;

        newText.transform.position = collision2D.transform.position;

        StartCoroutine(TextObjectColorChange());
    }

    public IEnumerator TextObjectColorChange()
    {
        yield return new WaitForSeconds(0.5f);

        newText.DOFade(0, 1);
    }

    public void ParticalesEffectCreate(ParticleSystem newParticle, Collision2D newCollision)
    {
        ParticleSystem particle_system = Instantiate(newParticle, particalParent);

        particle_system.transform.position = newCollision.transform.position;
    }

    public void DestroyFruitObject(Collision2D newCollision)
    {
        StartCoroutine(GameManager.instance.IsFruitCheckBool());

        _SpriteRenderer.enabled = false;

        newCollision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        gameObject.GetComponent<Collider2D>().enabled = false;

        newCollision.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void FruitObjectChanges(GameObject fruitsObject, Collision2D collsionNew)
    {
        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            Vibration.Vibrate(50);
        }

        GameObject newFruitObject = Instantiate(fruitsObject);

        newFruitObject.transform.position = collsionNew.transform.position;

        newFruitObject.transform.SetParent(GameManager.instance.fruitsParent.transform);

        newFruitObject.GetComponent<Collider2D>().enabled = true;

        newFruitObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SFruitMergePlay();
        }

        GameManager.instance.imageFruit.Add(newFruitObject);

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