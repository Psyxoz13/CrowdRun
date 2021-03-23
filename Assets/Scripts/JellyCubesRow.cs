using System.Collections.Generic;
using UnityEngine;

public class JellyCubesRow : MonoBehaviour
{
    [SerializeField] private bool _automaticDetectCubes = true;

    public List<JellyCube> Cubes = new List<JellyCube>();

    private void OnValidate()
    {
        if (_automaticDetectCubes)
        {
            DetectCubes();
        }
    }

    private void DetectCubes()
    {
        Cubes.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Cubes.Add(transform.GetChild(i).GetComponent<JellyCube>());
        }
    }

    public void Explore()
    {
        for (int i = 0; i < Cubes.Count; i++)
        {
            var cube = Cubes[i];

            if (cube.gameObject.activeSelf)
                cube.SetNotExplosion();
        }
    }
}
