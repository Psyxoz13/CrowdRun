using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float _addSpeed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrowdUnit crowdUnit))
        {
            crowdUnit.Crowd.SplineFollower.followSpeed += _addSpeed;
            gameObject.SetActive(false);
        }
    }
}
