using System;
using System.Collections.Generic;
using UnityEngine;

using Plants;


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
    private Pool<Carrot> carrotPool = new Pool<Carrot>(Instantiate);


    private void Start()
    {
        carrotPool.InitPool();
    }


    public T GetItem<T>()
    {
        switch (typeof(T).ToString())
        {
            case "CarrotInstance":
            default:
                return (T)(object)carrotPool.GetItem();
        }
    }

    public void ReturnToPool<T>(T item)
    {
        switch (typeof(T).ToString())
        {
            case "CarrotInstance":
            default:
                carrotPool.ReturnToPool(item as Carrot);

                break;
        }
    }
}


[Serializable]
public class Pool<T>
{
    [SerializeField] private T prefab;

    [SerializeField] private int poolSize;

    private List<T> _pool = new List<T>();

    private Func<T, T> _instanceDelegate;

    private Transform _container;

    public int Count => _pool.Count;

    public Pool(Func<T, T> instantiateDelegate)
    {
        _instanceDelegate = instantiateDelegate;
    }

    public void InitPool()
    {
        _container = new GameObject($"{typeof(T).Name}Pool").transform;

        for (int i = 0; i < poolSize; i++)
        {
            AddItem(i);
        }
    }

    private T AddItem(int number)
    {
        T item = _instanceDelegate(prefab);
        _pool.Add(item);

        if (item is MonoBehaviour mono)
        {
            mono.name += $" {number}";
            mono.transform.parent = _container;
            mono.gameObject.SetActive(false);
        }
        else if (item is GameObject obj)
        {
            obj.name += $" {number}";
            obj.transform.parent = _container;
            obj.SetActive(false);
        }

        return item;
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
        _pool.Add(item);
        if (item is MonoBehaviour mono) mono.gameObject.SetActive(false);
        else if (item is GameObject obj) obj.SetActive(false);
    }
}