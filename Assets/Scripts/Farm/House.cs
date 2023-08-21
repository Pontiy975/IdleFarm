using UnityEngine;

public class House : MonoBehaviour
{
    private const float
        BaseScale = 0.7f,
        BounceScale = 0.05f,
        BounceDuration = 0.1f;


    [SerializeField]
    private Transform body;

    public Transform Body => body;

    public void Bounce()
    {
        body.BounceY(Vector3.one * BaseScale, BounceScale, BounceDuration);
    }
}
