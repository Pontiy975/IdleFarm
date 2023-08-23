using DG.Tweening;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Plants
{
    public abstract class Plant : PoolableObject
    {
        public event Action OnGrown;

        [SerializeField]
        protected Transform sprout, plant;

        protected float growthTime, cost;
        protected bool isGrownUp, isPlanted;

        protected PoolController controller;
        protected Transform _transform;

        private float _cooldown;

        public PlantConfiguration Config { get; private set; }


        private void Awake()
        {
            controller = GameManager.Instance.PoolController;
            _transform = transform;
        }


        public void Planting(Transform bed)
        {
            isPlanted = true;

            _transform.position = bed.position;
            _transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);

            _transform.localScale = new Vector3(1f, 0f, 1f);
            _transform.DOScaleY(1f, 0.3f).SetEase(Ease.OutBounce);
        }

        public void Init(PlantConfiguration plant)
        {
            Config = plant;

            growthTime = plant.growthTime;
            cost = plant.cost;

            _cooldown = growthTime;
        }

        public abstract void ReturnToPool();

        public override void OnDespawn()
        {
            base.OnDespawn();

            isPlanted = false;
            isGrownUp = false;
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

        protected virtual void Growth()
        {
            OnGrown?.Invoke();
        }
    }
}