using UnityEngine;

namespace Plants
{
    public abstract class Plant : MonoBehaviour
    {
        [SerializeField]
        protected Transform sprout, plant;

        protected float growthTime, cost;
        protected bool isGrownUp;

        private float _cooldown;

        protected void Init(PlantConfiguration plant)
        {
            growthTime = plant.growthTime;
            cost = plant.cost;

            _cooldown = growthTime;
        }

        protected void Timer()
        {
            if (isGrownUp) return;

            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0f)
            {
                isGrownUp = true;
                Growth();
            }
        }

        protected abstract void Growth();
    }
}