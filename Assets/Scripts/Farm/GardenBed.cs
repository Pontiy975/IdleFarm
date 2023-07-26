using UnityEngine;

using Plants;


public class GardenBed : MonoBehaviour
{
    [SerializeField]
    private PlantType type;

    private Plant plant;

    private Transform _transform;
    private PoolController _controller;


    private void Start()
    {
        _transform = transform;
        _controller = GameManager.Instance.PoolController;
    }

    public bool IsEmpty { get; private set; } = true;

    public PlantType Type => type;

    public void Planting()
    {
        IsEmpty = false;
        plant = _controller.GetPlantFromPool(type);
        plant.Planting(_transform);
    }
}
