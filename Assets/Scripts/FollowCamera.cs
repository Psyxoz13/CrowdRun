using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private Vector3 _unitOffset;

    private static Transform _target;
    private static int _unitsCount;

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

    private void Follow()
    {
        var offset = _unitOffset * _unitsCount;
        var moveVector = offset + new Vector3(
            0f,
            0f,
            _target.localPosition.z);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
           moveVector,
            _followSpeed * Time.deltaTime);
    }
}
