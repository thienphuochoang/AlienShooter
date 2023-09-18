using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _fillBar;
    private Enemy _enemy;
    private HealthSystem _healthSystem;

    private void Awake()
    {
    }

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _healthSystem = GetComponentInParent<HealthSystem>();
        _healthSystem.onDamaged += UpdateFillHPBar;
        _healthSystem.onDead += DisableHealthBarUI;
        UpdateFillHPBar(0, null);
    }

    private void UpdateFillHPBar(float amountOfDamage, GameObject attacker)
    {
        _fillBar.value = _healthSystem.GetHealthNormalized();
    }

    private void DisableHealthBarUI()
    {
        this.gameObject.SetActive(false);
    }
}
