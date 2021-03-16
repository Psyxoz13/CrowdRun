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
                Units[i].Follow(transform, _unitsOffset, _unitsHorizontalSpeed, _unitsForwardSpeed, _unitsRotateSpeed);
            }
            else
            {
                Transform target = Units[i - 1].transform;
                Units[i].Follow(target, _unitsOffset, _unitsHorizontalSpeed, _unitsForwardSpeed, _unitsRotateSpeed);
            }
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
}
