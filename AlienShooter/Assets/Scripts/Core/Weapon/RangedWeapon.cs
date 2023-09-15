using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    protected ParticleSystem bulletTrail;
    [SerializeField]
    protected Transform bulletSpawnPoint;
    
    private AimSystem aimSystem;
    [SerializeField]
    protected float shootRate = 1;
    [SerializeField]
    protected int damage = 5;

    protected virtual void GenerateBulletVFX(Vector3 hitPosition, Vector3 aimDirection)
    {
    }

    protected override void Start()
    {
        base.Start();
        aimSystem = GetComponent<AimSystem>();
    }

    public override void Shoot()
    {
        AimSystem.HitInfo hitInfo = aimSystem.GetAimTarget(out Vector3 aimDirection);
        GameObject target = hitInfo.hitObject;
        if (target != null)
            Damage(target, damage);
        GenerateBulletVFX(hitInfo.hitPoint, aimDirection);
    }
    public override void Equip()
    {
        base.Equip();
        owner.GetComponent<Animator>().SetFloat("shootRate", shootRate);
    }
}
