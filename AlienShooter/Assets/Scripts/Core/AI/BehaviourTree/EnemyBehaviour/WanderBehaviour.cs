using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderBehaviour : MonoBehaviour
{
    public float wanderRadius = 10f; // Radius within which the zombie will wander.
    /*private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = 0f;
    }*/

    /*private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }*/

    // Generate a random point within a sphere for wandering.
    private Vector3 RandomWanderingLocation(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public bool GetNextWanderingLocation(out Vector3 point)
    {
        point = RandomWanderingLocation(transform.position, wanderRadius, -1);
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
