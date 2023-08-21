using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;


[SelectionBase]
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    private const int PayStep = 5;

    [Header("Movement")]
    [SerializeField]
    private CustomJoystick joystick;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private float speed, rotationSpeed;

    [Space]

    [Header("Animations")]
    [SerializeField]
    private Animator animator;

    [Space]

    [Header("Crate")]
    [SerializeField]
    private Crate crate;

    [Space]

    [Header("Stack")]
    [SerializeField]
    private Stack stack;

    [SerializeField]
    private RigBuilder rig;

    [SerializeField]
    private float loadingDelay;

    [Space]

    [Header("Tools")]
    [SerializeField]
    private GameObject axe;

    [SerializeField]
    private AxeAnimationHandler axeHandler;


    private bool _crateIsActive;

    private Transform _transform;
    private Rigidbody _rigidbody;
    
    private Coroutine
        _seedsLoadingRoutine,
        _purchasingRoutine;

    private GameManager _gameManager;
    private PurchasingArea _currentPurchasingArea;
    private Cluster _cluster;


    private readonly Quaternion _cameraRotationFix = Quaternion.Euler(0f, 210f, 0f);


    public bool IsMove => joystick.IsTouched && joystick.DirectionXZ != Vector3.zero;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void OnDestroy()
    {
        axeHandler.OnCutting -= WoodCutting;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeedsArea"))
        {
            SeedsArea dock = other.GetComponent<SeedsArea>();

            _seedsLoadingRoutine = StartCoroutine(SeedsLoadingRoutine(dock.Type));
        }

        if (other.CompareTag("Bed"))
        {
            GardenBed bed = other.GetComponent<GardenBed>();
            BedHandling(bed);
        }

        if (other.CompareTag("House"))
        {
            stack.Unstack(_gameManager.House);
        }

        if (other.CompareTag("Cluster"))
        {
            _cluster = other.GetComponentInParent<Cluster>();
            _cluster.OnCutted += StopCutting;

            axe.SetActive(true);
            animator.SetBool("IsCutting", true);
        }

        if (other.CompareTag("PurchasingArea"))
        {
            _currentPurchasingArea = other.GetComponent<PurchasingArea>();
            _purchasingRoutine = StartCoroutine(PurchasingRoutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SeedsArea") && _seedsLoadingRoutine != null)
        {
            StopCoroutine(_seedsLoadingRoutine);
            _seedsLoadingRoutine = null;
        }

        if (other.CompareTag("Cluster"))
        {
            StopCutting();
        }

        if (other.CompareTag("PurchasingArea") && _purchasingRoutine != null)
        {
            StopCoroutine(_purchasingRoutine);
            _purchasingRoutine = null;
        }
    }

    private void Init()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _gameManager = GameManager.Instance;

        axeHandler.OnCutting += WoodCutting;
    }

    private void UpdateMovement()
    {
        if (IsMove)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_cameraRotationFix * new Vector3(
                joystick.DirectionXZ.x,
                0f,
                joystick.DirectionXZ.z).normalized);

            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

            _rigidbody.AddForce(
                _cameraRotationFix * (speed * Time.fixedDeltaTime * joystick.DirectionXZ),
                ForceMode.Impulse);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("IsMove", IsMove);
    }

    private void ShowCrate()
    {
        _crateIsActive = true;

        crate.gameObject.SetActive(true);
        crate.Show();

        rig.enabled = true;
    }

    private void HideCrate()
    {
        _crateIsActive = false;

        crate.Hide();

        rig.enabled = false;
    }

    private IEnumerator SeedsLoadingRoutine(Plants.PlantType type)
    {
        if (!_crateIsActive)
        {
            ShowCrate();
            crate.AddSeeds(type, false);
        }

        while (!crate.IsFull)
        {
            yield return new WaitForSeconds(loadingDelay);
            crate.AddSeeds(type);
        }
    }

    private IEnumerator PurchasingRoutine()
    {
        while (_gameManager.Money > 0 && !_currentPurchasingArea.IsPurchased)
        {
            int remainder = _currentPurchasingArea.Remainder;
            int amount;
            
            if (_gameManager.Money >= remainder)
            {
                amount = remainder > PayStep ? PayStep : remainder;
            }
            else
            {
                amount = _gameManager.Money > PayStep ? PayStep : _gameManager.Money;
            }

            _gameManager.ChangeMoney(-amount);
            _currentPurchasingArea.Pay(amount);

            Coin coin = _gameManager.PoolController.GetFromPool<Coin>();
            coin.transform.position = _transform.position + Vector3.up;
            coin.JumpTo(_currentPurchasingArea.transform, _gameManager.PoolController);

            _currentPurchasingArea.Bounce();

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void BedHandling(GardenBed bed)
    {
        if (bed.ReadyToHarvest && !stack.IsFull)
        {
            stack.AddPlant(bed.Harvest());
        }

        if (_crateIsActive)
        {
            if (bed.IsEmpty && bed.Type == crate.Type && crate.SeedsCount > 0)
            {
                crate.GetSeed();
                bed.Planting();

                if (crate.SeedsCount <= 0)
                {
                    HideCrate();
                }
            }
        }
    }

    private void WoodCutting()
    {
        if (_cluster)
        {
            _cluster.Cutting();
        }
    }

    private void StopCutting()
    {
        _cluster.OnCutted -= StopCutting;
        _cluster = null;
        axe.SetActive(false);
        animator.SetBool("IsCutting", false);
    }
}