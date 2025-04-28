using UnityEngine;

public class LaserAutoDestroy : MonoBehaviour
{
    public float lifeTime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
