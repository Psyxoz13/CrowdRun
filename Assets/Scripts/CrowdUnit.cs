using System;
using System.Collections;
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
            RemoveUnit(collision.transform.parent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            RemoveUnit(other.transform.parent);
        }
    }

    public void Follow(Transform target, float offsetZ, float speed, float forwardSpeed, float rotateSpeed)
    {
        Follow(target.localPosition, offsetZ, speed, forwardSpeed, rotateSpeed);
    }

    public void Follow(Vector3 targetPosition, float offsetZ, float speed, float forwardSpeed, float rotateSpeed)
    {
        var lookDirection = (targetPosition - transform.localPosition).normalized;

        if (transform.localPosition.z < targetPosition.z)
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.LookRotation(lookDirection),
                rotateSpeed * Time.deltaTime);
        }

        var moveVector = new Vector3(
            targetPosition.x,
             transform.localPosition.y,
             Mathf.MoveTowards(
                  transform.localPosition.z,
                 offsetZ + targetPosition.z,
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

    public void SetDancing()
    {
        _animator.SetTrigger("SetDancing");
    }

    public void Rotate(float angle)
    {
        StartCoroutine(GetRotate(angle));
    }

    private IEnumerator GetRotate(float angle)
    {
        while (true)
        {
            transform.eulerAngles = Vector3.Lerp(
                transform.eulerAngles,
                Vector3.up * angle,
                3f * Time.deltaTime);
            yield return null;
        }
    }

    private void SetCrashed()
    {
        _animator.SetTrigger("SetCrash");
    }

    private void RemoveUnit(Transform newParent)
    {
        Crowd.RemoveUnit(this);
        transform.parent = newParent;
        IsDead = true;
    }
}
