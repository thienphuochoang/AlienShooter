using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : SenseSystem
{
    [SerializeField]
    private float sightDistance = 5f;
    [SerializeField]
    private float sightHalfAngle = 5f;
    [SerializeField]
    private float eyeHeight = 1f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {

        float distance = Vector3.Distance(stimuli.transform.position, transform.position);
        if (distance > sightDistance)
        {
            return false;
        }

        Vector3 forwardDirection = transform.forward;
        Vector3 stimuliDirection = (stimuli.transform.position - transform.position).normalized;
        if (Vector3.Angle(forwardDirection, stimuliDirection) > sightHalfAngle)
        {
            return false;
        }

        if (Physics.Raycast(transform.position + Vector3.up * eyeHeight, stimuliDirection, out RaycastHit hitInfo, sightDistance))
        {
            if (hitInfo.collider.gameObject != stimuli.gameObject)
            {
                return false;
            }
        }

        return true;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + Vector3.up * eyeHeight;
        Gizmos.DrawWireSphere(drawCenter, sightDistance);
        Vector3 leftLimitDirection = Quaternion.AngleAxis(sightHalfAngle, Vector3.up) * transform.forward;
        Vector3 rightLimitDirection = Quaternion.AngleAxis(-sightHalfAngle, Vector3.up) * transform.forward;
        
        Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDirection * sightDistance);
        Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDirection * sightDistance);
    }
}
