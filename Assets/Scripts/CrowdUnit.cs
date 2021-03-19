using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class CrowdUnit : MonoBehaviour
{
    public Vector3 Velocity { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsRush { get; set; }

    [HideInInspector] public Crowd Crowd;

    [HideInInspector] public Rigidbody Rigidbody;

    [SerializeField] private ParticleSystem _hitEffect;

    private Vector3 _prevPosition;
    private Animator _animator;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();
        _prevPosition = transform.position;
    }

    private void Start()
    {
        if (Crowd == null)
            _animator.SetTrigger("SetWaving");
    }

    private void Update()
    {
        SetVelocity();
        _animator.SetFloat("Speed", Velocity.magnitude);
    }

    private void SetVelocity()
    {
        var offset = transform.position - _prevPosition;
        _prevPosition = transform.position;
        Velocity = offset / Time.smoothDeltaTime;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.4f))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
        _animator.SetBool("IsGrounded", IsGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrowdUnit"))
        {
            _hitEffect.Play(true);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            _hitEffect.Play(true);

            SetCrashed();
            RemoveUnit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            RemoveUnit();
        }
    }

    public void Follow(Transform target, float offsetZ, float speed, float forwardSpeed, float rotateSpeed)
    {
        var lookDirection = (target.localPosition - transform.localPosition).normalized;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            Quaternion.LookRotation(lookDirection),
            rotateSpeed * Time.deltaTime);

        var moveVector = new Vector3(
            target.localPosition.x,
             transform.localPosition.y,
             Mathf.MoveTowards(
                  transform.localPosition.z,
                 offsetZ + target.localPosition.z,
                 forwardSpeed));

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            moveVector,
            speed * Time.deltaTime);
    }

    public void SetRunAnimation()
    {
        _animator.SetTrigger("SetRun");
    }

    private void SetCrashed()
    {
        _animator.SetTrigger("SetCrash");
    }

    private void RemoveUnit()
    {
        Crowd.RemoveUnit(this);
        transform.parent = null;
        IsDead = true;
    }
}
