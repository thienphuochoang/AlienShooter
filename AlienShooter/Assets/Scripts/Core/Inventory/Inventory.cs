using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] initialWeaponPrefabs;
    [SerializeField] private WeaponAttachSlot defaultWeaponSlot;
    [SerializeField] private WeaponAttachSlot[] weaponSlots;
    private int _currentWeaponIndex = -1;
    private List<Weapon> _weapons;
    private void Start()
    {
        InitWeapons();
    }

    private void InitWeapons()
    {
        _weapons = new List<Weapon>();
        foreach (Weapon weapon in initialWeaponPrefabs)
        {
            WeaponAttachSlot weaponSlot = defaultWeaponSlot;
            foreach (WeaponAttachSlot slot in weaponSlots)
            {
                if (slot.GetAttachSlotType() == weapon.GetWeaponType())
                {
                    weaponSlot = slot;
                }
            }

            Weapon newWeapon = Instantiate(weapon, weaponSlot.gameObject.transform);
            newWeapon.Init(this.gameObject);
            _weapons.Add(newWeapon);
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = _currentWeaponIndex + 1;
        if (nextWeaponIndex >= _weapons.Count)
            nextWeaponIndex = 0;
        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= _weapons.Count)
            return;

        if (_currentWeaponIndex >= 0 && _currentWeaponIndex < _weapons.Count)
        {
            _weapons[_currentWeaponIndex].Unequip();
        }
        _weapons[weaponIndex].Equip();
        _currentWeaponIndex = weaponIndex;
    }
}
