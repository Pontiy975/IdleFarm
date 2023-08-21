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


    [field: SerializeField]
    public UIManager UIManager { get; private set; }

    [field: SerializeField]
    public PoolController PoolController { get; private set; }

    [field: SerializeField]
    public House House { get; private set; }


    private int _money;

    public int Money => _money;

    private void Start()
    {
        Application.targetFrameRate = 60;

        UIManager.ChangeMoney(_money);
    }

    public void ChangeMoney(int value)
    {
        _money += value;
        UIManager.ChangeMoney(_money);
    }
}
