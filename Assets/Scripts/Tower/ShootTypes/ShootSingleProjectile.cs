using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingleProjectile : IShootType
{
    private Projectile           prefab = null;
    
    private Transform            muzzle = null;
    
    private List<Projectile>     projectilePool;

    private Action<Projectile>   returnAction;
    
    private float                damage = 0f;

    public ShootSingleProjectile(Projectile _prefab, Transform _muzzle, List<Projectile> _pool, Action<Projectile> _return_Action, float _damage)
    {
        projectilePool = new List<Projectile>();
        
        prefab = _prefab;
        muzzle = _muzzle;
        projectilePool = _pool;
        returnAction = _return_Action;
        damage = _damage;
    }
    
    public void Shoot() => CreateProjectile();

    public void CreateProjectile()
    {
        Projectile _projectile =
            ObjectPool.PoolInstantiate(prefab, muzzle.position, muzzle.rotation, projectilePool);
        
        _projectile.Init(returnAction, damage);
    }
}