using UnityEngine;


[SelectionBase]
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CustomJoystick joystick;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed, rotationSpeed;


    private Transform _transform;
    private Rigidbody _rigidbody;

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
}