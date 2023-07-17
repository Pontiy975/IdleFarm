using UnityEngine;

using Plants;

public class PlantsController : MonoBehaviour
{
    private PoolManager _poolManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    public Plant GetPlantFromPool(PlantType type)
    {
        switch (type)
        {
            case PlantType.Carrot:
            default:
                return _poolManager.GetItem<Carrot>();
        }
    }
}
