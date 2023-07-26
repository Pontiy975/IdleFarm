using UnityEngine;
using DG.Tweening;

using PlantType = Plants.PlantType;


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
    private PlantType _type = PlantType.None;

    private int _seedsCount;

    public int SeedsCount => _seedsCount;
    public bool IsFull => _seedsCount >= Capacity;
    public PlantType Type => _type;


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

    public void Hide()
    {
        _type = PlantType.None;
        _seedsCount = 0;

        gameObject.SetActive(false);
    }

    public void AddSeeds(PlantType type, bool withBounce = true)
    {
        if (_type != PlantType.None && _type != type) return;
        // TODO: added red blink animation

        if (seedsMesh.localPosition.y < MaxSeedHeight)
        {
            if (withBounce)
                Bounce();

            ChangeSeedsPosition(SeedStep);

            _seedsCount++;
            _type = type;
        }
    }

    private void ChangeSeedsPosition(float step)
    {
        Vector3 position = seedsMesh.localPosition;
        position.y += step;

        seedsMesh.localPosition = position;
    }

    public void GetSeed()
    {
        if (_seedsCount > 0)
        {
            _seedsCount--;
            ChangeSeedsPosition(-SeedStep);
        }
    }

    private void Bounce()
    {
        _transform.DOKill();

        _transform.localScale = Vector3.one * CrateScale;
        _transform.DOScale(_transform.localScale + Vector3.one * 0.1f, 0.3f).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo);
    }
}