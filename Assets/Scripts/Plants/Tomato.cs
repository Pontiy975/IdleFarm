using Plants;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;


public class Tomato : Plant
{
    private const float AnimationDuration = 0.3f;

    private List<Transform> _tomatoes = new List<Transform>();


    private void Start()
    {
        _tomatoes.AddRange(plant.GetComponentsInChildren<Transform>());
        _tomatoes.SafeRemove(plant.transform);
    }

    private void Update()
    {
        if (!isPlanted) return;

        Timer();
    }


    public override void ReturnToPool()
    {
        controller.ReturnToPool(this);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        plant.gameObject.SetActive(false);
    }


    protected override void Growth()
    {
        base.Growth();

        _transform.DOScale(1.5f, AnimationDuration).OnComplete(() =>
        {
            plant.gameObject.SetActive(true);

            foreach (var tomato in _tomatoes)
            {
                Vector3 originScale = tomato.localScale;

                tomato.localScale = Vector3.zero;
                tomato.DOScale(originScale, AnimationDuration).SetEase(Ease.OutBounce);
            }
        });
    }
}