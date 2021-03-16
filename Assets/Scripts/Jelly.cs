using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jelly : MonoBehaviour
{
    [SerializeField] private JellyBone[] _jellyBones;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        VibrateBones(collision.contacts[0].point);
    }

    private void VibrateBones(Vector3 hitPoint)
    {
        for (int i = 0; i < _jellyBones.Length; i++)
        {
            _jellyBones[i].Vibrate(_rigidbody.velocity);
        }
    }
}
