using DG.Tweening;
using UnityEngine;

public class Coin : PoolableObject
{
    private const float
        JumpPower = 1f,
        JumpDuration = 0.3f;

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
