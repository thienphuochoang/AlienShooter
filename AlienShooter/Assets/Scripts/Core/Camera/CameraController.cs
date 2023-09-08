using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float turnSpeed = 200f;

    private void Start()
    {
        transform.position = playerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position;
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
    }
}
