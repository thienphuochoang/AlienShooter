using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Animator _animator;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _animator = GetComponent<Animator>();
        _healthSystem.onDead += HealthSystem_OnDead;
        _healthSystem.onDamaged += HealthSystem_OnDamaged;
    }

    private void HealthSystem_OnDead()
    {
        TriggerDeathAnimation();
    }

    private void HealthSystem_OnDamaged(float amountOfDamage)
    {
        
    }

    private void TriggerDeathAnimation()
    {
        _animator.SetTrigger("isDead");
    }
}
