using UnityEngine;

using Plants;

public class PoolController : MonoBehaviour
{
    private PoolManager _poolManager;
    
    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    public T GetFromPool<T>() where T : PoolableObject
    {
        return _poolManager.GetItem<T>();
    }

    public void ReturnToPool<T>(T item) where T : PoolableObject
    {
        _poolManager.ReturnToPool(item);
    }
}
