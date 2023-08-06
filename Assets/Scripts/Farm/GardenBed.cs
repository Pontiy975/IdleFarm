using UnityEngine;

using Plants;


public class GardenBed : MonoBehaviour
{
    [SerializeField]
    private PlantType type;

    [SerializeField]
    private GameObject smokeFX;

    private Plant plant;

    private Transform _transform;
    private PoolController _controller;
    private Settings _settings;

    public bool ReadyToHarvest { get; private set; }


    private void Start()
    {
        _transform = transform;
        _controller = GameManager.Instance.PoolController;
        _settings = Settings.Instance;
    }

    public bool IsEmpty { get; private set; } = true;

    public PlantType Type => type;

    public void Planting()
    {
        IsEmpty = false;

        plant = GetPlant();

        plant.Init(_settings.GetPlantByType(type));
        plant.Planting(_transform);

        plant.OnGrown += Grown;
        PlaySmoke();
    }

    public PlantConfiguration Harvest()
    {
        PlantConfiguration config = plant.Config;

        plant.OnGrown -= Grown;

        ReadyToHarvest = false;
        IsEmpty = true;

        plant.ReturnToPool();

        PlaySmoke();

        return config;
    }


    private void Grown()
    {
        ReadyToHarvest = true;
    }

    private Plant GetPlant()
    {
        switch (type)
        {
            case PlantType.Carrot:
            default:
                return _controller.GetFromPool<Carrot>();
        }
    }

    private void DisableSmoke()
    {
        smokeFX.SetActive(false);
    }

    private void PlaySmoke()
    {
        smokeFX.SetActive(true);
        Invoke(nameof(DisableSmoke), 1f);
    }
}
