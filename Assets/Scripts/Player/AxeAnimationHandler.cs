using System;
using UnityEngine;

public class AxeAnimationHandler : MonoBehaviour
{
    public event Action OnCutting;


    public void WoodCutting()
    {
        OnCutting?.Invoke();
    }
}
