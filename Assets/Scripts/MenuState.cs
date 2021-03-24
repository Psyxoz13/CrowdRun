using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MenuState : MonoBehaviour
{
    private static Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public static void SetState(string state)
    {
        _animator.SetTrigger(state);
    }
}
