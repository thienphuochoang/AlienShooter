using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float shootRange = 1000f;
    [SerializeField] private LayerMask shootMask;

    public struct HitInfo
    {
        public GameObject hitObject;
        public Vector3 hitPoint;

        public HitInfo(GameObject inputHitObject, Vector3 inputHitPoint)
        {
            hitObject = inputHitObject;
            hitPoint = inputHitPoint;
        }
    }
    public HitInfo GetAimTarget()
    {
        Vector3 aimStart = shootPosition.position;

        if (Physics.Raycast(aimStart, GetAimDirection(), out RaycastHit raycastHitInfo, shootRange, shootMask))
        {
            HitInfo hitInfo = new HitInfo(raycastHitInfo.collider.gameObject, raycastHitInfo.point);
            return hitInfo;
        }

        return new HitInfo(null, Vector3.positiveInfinity);
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
