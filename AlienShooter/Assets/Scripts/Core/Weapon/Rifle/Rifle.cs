using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : RangedWeapon
{
    protected override void GenerateBulletVFX(Vector3 hitPosition)
    {
        base.GenerateBulletVFX(hitPosition);
        TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
        //StartCoroutine(SpawnTrail(trail, hitPosition))
    }
}
