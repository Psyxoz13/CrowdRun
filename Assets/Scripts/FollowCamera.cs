using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera Instance;

    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private Vector3 _unitOffset;

    [Header("Focus")]
    [SerializeField] private Vector3 _focusOffset;

    private Transform _target;
    private Vector3 _offset;
    private Quaternion _rotation;

    private void Awake()
    {
        Instance = this;

        _rotation = transform.localRotation;
    }

    public void Update()
    {
        Follow();
    }

    public void SetFollowTarget(Transform target)
    {
        _target = target;
    }

    public void SetFocus(Transform target)
    {
        _target = target;
        _rotation = Quaternion.identity;
        _startOffset = _focusOffset;
    }

    public void SetOffset(int unitsCount)
    {
        _offset = _unitOffset * unitsCount;
    }

    public void Stop()
    {
        _followSpeed = 0f;
    }

    private void Follow()
    {
        var moveVector = _startOffset
            + _offset
            + new Vector3(
                0f,
                0f,
                _target.localPosition.z);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
           moveVector,
            _followSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            _rotation,
            _followSpeed * Time.deltaTime);
    }
}
