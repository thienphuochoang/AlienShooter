using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        GameObject target = aimSystem.GetAimTarget();
        Debug.Log(target.name);
    }
}
