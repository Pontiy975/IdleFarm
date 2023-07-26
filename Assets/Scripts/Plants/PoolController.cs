using UnityEngine;

using Plants;

public class PoolController : MonoBehaviour
{
    private PoolManager _poolManager;
    private Settings _settings;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _settings = Settings.Instance;
    }

    public Plant GetPlantFromPool(PlantType type)
    {
        Plant plant;

        PlantConfiguration config = _settings.GetPlantByType(type);

        switch (type)
        {
            case PlantType.Carrot:
            default:
                plant = _poolManager.GetItem<Carrot>();
                break;
        }

        plant.Init(config);
        return plant;
    }
}
