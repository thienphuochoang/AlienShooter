using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBehaviourTree, ITeam
{
    private HealthSystem _healthSystem;
    private Animator _animator;
    private PerceptionComponent _perceptionComponent;
    private BehaviourTree _behaviourTree;
    [SerializeField]
    private float turnSpeed = 8f;

    [SerializeField] private int teamID = 2;
    public int GetTeamID() => teamID;
    
    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _animator = GetComponent<Animator>();
        _perceptionComponent = GetComponent<PerceptionComponent>();
        _behaviourTree = GetComponent<BehaviourTree>();
        _healthSystem.onDead += HealthSystem_OnDead;
        _healthSystem.onDamaged += HealthSystem_OnDamaged;
        _perceptionComponent.onPerceptionTargetChanged += PerceptionComponent_onPerceptionTargetChanged;
    }

    private void PerceptionComponent_onPerceptionTargetChanged(GameObject target, bool sensed)
    {
        if (sensed)
        {
            _behaviourTree.Blackboard.AddData("Target", target);
        }
        else
        {
            _behaviourTree.Blackboard.AddData("LastSeenLocation", target.transform.position);
            _behaviourTree.Blackboard.RemoveData("Target");
        }
    }

    private void HealthSystem_OnDead()
    {
        TriggerDeathAnimation();
    }

    private void HealthSystem_OnDamaged(float amountOfDamage, GameObject attacker)
    {
        
    }

    private void TriggerDeathAnimation()
    {
        _animator.SetTrigger("isDead");
    }
    public void TriggerWalkAnimation()
    {
        _animator.SetBool("Walk", true);
    }
    public void CancelWalkAnimation()
    {
        _animator.SetBool("Walk", false);
    }
    public void TriggerRunAnimation()
    {
        _animator.SetBool("Run", true);
    }
    public void CancelRunAnimation()
    {
        _animator.SetBool("Run", false);
    }

    private void OnDrawGizmos()
    {
        if (!_behaviourTree) return;
        if (_behaviourTree.Blackboard.GetData("Target", out GameObject target))
        {
            Vector3 drawTargetPosition = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPosition, 0.7f);
            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPosition);
        }
    }

    public void RotateTowards(GameObject target)
    {
        Vector3 aimDirection = target.transform.position - transform.position;
        aimDirection.y = 0;
        aimDirection = aimDirection.normalized;
        RotateTowards(aimDirection);
    }

    public void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }

    private void RotateTowards(Vector3 aimDirection)
    {
        float currentTurnSpeed = 0f;
        if (aimDirection.magnitude != 0)
        {
            Quaternion previousRotation = transform.rotation;
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnLerpAlpha);
            Quaternion currentRotation = transform.rotation;
            float direction = Vector3.Dot(aimDirection, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(previousRotation, currentRotation) * direction;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
    }
}
