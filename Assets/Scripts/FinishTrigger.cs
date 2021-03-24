using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class FinishTrigger : MonoBehaviour
{
    public UnityEvent OnFinished;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrowdUnit crowdUnit))
        {
            crowdUnit.Crowd.SplineFollower.enabled = false;
            crowdUnit.Crowd.Control.enabled = false;

            crowdUnit.Crowd.RemoveUnit(crowdUnit, true);
            crowdUnit.SetDancing();
            crowdUnit.Rotate(180);

            MenuState.SetState("SetWin");

            _trigger.enabled = false;

            OnFinished?.Invoke();
        }
    }
}
