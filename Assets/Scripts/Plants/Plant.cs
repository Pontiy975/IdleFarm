using DG.Tweening;
using UnityEngine;

namespace Plants
{
    public abstract class Plant : PoolableObject
    {
        [SerializeField]
        protected Transform sprout, plant;

        protected float growthTime, cost;
        protected bool isGrownUp, isPlanted;

        protected Transform _transform;

        private float _cooldown;


        private void Awake()
        {
            _transform = transform;
        }


        public void Planting(Transform bed)
        {
            isPlanted = true;

            _transform.position = bed.position;
            _transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);

            _transform.localScale = new Vector3(1f, 0f, 1f);
            _transform.DOScaleY(0.8f, 0.3f).SetEase(Ease.OutBounce);
        }

        public void Init(PlantConfiguration plant)
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