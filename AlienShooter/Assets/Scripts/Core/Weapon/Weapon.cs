using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum Types
    {
        Rifle,
        Pistol
    }
    [SerializeField] private Types type;
    [SerializeField] private AnimatorOverrideController _overrideController;
    public GameObject owner { get; private set; }


    protected virtual void Start()
    {
        
    }

    public abstract void Shoot();
    public void Init(GameObject inputOwner)
    {
        owner = inputOwner;
        Unequip();
    }

    public Types GetWeaponType() => type;
        

    public virtual void Equip()
    {
        gameObject.SetActive(true);
        owner.GetComponent<Animator>().runtimeAnimatorController = _overrideController;
    }

    public void Unequip()
    {
        gameObject.SetActive(false);
    }

    protected void Damage(GameObject objToDamage, int damage)
    {
        HealthSystem healthSystem = objToDamage.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.ChangeHealth(-damage);
        }
    }
}
