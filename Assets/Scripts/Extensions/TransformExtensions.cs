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
}
