using UnityEngine;


public class SeedsDock : MonoBehaviour
{
    [field: SerializeField]
    public Plants.PlantType Type { get; private set; }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.sprite = Settings.Instance.GetPlantByType(Type).sprite;
    }
}