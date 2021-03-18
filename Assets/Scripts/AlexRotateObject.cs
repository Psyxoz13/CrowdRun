using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexRotateObject : MonoBehaviour
{
    [SerializeField] private float rotationFactor;
    Quaternion rotation;
    Quaternion originRotation;
    
    void Start()
    {
        originRotation = transform.rotation;
    }
        void FixedUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(rotationFactor * -1f, new Vector3(0, 1, 0));
    }
}
