using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageComponent : DamageComponent
{
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider trigger;
    [SerializeField] private bool startedEnabled = false;

    public void SetDamageEnabled(bool isEnable)
    {
        trigger.enabled = isEnable;
    }

    private void Start()
    {
        SetDamageEnabled(startedEnabled);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ShouldDamage(other.gameObject))
            return;
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
            healthSystem.ChangeHealth(-damage, gameObject);
    }
}
