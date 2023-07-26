using DG.Tweening;
using UnityEngine;

public class StackCrate : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void Show(Sprite sprite)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

        spriteRenderer.sprite = sprite;
    }
}
