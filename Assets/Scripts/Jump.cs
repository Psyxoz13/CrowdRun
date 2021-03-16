using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private LayerMask _enterLayer;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private bool _once = false;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enterLayer.GetNumber())
        {
            other.attachedRigidbody.velocity = Vector3.up * _jumpPower;

            if (_once)
            {
                _trigger.enabled = false;
            }
        }
    }
}
