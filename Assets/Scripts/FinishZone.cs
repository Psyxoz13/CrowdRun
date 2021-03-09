using UnityEngine;
using UnityEngine.Events;

public class FinishZone : MonoBehaviour
{
    public UnityEvent OnFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrowdUnit"))
        {
            OnFinished?.Invoke();
        }
    }
}
