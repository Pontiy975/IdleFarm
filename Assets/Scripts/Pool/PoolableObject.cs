using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }
}
