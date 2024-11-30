using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileTower : Tower
{
    [SerializeField] private DOPunch      muzzlePunch;
    
    [SerializeField] private Projectile   projectilePrefab = null;
    
    private List<Projectile>              projectilePool = null;
    
    private Transform                     projectilePoolTransform = null;
    
    public override void Init(Action<Tower> _action, List<Projectile> _pool, Transform _pool_Transform)
    {
        projectilePool = new List<Projectile>();
        
        base.Init(_action, _pool, _pool_Transform);
        
        projectilePool = _pool;
        projectilePoolTransform = _pool_Transform;
    }
    
    protected override void Shoot()
    {
        if (!Target)
        {
            return;
        }

        if (!ShootCooldown() || !IsEnemyClosely())
        {
            return;
        }

        shootType = new ShootSingleProjectile(projectilePrefab, shootPoint, projectilePool, ReturnProjectileToPool, damage);
        
        ShootEffect();
        
        shootType.Shoot();
        
        towerAudio.PlayShootSound();
    }

    protected override void ShootEffect()
    {
        muzzle.transform.DOPunchScale(muzzlePunch.punch,
                muzzlePunch.duration,
                muzzlePunch.vibrato,
                muzzlePunch.elasticity)
            .SetLink(muzzle.gameObject);
    }
    
    private void ReturnProjectileToPool(Projectile _projectile)
    {
        _projectile.transform.SetParent(projectilePoolTransform);
        
        ObjectPool.ReturnToPool(_projectile, projectilePool);
    }
}