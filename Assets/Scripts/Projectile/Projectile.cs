using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float           movementSpeed = 7.5f;

    [SerializeField] private TrailRenderer   trail = null;

    private event Action<Projectile>         OnCollided = null;

    private float                            damage = 0;

    private Transform                        _transform = null;

    private Vector3                          direction;
    
    private void Awake()
    {
        _transform = transform;

        direction = Vector3.forward;
    }

    public void Init(Action<Projectile> _action, float _damage)
    {
        OnCollided += _action;
        
        damage = _damage;

        StartCoroutine(Move());
        
        trail.gameObject.SetActive(true);
    }
    
    private IEnumerator Move()
    {
        float _movement_Speed = movementSpeed;
        
        while (true)
        {
            _transform.Translate(direction * _movement_Speed * Time.deltaTime);

            yield return null;
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _enemy))
        {
            _enemy.TakeDamage(damage);
            OnCollided?.Invoke(this);
        }

        if (other.CompareTag("Border"))
        {
            OnCollided?.Invoke(this);
        }
            
    }

    private void OnDisable()
    {
        OnCollided = null;
        
        trail.gameObject.SetActive(false);
    }
}