using System.Collections.Generic;
using UnityEngine;

using Plants;
using System.Collections;

public class Stack : MonoBehaviour
{
    private const float UnstackInterval = 0.2f;

    [SerializeField]
    private StackCrate[] crates;

    private List<PlantConfiguration> _plants = new List<PlantConfiguration>();

    private int _capacity = 3;

    private GameManager _gameManager;

    public bool IsFull => _plants.Count >= _capacity;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void AddPlant(PlantConfiguration plant)
    {
        _plants.Add(plant);
        StackCrate crate = crates[_plants.Count - 1];

        crate.gameObject.SetActive(true);
        crate.Show(plant.sprite);
    }

    public void Unstack(House house)
    {
        if (_plants.Count == 0) return;
        StartCoroutine(UnstackRoutine(house));
    }

    private IEnumerator UnstackRoutine(House house)
    {
        int count = _plants.Count;

        for (int i = 0; i < count; i++)
        {
            PlantConfiguration plant = _plants[^1];
            StackCrate crate = crates[_plants.Count - 1];

            house.Bounce();
            crate.JumpTo(house.Body);
            
            _plants.SafeRemovå(plant);
            _gameManager.ChangeMoney(plant.cost);

            yield return new WaitForSeconds(UnstackInterval);
        }
    }
}
