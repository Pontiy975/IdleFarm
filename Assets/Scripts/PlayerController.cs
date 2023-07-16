using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;


[SelectionBase]
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private CustomJoystick joystick;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private float speed, rotationSpeed;


    [Header("Animations")]
    [SerializeField]
    private Animator animator;


    [Header("Crate")]
    [SerializeField]
    private Crate crate;

    [SerializeField]
    private RigBuilder rig;

    [SerializeField]
    private float loadingDelay;


    private Transform _transform;
    private Rigidbody _rigidbody;

    private bool _isLoading;

    private Coroutine _seedsLoadingRoutine;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dock") && !_isLoading)
        {
            _isLoading = true;
            _seedsLoadingRoutine = StartCoroutine(SeedsLoadingRoutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dock") && _isLoading)
        {
            _isLoading = false;
            StopCoroutine(_seedsLoadingRoutine);
        }
    }

    private void Init()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
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
        crate.gameObject.SetActive(true);
        crate.Show();

        rig.enabled = true;
    }

    private IEnumerator SeedsLoadingRoutine()
    {
        if (!crate.gameObject.activeInHierarchy)
        {
            ShowCrate();
            crate.AddSeeds(false);
        }

        while (!crate.IsFull)
        {
            yield return new WaitForSeconds(loadingDelay);
            crate.AddSeeds();
        }

        _isLoading = false;
    }
}