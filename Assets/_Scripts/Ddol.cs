using UnityEngine;

public class Ddol : MonoBehaviour
{
    public bool isDontDestroy = false;

    public void Awake()
    {
        if (isDontDestroy == false)
        {
            isDontDestroy = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
