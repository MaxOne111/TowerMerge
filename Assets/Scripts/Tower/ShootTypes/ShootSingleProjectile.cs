using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootSingleProjectile : IShootType
{
    private Projectile _Prefab;
    
    private Transform _Muzzle;
    
    private List<Projectile> _Projectile_Pool;

    private Action<Projectile> _Return_Action;
    
    private float _Damage;

    public ShootSingleProjectile(Projectile _prefab, Transform _muzzle, List<Projectile> _pool, Action<Projectile> _return_Action, float _damage)
    {
        _Prefab = _prefab;
        _Muzzle = _muzzle;
        _Projectile_Pool = _pool;
        _Return_Action = _return_Action;
        _Damage = _damage;
    }
    
    public void Shoot() => CreateProjectile();

    public void CreateProjectile()
    {
        Projectile _projectile =
            ObjectPool.PoolInstantiate(_Prefab, _Muzzle.position, _Muzzle.rotation, _Projectile_Pool);
        
        _projectile.Init(_Return_Action, _Damage);
    }
}