using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : RangedWeapon
{
    protected override void GenerateBulletVFX(Vector3 hitPosition, Vector3 aimDirection)
    {
        base.GenerateBulletVFX(hitPosition, aimDirection);
        bulletTrail.transform.rotation = Quaternion.LookRotation(aimDirection);
        bulletTrail.Emit(bulletTrail.emission.GetBurst(0).maxCount);
    }
}
