using DG.Tweening;
using UnityEngine;

public static class TransformExtensions
{
    public static float GetSqrDistance(this Transform origin, Transform target)
    {
        return (target.position - origin.position).sqrMagnitude;
    }

    public static float GetSqrDistance(this Transform origin, Vector3 target)
    {
        return (target - origin.position).sqrMagnitude;
    }
    
    public static bool IsSameTransform(this Transform origin, Transform target)
    {
        return origin == target;
    }

    public static void Bounce(this Transform origin, Vector3 originScale, float scale, float duration)
    {
        ResetScale(origin, originScale);
        origin.DOScale(origin.localScale + Vector3.one * scale, duration).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo);
    }

    public static void BounceY(this Transform origin, Vector3 originScale, float scale, float duration)
    {
        ResetScale(origin, originScale);
        origin.DOScaleY(origin.localScale.y + scale, duration).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo);
    }


    private static void ResetScale(Transform origin, Vector3 originScale)
    {
        origin.DOKill();
        origin.localScale = originScale;
    }
}
