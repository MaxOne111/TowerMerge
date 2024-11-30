using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private TowerConfig      config = null;
    
    [SerializeField] protected float          damage = 10f;
    [SerializeField] private float            fireRate = 1f;
    [SerializeField] private float            rotationSpeed = 15f;
    [SerializeField] private float            distanceToTarget = 5f;
    
    [SerializeField] protected Transform      shootPoint = null;
    [SerializeField] protected Transform      muzzle = null;
    [SerializeField] private Transform        head = null;

    [SerializeField] private DOShake          shakeScale;

    [SerializeField] private ParticleSystem   appearanceParticles = null;

    [SerializeField] protected TowerAudio     towerAudio = null;

    private event Action<Tower>               OnDestroyed = null;
    
    #region IShootType

    protected IShootType shootType = null;

    #endregion
    
    private Transform                         _transform = null;
    
    private float                             currentDisacance = 0f;

    private float                             lastFire = 0;

    private bool                              isActive = true;
    
    public int                                Level => config.Level;
    
    public Tower                              NextTower => config.NextTower;
    
    public bool                               IsMaxLevel => config.IsMaxLevel;

    public Transform Target
    {
        get;
        set;
    }

    public PlayerCell Cell
    {
        get; 
        private set;
    }

    protected abstract void Shoot();
    
    public virtual void Init(Action<Tower> _action, List<Projectile> _pool, Transform _pool_Transform) => OnDestroyed += _action;

    protected virtual void ShootEffect()
    {
    }

    private void Awake()
    {
        GameEvents.OnPlayerDefeated += Deactivate;

        _transform = transform;
    }

    private void Start()
    {
        AppearanceEffect();
        
        Activate();
    }
    
    public void Activate() => StartCoroutine(Working());
    
    private IEnumerator Working()
    {
        isActive = true;
        
        while (isActive)
        {
            RotateHead();

            Shoot();

            yield return null;
        }
    }

    protected bool IsEnemyClosely()
    {
        currentDisacance = Vector3.Distance(_transform.position, Target.position);
        
        if (currentDisacance > distanceToTarget)
        {
            return false;
        }
        
        return true;
    }
    
    private void RotateHead()
    {
        if (!Target || !IsEnemyClosely())
        {
            return;
        }
        
        head.localRotation = Quaternion.RotateTowards(head.localRotation,
            Quaternion.LookRotation(Target.position - head.position),
            10 * Time.deltaTime * rotationSpeed);
        
        head.localRotation = Quaternion.Euler(0, head.eulerAngles.y, 0);
    }
    
    public void OccupiedCell(PlayerCell _cell) => Cell = _cell;
    
    protected bool ShootCooldown()
    {
        if (Time.time < lastFire)
        {
            return false;
        }
        
        lastFire = Time.time + 1f / fireRate;

        return true;
    }
    
    private void AppearanceEffect()
    {
        transform.DOShakeScale(shakeScale.duration,
            shakeScale.strength,
            shakeScale.vibrato,
            shakeScale.randomness,
            shakeScale.fadeOut).SetLink(gameObject);
        
        appearanceParticles.gameObject.SetActive(true);
        appearanceParticles.transform.SetParent(null);
    }

    public void Deactivate()
    {
        head.localRotation = Quaternion.Euler(Vector3.zero);
        isActive = false;
    }

    public void DestroyTower() => OnDestroyed?.Invoke(this);
    
    private void OnDestroy() => GameEvents.OnPlayerDefeated -= Deactivate;
}