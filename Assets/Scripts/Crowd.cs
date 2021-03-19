using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crowd : MonoBehaviour
{
    public bool IsMove { get; set; }

    public UnityEvent OnCrowdEmpty;
    public UnityEvent OnUnitAdded;

    public List<CrowdUnit> Units = new List<CrowdUnit>();

    [SerializeField] private Transform _crowdUnits;
    [SerializeField] private float _unitsRotateSpeed = 3f;
    [SerializeField] private float _unitsForwardSpeed = 3f;
    [SerializeField] private float _unitsHorizontalSpeed = 3f;
    [SerializeField] private float _unitsOffset = -0.1f;

    [Header("Rush")]
    [SerializeField] private LayerMask _rushMask;
    [SerializeField] private float _distanceToRush = 5f;
    [SerializeField] private float _rushSpeed = 5f;

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

    private void FixedUpdate()
    {
        TrySetRush(Units[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrowdUnit") &&
            other.TryGetComponent(out CrowdUnit crowdUnit) &&
            Units.Contains(crowdUnit) == false &&
            crowdUnit.IsDead == false)
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
                FollowFirstUnit();
            }
            else
            {
                Transform target = Units[i - 1].transform;
                Units[i].Follow(target, _unitsOffset, _unitsHorizontalSpeed, _unitsForwardSpeed, _unitsRotateSpeed);
            }
        }
    }

    private void FollowFirstUnit()
    {
        CrowdUnit crowdUnit = Units[0];
        if (crowdUnit.IsRush)
        {
            crowdUnit.Follow(
                transform,
                _unitsOffset + _distanceToRush,
                _unitsHorizontalSpeed,
                _rushSpeed,
                _unitsRotateSpeed);
        }
        else
        {
            crowdUnit.Follow(
                transform,
                _unitsOffset,
                _unitsHorizontalSpeed,
                _unitsForwardSpeed,
                _unitsRotateSpeed);
        }
    }

    private void AddUnit(CrowdUnit crowdUnit)
    {
        Units.Insert(0, crowdUnit);

        crowdUnit.Crowd = this;
        crowdUnit.transform.parent = _crowdUnits;

        FollowCamera.SetUnitsCount(Units.Count);

        OnUnitAdded?.Invoke();
    }

    private void TrySetRush(CrowdUnit unit)
    {
        if (_isCrowdEmpty == false &&
            Physics.Raycast(Units[0].transform.position + Vector3.up * 0.5f, Vector3.forward, _distanceToRush, _rushMask))
        {
            unit.IsRush = true;
        }
        else
        {
            unit.IsRush = false;
        }
    }
}
