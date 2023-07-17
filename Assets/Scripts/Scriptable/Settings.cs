using UnityEngine;

using Plants;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    #region Singletion
    private static Settings _instance;

    public static Settings Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<Settings>("Data/Settings");

            return _instance;
        }
    }
    #endregion

    [SerializeField]
    private PlantConfiguration[] plants;

    public PlantConfiguration GetPlantByIndex(int index) => plants[index];

    public PlantConfiguration GetPlantByType(PlantType type)
    {
        foreach (var plant in plants)
            if (plant.type == type)
                return plant;

        return null;
    }
}