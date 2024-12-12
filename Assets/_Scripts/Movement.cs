using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 startFruitPos, endFruitPos;

    [SerializeField] private Vector3 screenTouchSize;

    [SerializeField] private GameObject lineObject;

    [SerializeField] private float minValue = 0, maxValue = 0;

    [SerializeField] private Rigidbody2D rigidbody2D;

    public bool isSelect = false;

    private GameManager gameManager;

    public static Movement Instance;

    private void Awake()
    {
        Instance = this;
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        lineObject = GameObject.Find("Line");
        rigidbody2D = GetComponent<Rigidbody2D>();
        screenTouchSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private void Update()
    {
        HandleClickInput();
    }

    private void HandleClickInput()
    {
        if (gameManager.isPanelStartCheck == false)
        {
            if (gameManager.isGameOverCheck || isSelect || gameManager.isBomOption || gameManager.isButtonFirst2ObjectDestroy ||
            gameManager.isButtonReplce || gameManager.isBoxVibrateCheck || gameManager.isTimeCount || gameManager.isBoxVibrateCheck || gameManager.isExitCheck || gameManager.isHelpOption || gameManager.isSettingCheck || gameManager.isInternetCheck || gameManager.isUpdateCheck)
            {
                return;
            }

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (gameManager.fruitsParent.transform.position.y > pos.y)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lineObject.SetActive(true);
                    Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

                if (Input.GetMouseButton(0))
                {
                    endFruitPos = pos;
                    Vector3 differance = endFruitPos - startFruitPos;
                    differance.x = Mathf.Clamp(differance.x, minValue, maxValue);
                    UpdatePositionsCheck(differance.x);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    DeactivateGameOver();
                    MouseButtonUp();
                }
            }
        }
    }

    private void DeactivateGameOver()
    {
        gameManager.gameOverObject1.SetActive(false);
        gameManager.gameOverObject2.SetActive(false);
        gameManager.gameOverObject3.SetActive(false);
    }

    private void UpdatePositionsCheck(float clampedXpos)
    {
        transform.position = new Vector3(clampedXpos, transform.position.y, transform.position.z);
        lineObject.transform.position = new Vector3(clampedXpos, lineObject.transform.position.y, lineObject.transform.position.z);
    }

   
    private void MouseButtonUp()
    {
        GetComponent<Collider2D>().enabled = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SFruitSoundPlay();
        }
        gameManager.imageFruit.Add(gameObject);
        isSelect = true;
        gameObject.GetComponent<Movement>().enabled = false;
        lineObject.SetActive(false);
        rigidbody2D.freezeRotation = false;
        rigidbody2D.angularVelocity = Random.Range(-360, 360);

        StartCoroutine(ChangeGameOver());
        StartCoroutine(FruitChange());
    }

    private IEnumerator ChangeGameOver()
    {
        yield return new WaitForSeconds(0.7f);
        gameManager.gameOverObject1.SetActive(true);
        gameManager.gameOverObject2.SetActive(true);
        gameManager.gameOverObject3.SetActive(true);
    }

    private IEnumerator FruitChange()
    {
        yield return new WaitForSeconds(0.8f);
        lineObject.SetActive(true);
        lineObject.transform.position = Vector3.zero;

        gameManager.AfterNextImage();
        gameManager.NextFruitImageCall();
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
