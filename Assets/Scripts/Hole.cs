using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hole : MonoBehaviour
{
    [SerializeField] private LayerMask _enterLayer;
    [SerializeField] private LayerMask _exitLayer;
    [SerializeField] private float _downForce = 2f;
    [SerializeField, Range(0f, 1f)] private float _slow = .2f;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrowdUnit crowdUnit) && 
            other.gameObject.layer == _enterLayer.GetNumber())
        {
            _trigger.enabled = false;
            other.attachedRigidbody.velocity = crowdUnit.Velocity - crowdUnit.Velocity * _slow + Vector3.down * _downForce;
            other.gameObject.layer = _exitLayer.GetNumber();
        }
    }
}
