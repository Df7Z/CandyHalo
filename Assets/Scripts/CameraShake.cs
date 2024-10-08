﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake instance;
    [HideInInspector]
    public float shakeTimer;        
    [HideInInspector]
    public float shakeAmount; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


	
	// Update is called once per frame
	void Update ()
    {

        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y , transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
	}

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeTimer = shakeDur;
        shakeAmount = shakePwr;
    }

}
