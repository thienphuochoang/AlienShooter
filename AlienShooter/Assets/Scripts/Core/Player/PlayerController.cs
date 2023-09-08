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
        characterController.Move((rightDirection * _moveInput.x + upDirection * _moveInput.y) * (Time.deltaTime * moveSpeed));
    }

    private void Joystick_OnStickValueUpdated(Vector2 inputValue)
    {
        _moveInput = inputValue;
    }
}
