using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class BonusLine : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1f;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrowdUnit"))
        {
            SetScoreMultiplier();
        }
    }

    private void SetScoreMultiplier()
    {
        LevelScore.Multiplier = _multiplier;

        _material.SetInt("_IsActivated", 1);
    }
}
