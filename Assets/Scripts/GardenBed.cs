using UnityEngine;

using Plants;


public class GardenBed : MonoBehaviour
{
    [SerializeField]
    private PlantType type;

    private Plant plant;

    private Transform _transform;
    private PlantsController _controller;


    private void Start()
    {
        _transform = transform;
        _controller = GameManager.Instance.PlantsController;
    }

    public bool IsEmpty { get; private set; } = true;

    public PlantType Type => type;

    public void Planting()
    {
        IsEmpty = false;

        plant = _controller.GetPlantFromPool(type);

        plant.gameObject.SetActive(true);

        plant.transform.SetParent(_transform);
        plant.transform.localPosition = Vector3.zero;
        plant.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);
    }
}
