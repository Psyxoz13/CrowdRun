using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlusOneText : MonoBehaviour
{
    [SerializeField] private Transform _hookTarget;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;
    private Animation _animation;

    private void Awake()
    {
        _camera = Camera.main;
        _animation = GetComponent<Animation>();
    }

    public void Show()
    {
        transform.position = _camera.WorldToScreenPoint(_hookTarget.position) + _offset;
        _animation.Play();
    }
}
