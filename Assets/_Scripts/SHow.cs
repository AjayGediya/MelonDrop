using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHow : MonoBehaviour
{
    public static SHow Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; } 
        DontDestroyOnLoad(gameObject); 
        Instance = this; 
    }

    private void Start()
    {
        GameManager.instance.OpenUpdateDialog();
    }
}
