using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _Start_Health;
    [SerializeField] private float _Movement_Speed;

    [SerializeField] private ParticleSystem _Damage_Particle;
    [SerializeField] private ParticleSystem _Death_Particle;
    
    private event Action<Enemy> _On_Killed;

    private Transform _Transform;

    private float _Current_Health;

    private bool _Is_Moving = true;

    private Vector3 _Death_Particle_Position;

    private void Awake()
    {
        _Transform = transform;
        
        _Death_Particle_Position = _Death_Particle.transform.localPosition;

        GameEvents._On_Player_Defeated += StopMove;
    }

    private void OnEnable()
    {
        _Death_Particle.transform.SetParent(_Transform);
        
        _Death_Particle.transform.localPosition = _Death_Particle_Position;
    }


    private IEnumerator Move()
    {
        Vector3 _direction = Vector3.forward;

        while (_Is_Moving)
        {
            _Transform.Translate(_direction * _Movement_Speed * Time.deltaTime);

            yield return null;
        }
    }

    private void StopMove() => _Is_Moving = false;

    public void Init(Action<Enemy> _action)
    {
        _On_Killed += _action;
        _Current_Health = _Start_Health;
        
        StartCoroutine(Move());
    }

    public void TakeDamage(float _damage)
    {
        if (_damage < 0)
            return;

        if (_damage >= _Current_Health)
        {
            _Current_Health = 0;
            Death();
        }

        _Current_Health -= _damage;
        
        _Damage_Particle.gameObject.SetActive(false);
        _Damage_Particle.gameObject.SetActive(true);
    }

    private void Death()
    {
        _Death_Particle.transform.SetParent(null);
        _Death_Particle.gameObject.SetActive(true);
        
        _On_Killed?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StopMove();
            GameEvents.OnPlayerDefeated();
        }
    }

    private void OnDisable() => _On_Killed = null;

    private void OnDestroy() => GameEvents._On_Player_Defeated -= StopMove;
}