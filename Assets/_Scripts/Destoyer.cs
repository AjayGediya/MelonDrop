using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyer : MonoBehaviour
{
    private void Start()
    {
        // particals system destroy
        Destroy(this.gameObject,3f);
    }
}
