using DG.Tweening;
using Plants;
using UnityEngine;

public class Carrot : Plant
{
    private float basePlantY;

    private void Start()
    {
        basePlantY = plant.localPosition.y;
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


    protected override void Growth()
    {
        base.Growth();

        sprout.gameObject.SetActive(false);
        plant.gameObject.SetActive(true);

        plant.DOLocalMoveY(0f, 0.3f);

        _transform.DOScale(1.1f, 0.5f).SetDelay(0.3f).SetEase(Ease.InSine);
        _transform.DOScale(1f, 0.3f).SetDelay(0.7f).SetEase(Ease.OutSine);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        sprout.gameObject.SetActive(true);
        plant.gameObject.SetActive(false);

        Vector3 position = plant.localPosition;
        position.y = basePlantY;

        plant.localPosition = position;
    }
}
