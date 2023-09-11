using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachSlot : MonoBehaviour
{
    [SerializeField] private Weapon.Types attachSlotType;
    public Weapon.Types GetAttachSlotType() => attachSlotType;
}
