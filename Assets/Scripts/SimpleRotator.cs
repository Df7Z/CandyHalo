using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRotator : MonoBehaviour
{
    public float f1;
    public float f2;
    private Vector3 Rot;

    private void Awake()
    {
        Rot = new Vector3(0, 0, 0);
        Rot.z = Random.Range(f1, f2);        
    }

    private void OnEnable()
    {
        Rot.z = Random.Range(f1, f2);
    }

    private void Update()
    {

        transform.rotation *= Quaternion.Euler(Rot);
    }
}
