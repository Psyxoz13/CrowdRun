using UnityEngine;

public static class LayerMaskNumber
{
    public static int GetNumber(this LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }
}
