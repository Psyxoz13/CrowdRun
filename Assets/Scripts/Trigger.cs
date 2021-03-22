using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    [SerializeField] private bool _once;
    [SerializeField] private string[] _tags;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTags(other))
        {
            OnEnter?.Invoke();

            if (_once)
            {
                _trigger.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTags(other))
        {
            OnExit?.Invoke();
        }
    }

    private bool CheckTags(Collider other)
    {
        for (int i = 0; i < _tags.Length; i++)
        {
            if  (other.CompareTag(_tags[i]))
            {
                return true;
            }
        }
        return false;
    }
}
