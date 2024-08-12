using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 startPos, endPos;
    [SerializeField] private Vector3 screenSize;
    [SerializeField] private GameObject line;
    public bool isSelect = false;
    [SerializeField] private float min = 0, max = 0;
    [SerializeField] private Rigidbody2D rb;

    private GameManager gameManager;

    public static Movement Instance;

    private void Awake()
    {
        Instance = this;
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        line = GameObject.Find("Line");
        rb = GetComponent<Rigidbody2D>();
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private void Update()
    {
        Profiler.BeginSample("TouchInput");
        HandleTouchInput();
        Profiler.EndSample();
    }

    private void HandleTouchInput()
    {
        if (gameManager.isGameOver || isSelect || gameManager.isButtonOption || gameManager.isButtonFirst2Destroy ||
            gameManager.isButtonChange || gameManager.isButtonBoxVibrate)
        {
            return;
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (gameManager.FruitsParent.transform.position.y > pos.y)
        {
            if (Input.GetMouseButtonDown(0))
            { 
                line.SetActive(true);
                DeactivateGameOverObjects();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(mousePos);
                // startPos = pos;
            }

            if (Input.GetMouseButton(0))
            {
                DeactivateGameOverObjects();
                endPos = pos;
                Vector3 diff = endPos - startPos;
                diff.x = Mathf.Clamp(diff.x, min, max);
                UpdatePositions(diff.x);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnMouseButtonUp();
            }
        }
    }

    private void DeactivateGameOverObjects()
    {
        gameManager.GameOverObject1.SetActive(false);
        gameManager.GameOverObject2.SetActive(false);
        gameManager.GameOverObject3.SetActive(false);
    }

    private void UpdatePositions(float clampedX)
    {
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        line.transform.position = new Vector3(clampedX, line.transform.position.y, line.transform.position.z);
    }

    private void OnMouseButtonUp()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.FruitSoundPlay();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        gameManager.image.Add(gameObject);
        isSelect = true;
        line.SetActive(false);
        rb.freezeRotation = false;
        rb.angularVelocity = Random.Range(-360, 360);

        StartCoroutine(ChangeOver());
        StartCoroutine(ValueChange());
    }

    private IEnumerator ChangeOver()
    {
        yield return new WaitForSeconds(1f);
        gameManager.GameOverObject1.SetActive(true);
        gameManager.GameOverObject2.SetActive(true);
        gameManager.GameOverObject3.SetActive(true);
    }

    private IEnumerator ValueChange()
    {
        yield return new WaitForSeconds(1f);
        line.SetActive(true);
        line.transform.position = Vector3.zero;

        gameManager.AfterNextImageCall();
        gameManager.nextImage();
    }
}


//using System.Collections;
//using UnityEngine;
//using UnityEngine.Profiling;

//public class Movement : MonoBehaviour
//{
//    public Vector3 StartPos, EndPos;

//    public Vector3 ScreenSize;

//    public GameObject Line;

//    public bool isSelect = false;

//    public float Min = 0, Max = 0;

//    public static Movement Instance;

//    GameManager A = GameManager.instance;

//    public Rigidbody2D rb;

//    Vector3 Mouse;

//    public void Awake()
//    {
//        instance = this;
//    }

//    public void Start()
//    {
//        Line = GameObject.Find("Line");

//        rb = GetComponent<Rigidbody2D>();

//        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
//    }

//    public void Update()
//    {
//        Profiler.BeginSample("TouchInput");
//        TouchInput();
//        Profiler.EndSample();
//    }

//    public void TouchInput()
//    {
//        Vector3 pos;

//        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        if (GameManager.instance.FruitsParent.transform.position.y > pos.y)
//        {
//            if (Input.GetMouseButtonDown(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
//            {
//                Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                Line.SetActive(true);       
//                A.GameOverObject1.SetActive(false);
//                A.GameOverObject2.SetActive(false);
//                A.GameOverObject3.SetActive(false);
//                //StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            }

//            if (Input.GetMouseButton(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
//            {
//                A.GameOverObject1.SetActive(false);
//                A.GameOverObject2.SetActive(false);
//                A.GameOverObject3.SetActive(false);

//                EndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//                Vector3 diff = EndPos - StartPos;

//                diff.x = Mathf.Clamp(diff.x, Min, Max);

//                transform.position = new Vector3(diff.x, transform.position.y, transform.position.z);

//                Line.transform.position = new Vector3(diff.x, Line.transform.position.y, Line.transform.position.z);
//            }

//            if (Input.GetMouseButtonUp(0) && isSelect == false && A.isGameOver == false && A.isButtonOption == false && A.isButtonFirst2Destroy == false && A.isButtonChange == false && A.isButtonBoxVibrate == false)
//            {
//                // Debug.Log("Up");
//                if (SoundManager.Instance != null)
//                    SoundManager.Instance.FruitSoundPlay();
//                gameObject.transform.GetComponent<Collider2D>().enabled = true;
//                GameManager.instance.image.Add(gameObject);
//                isSelect = true;
//                Line.SetActive(false);
//                rb.bodyType = RigidbodyType2D.Dynamic;
//                rb.freezeRotation = false;
//                rb.angularVelocity = UnityEngine.Random.Range(-360, 360);
//                Profiler.BeginSample("valueChange");
//                StartCoroutine(valueChange());
//                Profiler.EndSample();
//                Profiler.BeginSample("ChangeOver");
//                StartCoroutine(ChangeOver());
//                Profiler.EndSample();
//            }
//        }
//    }


//    public IEnumerator ChangeOver()
//    {
//        yield return new WaitForSeconds(1f);
//        A.GameOverObject1.SetActive(true);
//        A.GameOverObject2.SetActive(true);
//        A.GameOverObject3.SetActive(true);
//    }

//    public IEnumerator valueChange()
//    {
//        yield return new WaitForSeconds(1);
//        Line.SetActive(true);
//        Line.transform.position = new Vector3(0, 0, 0);
//        Profiler.BeginSample("AfterNextImageCall");
//        A.AfterNextImageCall();
//        Profiler.EndSample();

//        Profiler.BeginSample("nextImage");
//        A.nextImage();
//        Profiler.EndSample();
//    }
//}
