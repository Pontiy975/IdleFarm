using System;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    #region Singleton

    private static PoolManager _instance;
    public static PoolManager Instance => _instance;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion


    [SerializeField]
    private Pool<PoolableObject>[] pools;


    private void Start()
    {
        for (int i = 0; i < pools.Length; i++)
            pools[i].InitPool(Instantiate);
    }


    public T GetItem<T>() where T : PoolableObject
    {
        Pool<PoolableObject> pool = GetPool<T>();

        if (pool != null)
        {
            PoolableObject item = pool.GetItem();
            item.OnSpawn();
            
            return item as T;
        }

        return default;
    }

    public void ReturnToPool<T>(T item) where T : PoolableObject
    {
        Pool<PoolableObject> pool = GetPool<T>();

        if (pool != null)
        {
            item.OnDespawn();
            pool.ReturnToPool(item);
        }
    }

    private Pool<PoolableObject> GetPool<T>()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            Pool<PoolableObject> pool = pools[i];

            if (pool.CheckItemType<T>())
                return pool;
        }

        return null;
    }
}


[Serializable]
public class Pool<T>
{
    [SerializeField] 
    private T prefab;

    [SerializeField]
    [Range(0, 100)]
    private int poolSize;

    private List<T> _pool = new List<T>();

    private Func<T, T> _instanceDelegate;

    private Transform _container;

    public int Count => _pool.Count;


    public void InitPool(Func<T, T> instantiateDelegate)
    {
        _container = new GameObject($"{prefab.GetType().Name}Pool").transform;
        _instanceDelegate = instantiateDelegate;
        
        for (int i = 0; i < poolSize; i++)
            AddItem(i);
    }

    private void AddItem(int number)
    {
        T item = _instanceDelegate(prefab);
        _pool.Add(item);

        MonoBehaviour obj = item as MonoBehaviour;

        obj.name += $" {number}";
        obj.transform.parent = _container;

        obj.gameObject.SetActive(false);
    }

    public T GetItem()
    {
        if (_pool.Count == 0) AddItem(0);
        T item = _pool[0];
        _pool.RemoveAt(0);

        return item;
    }

    public void ReturnToPool(T item)
    {
        _pool.SafeAdd(item);
    }

    public bool CheckItemType<S>()
    {
        return prefab != null && prefab.GetType() == typeof(S);
    }
}