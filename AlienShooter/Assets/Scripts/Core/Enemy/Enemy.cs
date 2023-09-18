using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Animator _animator;
    private PerceptionComponent _perceptionComponent;
    private BehaviourTree _behaviourTree;
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
}
