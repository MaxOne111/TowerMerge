using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float            startHealth = 50;
    [SerializeField] private float            movementSpeed = 1.25f;

    [SerializeField] private ParticleSystem   damageParticle = null;
    [SerializeField] private ParticleSystem   deathParticle;
    
    private event Action<Enemy>               OnKilled = null;

    private Transform                         _transform = null;

    private float                             currentHealth = 50;

    private bool                              isMoving = true;

    private Vector3                           deathParticlePosition;

    private void Awake()
    {
        _transform = transform;
        
        deathParticlePosition = deathParticle.transform.localPosition;

        GameEvents.OnPlayerDefeated += StopMove;
    }

    private void OnEnable()
    {
        deathParticle.transform.SetParent(_transform);
        
        deathParticle.transform.localPosition = deathParticlePosition;
    }


    private IEnumerator Move()
    {
        Vector3 _direction = Vector3.forward;

        while (isMoving)
        {
            _transform.Translate(_direction * movementSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void StopMove() => isMoving = false;

    public void Init(Action<Enemy> _action)
    {
        OnKilled += _action;
        currentHealth = startHealth;
        
        StartCoroutine(Move());
    }

    public void TakeDamage(float _damage)
    {
        if (_damage < 0)
        {
            return;
        }
        
        if (_damage >= currentHealth)
        {
            currentHealth = 0;
            Death();
        }

        currentHealth -= _damage;
        
        damageParticle.gameObject.SetActive(false);
        damageParticle.gameObject.SetActive(true);
    }

    private void Death()
    {
        deathParticle.transform.SetParent(null);
        deathParticle.gameObject.SetActive(true);
        
        OnKilled?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StopMove();
            GameEvents.PlayerDefeated();
        }
    }

    private void OnDisable() => OnKilled = null;

    private void OnDestroy() => GameEvents.OnPlayerDefeated -= StopMove;
}