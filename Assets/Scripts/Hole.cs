using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hole : MonoBehaviour
{
    [SerializeField] private LayerMask _enterLayer;
    [SerializeField] private LayerMask _exitLayer;

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
        }
    }

}
