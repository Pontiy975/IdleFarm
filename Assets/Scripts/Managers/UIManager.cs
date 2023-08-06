using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyCounter;

    public void ChangeMoney(int value)
    {
        moneyCounter.text = value.ToString();
        moneyCounter.rectTransform.Bounce(Vector3.one, 0.1f, 0.1f);
    }
}
