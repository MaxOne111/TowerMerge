using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private LineRenderer _Line_Renderer;
    
    protected override void Shoot()
    {
        _Shoot_Type = new ShootLaser(_Shoot_Point, Target, _Line_Renderer, _Damage);
        
        _Shoot_Type.CreateProjectile();

        if (!ShootCooldown() || !Target)
            return;

        if (!Target)
            _Tower_Audio.StopPlaying();

        _Shoot_Type.Shoot();

        if (!_Tower_Audio.IsPlaying())
        {
            _Tower_Audio.PlayShootSound();
        }
            
    }
}