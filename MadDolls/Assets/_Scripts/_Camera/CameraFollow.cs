﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> Targets;

    [Header("Movement")]
    public Vector3 CameraOffset;
    public float CameraSmoothTime = .5f;

    [Header("Zoom system")]
    public float MinZoomFOV = 70f;
    public float MaxZoomFOV = 40f;
    public float MaxDistanceLimiter = 15f;
    public float ZoomSpeed = 10f;

    private Camera cameraComponent;
    private Transform tf;
    private Vector3 velocity;
    private Bounds bounds;

    public GameObject bomb;

    void Start()
    {
        tf = transform;
        cameraComponent = Camera.main;
    }

    void SpawnBomb()
    {
        Instantiate(bomb, Targets[0].position + new Vector3(Random.Range(-15f, 15f), Random.Range(20f,30f), 0), Quaternion.identity).SetActive(true);
    }
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            KU.StartTimer(SpawnBomb, 1f, true);
        }
        if (Targets.Count == 0)
            return;

        SetBounds();
        Move();
        //Zoom();
    }

    void SetBounds()
    {
        bounds = new Bounds(Targets[0].position, Vector3.zero);

        for(int i = 1; i<Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }
    }

    float GetGreatestDistance()
    {
        return Mathf.Max(bounds.size.x, bounds.size.z);
    }

    void Move()
    {
        tf.position = Vector3.SmoothDamp(tf.position, bounds.center + CameraOffset, ref velocity, CameraSmoothTime);
    }

    void Zoom()
    {
        float newFOV = Mathf.Lerp(MinZoomFOV, MaxZoomFOV, GetGreatestDistance() / MaxDistanceLimiter);
        newFOV = Mathf.Lerp(cameraComponent.fieldOfView, newFOV, Time.deltaTime);
        cameraComponent.fieldOfView = newFOV;
    }
}
