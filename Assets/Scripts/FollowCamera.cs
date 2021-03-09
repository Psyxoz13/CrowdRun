using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private Vector3 _unitOffset;

    private static Transform _target;
    private Vector3 _startPosition;
    private static int _unitsCount;

    private void Awake()
    {
        _startPosition = transform.localPosition;
    }

    public void Update()
    {
        Follow();
    }

    public static void SetFollowTarget(Transform target)
    {
        _target = target;
    }

    public static void SetUnitsCount(int count)
    {
        _unitsCount = count;
    }

    public void Stop()
    {
        _followSpeed = 0f;
    }

    private void Follow()
    {
        var offset = _unitOffset * _unitsCount;
        var moveVector = _startPosition 
            + offset 
            + new Vector3(
                0f,
                0f,
                _target.localPosition.z);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
           moveVector,
            _followSpeed * Time.deltaTime);
    }
}
