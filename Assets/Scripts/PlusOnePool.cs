using System.Collections.Generic;
using UnityEngine;

public class PlusOnePool : MonoBehaviour
{
    public static PlusOnePool Instance { get; private set; }

    [SerializeField] private List<PlusOneText> _plusOneEffects;

    private Queue<PlusOneText> _plusOneEffectsQueue = new Queue<PlusOneText>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < _plusOneEffects.Count; i++)
        {
            _plusOneEffectsQueue.Enqueue(_plusOneEffects[i]);
        }
    }

    public void Show(Vector3 point)
    {
        var effect = _plusOneEffectsQueue.Dequeue();
        effect.Show(point);

        _plusOneEffectsQueue.Enqueue(effect);
    }
}
