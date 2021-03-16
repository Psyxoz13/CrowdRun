using UnityEngine;

[RequireComponent(typeof(Collider))]
public class JellyCube : MonoBehaviour
{
    [SerializeField] private GameObject _jumpZone;
    [SerializeField] private ParticleSystem _hitEffect;

    private Collider _collider;
    private Animator _animator;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrowdUnit"))
        {
            _collider.enabled = false;
            _animator.SetTrigger("SetBoom");
            _jumpZone.SetActive(true);

            //_hitEffect.Play(true);
        }
    }
}
