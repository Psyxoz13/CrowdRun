using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crowd : MonoBehaviour
{
    public bool IsMove { get; set; }

    public UnityEvent OnCrowdEmpty;

    public List<CrowdUnit> Units = new List<CrowdUnit>();

    [Space]
    [SerializeField] private SplineFollower _follower;
    [SerializeField] private Transform _crowdUnits;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _crowdFollowerSlowSpeed = 2f;
    [SerializeField] private float _crowdSlowSpeed = 1f;
    [SerializeField] private float _unitsOffset = -0.1f;

    private Vector3 _startMovePosition;
    private bool _isCrowdEmpty 
    {
        get => Units.Count <= 0;
    }

    private void Awake()
    {
        SetStartUnits();
    }

    private void Start()
    {
        FollowCamera.SetFollowTarget(transform);
        FollowCamera.SetUnitsCount(Units.Count);
    }

    private void Update()
    {
        Move();
        FollowUnits();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrowdUnit") &&
            other.TryGetComponent(out CrowdUnit crowdUnit) &&
            Units.Contains(crowdUnit) == false)
        {
            AddUnit(crowdUnit);
        }
    }

    public void RemoveUnit(CrowdUnit unit)
    {
        Units.Remove(unit);
        if (_isCrowdEmpty)
        {
            OnCrowdEmpty?.Invoke();
            return;
        }
        FollowCamera.SetUnitsCount(Units.Count);
    }

    public void Slow(float time)
    {
        StartCoroutine(GetSlowTime(time));
    }

    private void SetStartUnits()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            Units[i].Crowd = this;
        }
    }

    private void Move()
    {
        if (IsMove)
        {
            transform.localPosition = new Vector3(
                Control.Delta.x + _startMovePosition.x,
                transform.localPosition.y,
                transform.localPosition.z);
        }
        else
        {
            _startMovePosition = transform.localPosition;
        }
    }

    private void FollowUnits()
    {
        if (_isCrowdEmpty)
            return;

        for (int i = 0; i < Units.Count; i++)
        {
            if (i == 0)
            {
                Units[i].Follow(transform, _unitsOffset, _speed);
            }
            else
            {
                var target = Units[i - 1].transform;
                Units[i].Follow(target, _unitsOffset, _speed);
            }
        }
    }

    private void AddUnit(CrowdUnit crowdUnit)
    {
        Units.Insert(0, crowdUnit);
        crowdUnit.Crowd = this;
        crowdUnit.transform.parent = _crowdUnits;
        FollowCamera.SetUnitsCount(Units.Count);
    }

    private IEnumerator GetSlowTime(float time)
    {
        float tempFollowSpeed = _follower.followSpeed;
        float tempSpeed = _speed;
        _follower.followSpeed = _crowdFollowerSlowSpeed;
        _speed = _crowdSlowSpeed;
        yield return new WaitForSecondsRealtime(time);
        _follower.followSpeed = tempFollowSpeed;
        _speed = tempSpeed;
    }
}
