using DG.Tweening;
using UnityEngine;

public class Coin : PoolableObject
{
    private const float
        JumpPower = 0.7f,
        JumpDuration = 0.5f;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void JumpTo(Transform point, PoolController controller)
    {
        _transform.DOJump(point.position, JumpPower, 1, JumpDuration).OnComplete(() =>
        {
            controller.ReturnToPool(this);
        });
    }
}
