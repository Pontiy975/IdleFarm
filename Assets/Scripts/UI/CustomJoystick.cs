using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomJoystick : MonoBehaviour
{
    public static CustomJoystick Instance;

    private enum TouchPointMode
    {
        Static,
        Dynamic
    }

    [SerializeField] private Image backImage;
    [SerializeField] private Image middleImage;

    [SerializeField] private RectTransform middle;

    [SerializeField] private TouchPointMode touchPointModeMode = TouchPointMode.Static;

    private RectTransform _rect;

    private Vector3 _touchPosition;
    private bool _isTouched = false;

    private Vector3 _directionInPixels;
    private Vector3 _direction;

    private float _directionMagnitude;

    private RectTransform _canvasRect;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _canvasRect = transform.parent.GetComponent<RectTransform>();
    }

    private void ShowJoy()
    {
        backImage.gameObject.SetActive(true);
        middleImage.gameObject.SetActive(true);

        Color white = new Color(1f, 1f, 1f, 1f);
        backImage.DOColor(white, 0.2f);
        middleImage.DOColor(white, 0.2f);
    }

    private void HideJoy()
    {
        backImage.DOColor(new Color(1f, 1f, 1f, 0f), 0.2f);
        middleImage.DOColor(new Color(1f, 1f, 1f, 0f), 0.2f);
    }

    private bool IsCanBeTouched()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject();
#else
        return Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
#endif
    }

    void Update()
    {
        if (IsCanBeTouched())
        {
            if (!_isTouched)
            {
                ShowJoy();

                _isTouched = true;
                _touchPosition = Input.mousePosition;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, _touchPosition, null, out Vector2 screenPos);
                _rect.anchoredPosition = screenPos;
            }

            _directionInPixels = Input.mousePosition - _touchPosition;
            _direction = _directionInPixels * (1536f / Screen.width);

            _directionMagnitude = _direction.magnitude;

            if (_directionMagnitude > 180f)
            {
                if (touchPointModeMode == TouchPointMode.Dynamic && !Application.isEditor)
                {
                    _touchPosition += _direction - _direction.normalized * 180f;

                    RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, _touchPosition, null,
                        out Vector2 screenPos);
                    _rect.anchoredPosition = screenPos;
                }

                _direction *= 180f / _directionMagnitude;
            }

            _direction /= 180f;
            _directionMagnitude = _direction.magnitude;

            middle.anchoredPosition = _direction * 180f;

        }
        else
        {
            if (_isTouched)
            {
                HideJoy();
                _isTouched = false;
            }
        }
    }

    public bool IsTouched => _isTouched;

    public Vector3 DirectionXZ => new Vector3(_direction.x, 0f, _direction.y);
    public float DirectionMagnitude => _directionMagnitude;
}
