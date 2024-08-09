using UnityEngine;

public class Destoyer : MonoBehaviour
{
    private void Start()
    {
        // particals system destroy && text Destroy
        Destroy(this.gameObject,3f);
    }
}
