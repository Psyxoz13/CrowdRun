using UnityEngine;

[RequireComponent(typeof(Animation), typeof(Collider))]
public class Wall : MonoBehaviour
{
    private Animation _animation;
    private Collider _collider;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrowdUnit"))
        {
            _animation.Play();
            _collider.enabled = false;
        }
    }
}
