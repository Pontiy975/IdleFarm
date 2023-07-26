using System.Collections.Generic;
using UnityEngine;

using Plants;

public class Stack : MonoBehaviour
{
    [SerializeField]
    private StackCrate[] crates;

    private List<PlantConfiguration> _plants = new List<PlantConfiguration>();

    private int _capacity = 3;

    public bool IsFull => _plants.Count >= _capacity;

    public void AddPlant(PlantConfiguration plant)
    {
        _plants.Add(plant);
        StackCrate crate = crates[_plants.Count - 1];

        crate.gameObject.SetActive(true);
        crate.Show(plant.sprite);
    }
}
