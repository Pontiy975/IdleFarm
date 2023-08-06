using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchasingArea : MonoBehaviour
{
    private const float
        BodyBounceScale = 0.15f,
        BodyBounceDuration = 0.1f;

    [SerializeField]
    private Transform
        purchasedObject,
        body;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private int cost;

    private int _paidAmount;

    public bool IsPurchased => _paidAmount >= cost;
    public int Remainder => cost - _paidAmount;


    private void Start()
    {
        UpdateText();
    }

    public void Pay(int sum)
    {
        _paidAmount += sum;

        if (IsPurchased)
        {
            purchasedObject.gameObject.SetActive(true);
            purchasedObject.localScale = Vector3.zero;

            purchasedObject.DOScale(1f, 0.3f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.3f)
                .OnComplete(() => gameObject.SetActive(false));

            transform.DOScale(0f, 0.3f).SetEase(Ease.InBack);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = (cost - _paidAmount).ToString();
    }

    public void Bounce()
    {
        body.Bounce(Vector3.one, BodyBounceScale, BodyBounceDuration);
    }
}
