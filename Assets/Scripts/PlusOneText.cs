using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlusOneText : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Camera _camera;
    private Animation _animation;

    private void Awake()
    {
        _camera = Camera.main;
        _animation = GetComponent<Animation>();
    }

    public void Show(Vector3 position)
    {
        transform.position = _camera.WorldToScreenPoint(position) + _offset;
        _animation.Play();
    }
}
