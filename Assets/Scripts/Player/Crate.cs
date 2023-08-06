using UnityEngine;
using DG.Tweening;

using PlantType = Plants.PlantType;


public class Crate : MonoBehaviour
{
    private const int Capacity = 3;

    private const float
        CrateScale = 1.2f,
        BounceScale = 0.1f,
        BounceDuration = 0.3f,
        MaxSeedHeight = 0.2f,
        SeedStep = MaxSeedHeight / Capacity;


    [SerializeField]
    private Transform seedsMesh;

    [SerializeField]
    private SpriteRenderer spriteRenderer;


    private Transform _transform;
    private Settings _settings;
    private PlantType _type = PlantType.None;

    private int _seedsCount;

    public int SeedsCount => _seedsCount;
    public bool IsFull => _seedsCount >= Capacity;
    public PlantType Type => _type;


    private void Start()
    {
        _transform = transform;
        _settings = Settings.Instance;
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
                _transform.Bounce(Vector3.one * CrateScale, BounceScale, BounceDuration);

            ChangeSeedsPosition(SeedStep);

            _seedsCount++;
            _type = type;

            spriteRenderer.sprite = _settings.GetPlantByType(type).sprite;
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
}
