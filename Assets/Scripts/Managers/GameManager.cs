using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    [SerializeField]
    private PoolController poolController;

    public PoolController PoolController => poolController;
}
