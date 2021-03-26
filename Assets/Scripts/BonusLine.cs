using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class BonusLine : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1f;
    [SerializeField] private bool _isStoping = true;

    [Header("Text")]
    [SerializeField] private TextMeshPro _multiplierText;
    [SerializeField] private Color _color;
    [SerializeField] private Color _activatedColor;

    private Material _material;
    private Collider _trigger;

    private void OnValidate()
    {
        _multiplierText.text = "x" + _multiplier.ToString("0.0").Replace(',', '.');
        _multiplierText.color = _color;
    }

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
            _multiplierText.color = _activatedColor;

            SetScoreMultiplier();

            if (crowdUnit.Crowd.Units.Count == 1 &&
                _isStoping)
            {
                crowdUnit.Crowd.RemoveUnit(crowdUnit, true);
                crowdUnit.Crowd.SplineFollower.enabled = false;
                crowdUnit.SetDancing();
                crowdUnit.Rotate(180f);
                MenuState.SetState("SetWin");
                FollowCamera.Instance.SetFocus(crowdUnit.transform);
            }
        }
    }

    private void SetScoreMultiplier()
    {
        LevelScore.Multiplier = _multiplier;
    }
}
