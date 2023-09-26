using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    private HealthSystem _playerHealthSystem;
    [SerializeField] private Slider hpFillBar;
    [SerializeField] private TextMeshProUGUI hpText;

    public void UpdateHealth(float amountOfHealth, GameObject attacker)
    {
        hpFillBar.value = _playerHealthSystem.GetHealthNormalized();
        if (_playerHealthSystem.GetHealth() <= 0)
            hpText.text = "0/100";
        else
            hpText.text = _playerHealthSystem.GetHealth().ToString() + "/100";
    }

    private void Start()
    {
        _playerHealthSystem = _player.GetComponent<HealthSystem>();
        UpdateHealth(0, null);
    }
    
}
