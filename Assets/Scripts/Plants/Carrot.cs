using DG.Tweening;
using Plants;

public class Carrot : Plant
{
    private void Update()
    {
        if (!isPlanted) return;

        Timer();
    }


    protected override void Growth()
    {
        print("growthn");
        sprout.gameObject.SetActive(false);
        plant.gameObject.SetActive(true);

        plant.DOLocalMoveY(0f, 0.3f);

        _transform.DOScale(1.1f, 0.5f).SetDelay(0.3f).SetEase(Ease.InSine);
        _transform.DOScale(1f, 0.3f).SetDelay(0.7f).SetEase(Ease.OutSine);
    }
}
