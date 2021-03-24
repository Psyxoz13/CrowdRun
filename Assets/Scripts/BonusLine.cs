using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class BonusLine : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1f;
    [SerializeField] private bool _isStoping = true;

    private Material _material;
    private Collider _trigger;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrowdUnit crowdUnit))
        {
            _material.SetInt("_IsActivated", 1);
            _trigger.enabled = false;

            SetScoreMultiplier();

            if (crowdUnit.Crowd.Units.Count == 1 &&
                _isStoping)
            {
                crowdUnit.Crowd.RemoveUnit(crowdUnit, true);
                crowdUnit.Crowd.SplineFollower.enabled = false;
                crowdUnit.SetDancing();
                crowdUnit.Rotate(180f);
                MenuState.SetState("SetWin");
            }
        }
    }

    private void SetScoreMultiplier()
    {
        LevelScore.Multiplier = _multiplier;
    }
}
