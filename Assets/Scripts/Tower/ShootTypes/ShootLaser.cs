using UnityEngine;

public class ShootLaser : IShootType
{
    private Transform      shootPoint = null;

    private LineRenderer   lineRenderer = null;

    private Transform      target = null;

    private Ray            ray;
    private RaycastHit     hit;

    private float          damage = 0f;

    private Vector3        zeroPosition;

    public ShootLaser(Transform _shoot_Point, Transform _target , LineRenderer _line_Renderer, float _damage)
    {
        zeroPosition = Vector3.zero;
        
        shootPoint = _shoot_Point;
        target = _target;
        lineRenderer = _line_Renderer;
        damage = _damage;
    }
    
    
    public void Shoot()
    {
        Vector3 _direction = shootPoint.forward;
        
        ray = new Ray(shootPoint.position, _direction);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Enemy _enemy))
            {
                _enemy.TakeDamage(damage);
            }
        }
    }

    public void CreateProjectile()
    {
        if (!target)
        {
            ResetLaser();
            
            return;
        }
        
        Vector3 _target = new Vector3(target.transform.position.x, target.transform.position.y + 0.35f,
            target.transform.position.z);

        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, _target);
    }

    public void ResetLaser()
    {
        lineRenderer.SetPosition(0, zeroPosition);
        lineRenderer.SetPosition(1, zeroPosition);
    }
}