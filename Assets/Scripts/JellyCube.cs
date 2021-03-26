using UnityEngine;

[RequireComponent(typeof(Collider))]
public class JellyCube : MonoBehaviour
{
    [SerializeField] private Collider _cubeColider;

    private JellyCubesRow _parentRow;
    private Collider _collider;
    private Animator _animator;
    private bool _exploding = true;

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
        if (collision.gameObject.CompareTag("CrowdUnit") &&
            _exploding)
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
        _cubeColider.enabled = false;
        _animator.SetTrigger("SetBoom");
    }

    public void SetNotExplosion()
    {
        _exploding = false;
        gameObject.tag = "Untagged";
    }
}
