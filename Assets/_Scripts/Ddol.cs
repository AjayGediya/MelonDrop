using UnityEngine;

public class Ddol : MonoBehaviour
{
    public bool isDon = false;

    public void Awake()
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
