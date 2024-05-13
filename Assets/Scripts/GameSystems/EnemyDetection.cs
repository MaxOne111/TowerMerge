using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private List<Enemy> _Enemies;

    public static event Action<List<Enemy>> _On_Targets_Ready;
    
    public void GlobalInit() => EnemyFactory._On_List_Changed += DetectEnemy;

    private void DetectEnemy(List<Enemy> _enemies)
    {
        _Enemies = _enemies;

        if (_Enemies.Count <= 1)
        {
            _On_Targets_Ready?.Invoke(_Enemies);
            return;
        }
        
        _Enemies.Sort((x,y)=>x.transform.position.z.CompareTo(y.transform.position.z));
        
        _On_Targets_Ready?.Invoke(_Enemies);
    }
    
    private void OnDestroy()
    {
        EnemyFactory._On_List_Changed -= DetectEnemy;
    }
}