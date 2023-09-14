using DG.Tweening;
using System;
using UnityEngine;


public class Cluster : LockObject
{
    private static float
        PunchMultiplier = 0.1f,
        AnimationDuration = 0.3f;

    public event Action OnCutted;

    [SerializeField]
    private GameObject zone;

    [SerializeField]
    private Transform[] areaObjects;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private GameObject[] stages;


    private Transform _transform;

    private int _stage = 0;

    private void Start()
    {
        _transform = transform;
    }

    public void Cutting()
    {
        body.transform.DOPunchScale(Vector3.up * PunchMultiplier, AnimationDuration).OnComplete(() =>
        {
            stages[_stage].SetActive(false);
            _stage++;

            if (_stage >= stages.Length)
            {
                OnCutted?.Invoke();
                
                _transform.DOScaleY(0f, AnimationDuration)
                          .SetEase(Ease.InBack)
                          .OnComplete(() =>
                          {
                              ProgressManager.Instance.UnlockNext(this);
                              gameObject.SetActive(false);
                          });

                foreach (var obj in areaObjects)
                {
                    obj.gameObject.SetActive(true);

                    Vector3 scale = obj.localScale;
                    scale.y = 0f;

                    obj.localScale = scale;
                    
                    obj.DOScaleY(1f, AnimationDuration)
                       .SetEase(Ease.OutSine)
                       .OnComplete(() => gameObject.SetActive(false));
                }
            }
        });
    }


    public override void Unlock()
    {
        zone.SetActive(true);
    }
}
