using UnityEngine;

public class Destoyer : MonoBehaviour
{
    public void Start()
    {
        // particals system destroy && text Destroy
        Destroy(this.gameObject,3f);
    }
}
