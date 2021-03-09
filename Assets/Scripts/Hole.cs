using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hole : MonoBehaviour
{
    [SerializeField] private LayerMask _enterLayer;
    [SerializeField] private LayerMask _exitLayer;
    [SerializeField] private float _downForce = 2f;
    [SerializeField] private float _crowdSlowTime = 2f;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enterLayer.GetNumber() &&
            other.TryGetComponent(out CrowdUnit unit))
        {
            other.gameObject.layer = _exitLayer.GetNumber();
            _trigger.enabled = false;
            unit.TryGetComponent(out Rigidbody rigidbody);
            rigidbody.velocity += Vector3.down * _downForce;
            unit.Crowd.Slow(_crowdSlowTime);
        }
    }

}
