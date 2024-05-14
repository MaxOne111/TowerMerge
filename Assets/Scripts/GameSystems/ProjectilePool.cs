using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private List<Projectile>   pool = null;
    [SerializeField] private Transform          poolTransform = null;

    public List<Projectile> Pool => pool;
    
    public Transform PoolTransform => poolTransform;
}