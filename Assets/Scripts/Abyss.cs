using UnityEngine;

public class Abyss : MonoBehaviour
{
    [SerializeField] private LayerMask _enterLayer;
    [SerializeField] private LayerMask _exitLayer;
    [SerializeField] private float _downForce = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enterLayer.GetNumber())
        {
            other.gameObject.layer = _exitLayer.GetNumber();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CrowdUnit crowdUnit) &&
            crowdUnit.IsGrounded == false)
        {
            other.attachedRigidbody.velocity = Vector3.down * _downForce;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _exitLayer.GetNumber())
        {
            other.gameObject.layer = _enterLayer.GetNumber();
        }
    }

}
