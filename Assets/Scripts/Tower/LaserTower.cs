using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private LineRenderer lineRenderer = null;
    
    protected override void Shoot()
    {
        shootType = new ShootLaser(shootPoint, Target, lineRenderer, damage);
        
        shootType.CreateProjectile();

        if (!ShootCooldown() || !Target)
        {
            return;
        }

        if (!Target)
        {
            towerAudio.StopPlaying();
        }
        
        shootType.Shoot();

        if (!towerAudio.IsPlaying())
        {
            towerAudio.PlayShootSound();
        }
    }
}