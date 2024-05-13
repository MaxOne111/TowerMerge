using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private List<Projectile> _Pool;
    [SerializeField] private Transform _Pool_Transform;

    public List<Projectile> Pool => _Pool;
    public Transform PoolTransform => _Pool_Transform;
}