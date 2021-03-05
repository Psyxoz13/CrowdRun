using UnityEngine;
using UnityEngine.Events;

public class Control : MonoBehaviour
{
    public UnityEvent OnStartDrag;
    public UnityEvent OnEndDrag;

    public static Vector3 Delta;

    [SerializeField] private float _sensivity = 10f;

    private Camera _mainCamera;
    private Vector3 _startPosition;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnStartDrag?.Invoke();
            SetStartPosition();
        }
        else if (Input.GetMouseButton(0))
        {
            SetDelta();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag?.Invoke();
            Delta = Vector3.zero;
        }
    }

    private void SetStartPosition()
    {
        _startPosition = Input.mousePosition;
    }

    private void SetDelta()
    {
        Delta = _mainCamera.ScreenToViewportPoint(Input.mousePosition - _startPosition) * _sensivity;
    }
}

