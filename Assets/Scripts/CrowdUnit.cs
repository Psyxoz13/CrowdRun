using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class CrowdUnit : MonoBehaviour
{
    [HideInInspector] public Crowd Crowd;

    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public Vector3 Velocity;

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
            _animator.SetBool("IsGrounded", true);
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
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

    public void Follow(Transform target, float offsetZ, float speed)
    {
        var lookDirection = (target.localPosition - transform.localPosition).normalized;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            Quaternion.LookRotation(lookDirection),
            speed * Time.deltaTime);

        var moveVector = new Vector3(
            target.localPosition.x,
             transform.localPosition.y,
             offsetZ + target.localPosition.z);

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
        transform.forward *= -1;
        _animator.SetTrigger("SetCrash");
    }

    private void RemoveUnit(Transform newParent)
    {
        Crowd.RemoveUnit(this);
        transform.parent = newParent;
    }
}
