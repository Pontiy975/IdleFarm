using DG.Tweening;
using UnityEngine;

public class StackCrate : MonoBehaviour
{
    private const float
        JumpPower = 1f,
        JumpDuration = 1f;


    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void Show(Sprite sprite)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

        spriteRenderer.sprite = sprite;
    }

    public void JumpTo(Transform point)
    {
        Vector3 position = _transform.localPosition;
        Transform parent = _transform.parent;

        _transform.SetParent(null);

        _transform.DOJump(point.position, JumpPower, 1, JumpDuration).OnComplete(() =>
        {
            _transform.SetParent(parent);

            _transform.localPosition = position;
            _transform.localRotation = Quaternion.identity;

            gameObject.SetActive(false);
        });
    }
}
