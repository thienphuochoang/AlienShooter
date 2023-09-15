using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = 100;

    public delegate void OnHealthChanged(float amountOfHealth);
    public event OnHealthChanged onHealthChanged;
    
    public delegate void OnDamaged(float amountOfDamage);
    public event OnDamaged onDamaged;
    
    public delegate void OnDead();
    public event OnDead onDead;

    private void Awake()
    {
        health = maxHealth;
    }

    public void ChangeHealth(float amountOfHealth)
    {
        if (amountOfHealth == 0 || health == 0) return;
        health += amountOfHealth;
        if (amountOfHealth < 0)
            onDamaged?.Invoke(amountOfHealth);
        onHealthChanged?.Invoke(amountOfHealth);

        if (health <= 0)
        {
            health = 0;
            Die();
        }
        Debug.Log("Current Health: " + health + ", " + this.gameObject.name);
    }

    private void Die()
    {
        onDead?.Invoke();
    }

    public float GetHealthNormalized()
    {
        return health / maxHealth;
    }

    public float GetHealth() => health;
    public float GetMaxHealth() => maxHealth;

}
