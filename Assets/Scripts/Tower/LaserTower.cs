using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private LineRenderer lineRenderer = null;
    
    protected override void Shoot()
    {
        if (!Target)
        {
            towerAudio.StopPlaying();
            
            return;
        }
        
        if (!IsEnemyClosely())
        {
            return;
        }
        
        shootType = new ShootLaser(shootPoint, Target, lineRenderer, damage);
        
        shootType.CreateProjectile();
        
        if (!towerAudio.IsPlaying())
        {
            towerAudio.PlayShootSound();
        }
        
        if (!ShootCooldown())
        {
            return;
        }
        
        shootType.Shoot();
        
    }
}