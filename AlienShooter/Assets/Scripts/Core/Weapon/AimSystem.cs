using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float shootRange = 1000f;
    [SerializeField] private LayerMask shootMask;

    public GameObject GetAimTarget()
    {
        Vector3 aimStart = shootPosition.position;
        Vector3 aimDirection = shootPosition.forward;

        if (Physics.Raycast(aimStart, GetAimDirection(), out RaycastHit hitInfo, shootRange, shootMask))
        {
            return hitInfo.collider.gameObject;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(shootPosition.position, shootPosition.position + GetAimDirection() * shootRange);
    }

    private Vector3 GetAimDirection()
    {
        Vector3 aimDirection = shootPosition.forward;
        return new Vector3(aimDirection.x, 0, aimDirection.z);
    }
}
