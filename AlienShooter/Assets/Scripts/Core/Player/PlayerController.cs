using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Joystick fireStick;
    [SerializeField]
    private Joystick moveStick;
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed;
    private Vector2 _fireInput;
    private Vector2 _moveInput;
    private Camera _mainCamera;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private float turnSpeed = 30f;

    private Animator _animator;
    private void Start()
    {
        _mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        fireStick.onStickValueUpdated += FireStick_OnStickValueUpdated;
        moveStick.onStickValueUpdated += MoveStick_OnStickValueUpdated;
        _animator = GetComponent<Animator>();
    }

    private Vector3 StickInputToWorldDirection(Vector2 inputValue)
    {
        Vector3 rightDirection = _mainCamera.transform.right;
        Vector3 upDirection = Vector3.Cross(rightDirection, Vector3.up);
        Vector3 moveDirection = rightDirection * inputValue.x + upDirection * inputValue.y;
        return moveDirection;
    }

    private void Update()
    {
        Vector3 moveDirection = StickInputToWorldDirection(_moveInput);
        characterController.Move(moveDirection * (Time.deltaTime * moveSpeed));
        
        Vector3 fireDirection = moveDirection;
        if (_fireInput.magnitude != 0)
        {
            fireDirection = StickInputToWorldDirection(_fireInput);
        }

        RotateTowards(fireDirection);
        UpdateCamera();

        float forward = Vector3.Dot(moveDirection, transform.forward);
        float right = Vector3.Dot(moveDirection, transform.right);
        
        _animator.SetFloat("forwardSpeed", forward);
        _animator.SetFloat("rightSpeed", right);
    }

    private void UpdateCamera()
    {
        if (_moveInput.magnitude != 0 && _fireInput.magnitude == 0)
        {
            cameraController.AddYawInput(_moveInput.x);
        }
    }

    private void RotateTowards(Vector3 fireDirection)
    {
        if (fireDirection.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(fireDirection, Vector3.up),
                turnLerpAlpha);
        }
    }

    private void FireStick_OnStickValueUpdated(Vector2 inputValue)
    {
        _fireInput = inputValue;
    }

    private void MoveStick_OnStickValueUpdated(Vector2 inputValue)
    {
        _moveInput = inputValue;
    }
}
