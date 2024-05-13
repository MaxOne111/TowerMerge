using UnityEngine;

public class ShootLaser : IShootType
{
    private Transform _Shoot_Point;

    private LineRenderer _Line_Renderer;

    public Transform _Target;

    private Ray _Ray;
    private RaycastHit _Hit;

    private float _Damage;
    
    Vector3 _Zero = Vector3.zero;

    public ShootLaser(Transform _shoot_Point, Transform _target , LineRenderer _line_Renderer, float _damage)
    {
        _Shoot_Point = _shoot_Point;
        _Target = _target;
        _Line_Renderer = _line_Renderer;
        _Damage = _damage;
    }
    
    
    public void Shoot()
    {
        Vector3 _direction = _Shoot_Point.forward;
        
        _Ray = new Ray(_Shoot_Point.position, _direction);
        
        if (Physics.Raycast(_Ray, out _Hit))
        {
            if (_Hit.collider.TryGetComponent(out Enemy _enemy))
                _enemy.TakeDamage(_Damage);
        }
    }

    public void CreateProjectile()
    {
        if (!_Target)
        {
            _Line_Renderer.SetPosition(0, _Zero);
            _Line_Renderer.SetPosition(1, _Zero);
            return;
        }
            
        
        Vector3 _target = new Vector3(_Target.transform.position.x, _Target.transform.position.y + 0.35f,
            _Target.transform.position.z);

        _Line_Renderer.SetPosition(0, _Shoot_Point.position);
        _Line_Renderer.SetPosition(1, _target);
    }
}