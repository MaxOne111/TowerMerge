using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDetection : MonoBehaviour
{
    public static event Action<List<Enemy>>   OnTargetsReady = null;
    
    private List<Enemy>                       enemies = null;
    
    public void GlobalInit() => EnemyFactory.OnListChanged += DetectEnemy;

    private void DetectEnemy(List<Enemy> _enemies)
    {
        enemies = new List<Enemy>();
        
        enemies = _enemies;

        if (enemies.Count <= 1)
        {
            OnTargetsReady?.Invoke(enemies);
            return;
        }
        
        enemies.Sort((x,y)=>x.transform.position.z.CompareTo(y.transform.position.z));
        
        OnTargetsReady?.Invoke(enemies);
    }
    
    private void OnDestroy() => EnemyFactory.OnListChanged -= DetectEnemy;
}