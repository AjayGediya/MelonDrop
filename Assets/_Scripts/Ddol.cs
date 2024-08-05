using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddol : MonoBehaviour
{
    public bool isDon = false;

    private void Awake()
    {
        if (isDon == false)
        {
            isDon = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
