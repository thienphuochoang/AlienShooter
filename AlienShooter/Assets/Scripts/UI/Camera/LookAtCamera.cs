using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField]
    private bool invert = true;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            //Vector3 cameraDirection = (cameraTransform.position - transform.position).normalized;
            Vector3 cameraDirection = (transform.position - cameraTransform.position).normalized;
            transform.LookAt(transform.position + cameraDirection);
        }
        else
            transform.LookAt(cameraTransform);
    }
}