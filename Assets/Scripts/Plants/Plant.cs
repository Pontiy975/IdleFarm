using System;
using UnityEngine;

namespace Plants
{
    public enum PlantType
    {
        Wheat,
        Corn,
        Tomato
    }

    [Serializable]
    public class Plant
    {
        public PlantType type;

        [Space]
        public Sprite sprite;

        [Space]
        public PlantInstance plantPrefab;

        [Space]
        public float growthTime;
        public float cost;
    }
}