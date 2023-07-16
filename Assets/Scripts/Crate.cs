using System;
using UnityEngine;
using DG.Tweening;

public class Crate : MonoBehaviour
{
    private const int Capacity = 3;

    private const float CrateScale = 1.2f;
    private const float
        MaxSeedHeight = 0.2f,
        SeedStep = MaxSeedHeight / Capacity;


    [SerializeField]
    private Transform seedsMesh;


    private Transform _transform;

    private int _seedsCount;


    public bool IsFull => _seedsCount >= Capacity;


    private void Start()
    {
        _transform = transform;
        gameObject.SetActive(false);
    }


    public void Show()
    {
        _transform.DOKill();
        _transform.localScale = Vector3.zero;

        _transform.DOScale(CrateScale, 0.7f)
                  .SetEase(Ease.OutBack);
    }

    public void AddSeeds(bool withBounce = true)
    {
        if (seedsMesh.localPosition.y < MaxSeedHeight)
        {
            if (withBounce)
                Bounce();

            Vector3 position = seedsMesh.localPosition;
            position.y += SeedStep;

            seedsMesh.localPosition = position;
        }
    }


    private void Bounce()
    {
        _transform.DOKill();

        _transform.localScale = Vector3.one * CrateScale;
        _transform.DOScale(_transform.localScale + Vector3.one * 0.1f, 0.3f).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo);
    }
}
