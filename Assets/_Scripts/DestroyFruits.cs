using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFruits : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject,4f);
    }
}
