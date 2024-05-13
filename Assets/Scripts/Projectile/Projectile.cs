using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _Movement_Speed;

    [SerializeField] private TrailRenderer _Trail;

    private Action<Projectile> _On_Collided;

    private float _Damage;

    private Transform _Transform;

    private Vector3 _Direction;
    
    private void Awake()
    {
        _Transform = transform;

        _Direction = Vector3.forward;
    }

    public void Init(Action<Projectile> _action, float _damage)
    {
        _On_Collided += _action;
        _Damage = _damage;

        StartCoroutine(Move());
        
        _Trail.gameObject.SetActive(true);
    }
    
    private IEnumerator Move()
    {
        float _movement_Speed = _Movement_Speed;
        
        while (true)
        {
            _Transform.Translate(_Direction * _movement_Speed * Time.deltaTime);

            yield return null;
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _enemy))
        {
            _enemy.TakeDamage(_Damage);
            _On_Collided?.Invoke(this);
        }
        
        if (other.CompareTag("Border"))
            _On_Collided?.Invoke(this);
    }

    private void OnDisable()
    {
        _On_Collided = null;
        
        _Trail.gameObject.SetActive(false);
    }
}