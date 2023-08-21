using DG.Tweening;
using System;
using UnityEngine;

public class Cluster : MonoBehaviour
{
    public event Action OnCutted;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private GameObject[] stages;

    private int _stage = 0;

    public void Cutting()
    {
        body.transform.DOPunchScale(UnityEngine.Random.insideUnitSphere * 0.1f, 0.3f).OnComplete(() =>
        {
            stages[_stage].SetActive(false);
            _stage++;

            if (_stage >= stages.Length)
            {
                OnCutted?.Invoke();
                gameObject.SetActive(false);
            }
        });
    }
}
