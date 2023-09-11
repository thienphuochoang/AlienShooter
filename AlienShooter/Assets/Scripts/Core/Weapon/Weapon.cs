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

    public void Init(GameObject inputOwner)
    {
        owner = inputOwner;
        Unequip();
    }

    public Types GetWeaponType() => type;
        

    public void Equip()
    {
        gameObject.SetActive(true);
        owner.GetComponent<Animator>().runtimeAnimatorController = _overrideController;
    }

    public void Unequip()
    {
        gameObject.SetActive(false);
    }
}
