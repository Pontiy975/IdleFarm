using System;
using UnityEngine;

namespace Plants
{
    public enum PlantType
    {
        Carrot,
        Corn,
        Tomato,
        None
    }

    [Serializable]
    public class PlantConfiguration
    {
        public PlantType type;

        [Space]
        public Sprite sprite;

        [Space]
        public float growthTime;
        public int cost;

        public Type t;
    }
}