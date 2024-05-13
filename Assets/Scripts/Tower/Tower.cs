using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private TowerConfig _Config;
    
    [SerializeField] protected float _Damage;

    [SerializeField] private float _Fire_Rate;
    [SerializeField] private float _Rotation_Speed;
    
    [SerializeField] protected Transform _Shoot_Point;
    [SerializeField] protected Transform _Muzzle;
    [SerializeField] private Transform _Head;

    [SerializeField] private DOShake _Shake_Scale;

    [SerializeField] private ParticleSystem _Appearance_Particle;

    [SerializeField] protected TowerAudio _Tower_Audio;
    
    protected IShootType _Shoot_Type;
    
    private event Action<Tower> _On_Destroyed;
    
    private float _Last_Fire;

    private bool _Is_Active;

    public Transform Target { get; set; }
    public int Level => _Config.Level;
    public Tower NextTower => _Config.NextTower;
    public bool IsMaxLevel => _Config.IsMaxLevel;
    
    public PlayerCell Cell { get; private set; }

    protected abstract void Shoot();
    
    public virtual void Init(Action<Tower> _action, List<Projectile> _pool, Transform _pool_Transform) => _On_Destroyed += _action;
    
    protected virtual void ShootEffect() { }

    private void Awake() => GameEvents._On_Player_Defeated += Deactivate;

    private void Start()
    {
        AppearanceEffect();
        
        Activate();
    }
    
    public void Activate() => StartCoroutine(Working());
    
    private IEnumerator Working()
    {
        _Is_Active = true;
        
        while (_Is_Active)
        {
            RotateHead();

            Shoot();

            yield return null;
        }
    }
    
    private void RotateHead()
    {
        if (!Target)
            return;
        
        _Head.localRotation = Quaternion.RotateTowards(_Head.localRotation,
            Quaternion.LookRotation(Target.position - _Head.position),
            10 * Time.deltaTime * _Rotation_Speed);
        
        _Head.localRotation = Quaternion.Euler(0, _Head.eulerAngles.y, 0);
    }
    
    public void OccupiedCell(PlayerCell _cell) => Cell = _cell;
    
    protected bool ShootCooldown()
    {
        if(Time.time < _Last_Fire)
            return false;

        _Last_Fire = Time.time + 1f / _Fire_Rate;

        return true;
    }
    
    private void AppearanceEffect()
    {
        transform.DOShakeScale(_Shake_Scale._Duration,
            _Shake_Scale._Strenghth,
            _Shake_Scale._Vibrato,
            _Shake_Scale._Randomness,
            _Shake_Scale._Fade_Out).SetLink(gameObject);
        
        _Appearance_Particle.gameObject.SetActive(true);
        _Appearance_Particle.transform.SetParent(null);
    }

    public void Deactivate()
    {
        _Head.localRotation = Quaternion.Euler(Vector3.zero);
        _Is_Active = false;
    }

    public void DestroyTower() => _On_Destroyed?.Invoke(this);
    
    private void OnDestroy() => GameEvents._On_Player_Defeated -= Deactivate;
}