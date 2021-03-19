using UnityEngine;

[RequireComponent(typeof(Collider))]
public class JellyCube : MonoBehaviour
{
    private JellyCubesRow _parentRow;

    private Collider _collider;
    private Animator _animator;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        if (transform.parent.TryGetComponent(out JellyCubesRow cubesRow))
        {
            _parentRow = cubesRow;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrowdUnit"))
        {
            SetExplosion();

            if (_parentRow)
            {
                _parentRow.Explore();
            }
        }
    }

    public void SetExplosion()
    {
        _collider.enabled = false;
        _animator.SetTrigger("SetBoom");
    }
}
