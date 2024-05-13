using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileTower : Tower
{
    [SerializeField] private DOPunch _Muzzle_Punch;
    
    [SerializeField] private Projectile _Prefab;
    
    private List<Projectile> _Projectile_Pool;
    
    private Transform _Projectile_Pool_Transform;
    
    public override void Init(Action<Tower> _action, List<Projectile> _pool, Transform _pool_Transform)
    {
        base.Init(_action, _pool, _pool_Transform);
        _Projectile_Pool = _pool;
        _Projectile_Pool_Transform = _pool_Transform;
    }
    
    protected override void Shoot()
    {
        if (!ShootCooldown() || !Target)
            return;
        
        _Shoot_Type = new ShootSingleProjectile(_Prefab, _Shoot_Point, _Projectile_Pool, ReturnProjectileToPool, _Damage);
        
        ShootEffect();
        
        _Shoot_Type.Shoot();
        
        _Tower_Audio.PlayShootSound();
    }

    protected override void ShootEffect()
    {
        _Muzzle.transform.DOPunchScale(_Muzzle_Punch._Punch,
                _Muzzle_Punch._Duration,
                _Muzzle_Punch._Vibrato,
                _Muzzle_Punch._Elasticity)
            .SetLink(_Muzzle.gameObject);
    }
    
    private void ReturnProjectileToPool(Projectile _projectile)
    {
        _projectile.transform.SetParent(_Projectile_Pool_Transform);
        
        ObjectPool.ReturnToPool(_projectile, _Projectile_Pool);
    }
}