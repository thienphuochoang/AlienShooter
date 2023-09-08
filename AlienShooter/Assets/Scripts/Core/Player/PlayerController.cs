using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed;
    private Vector2 _moveInput;
    private Camera _mainCamera;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private float turnSpeed = 30f;
    private void Start()
    {
        _mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        joystick.onStickValueUpdated += Joystick_OnStickValueUpdated;
    }

    private void Update()
    {
        Vector3 rightDirection = _mainCamera.transform.right;
        Vector3 upDirection = Vector3.Cross(rightDirection, Vector3.up);
        Vector3 moveDirection = rightDirection * _moveInput.x + upDirection * _moveInput.y;
        characterController.Move(moveDirection * (Time.deltaTime * moveSpeed));
        if (_moveInput.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(moveDirection, Vector3.up), 
                turnLerpAlpha);
            cameraController.AddYawInput(_moveInput.x);
        }
    }

    private void Joystick_OnStickValueUpdated(Vector2 inputValue)
    {
        _moveInput = inputValue;
    }
}
