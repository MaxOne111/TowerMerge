using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyFactory : GenericFactory<Enemy>
{
    [SerializeField] private int              enemiesPerRound = 10;
    
    [SerializeField] private Transform[]      spawnPoints = null;

    [SerializeField] private float            startSpawnFrequency = 3f;
    [SerializeField] private float            spawnFrequencyStep = 0.035f;
    [SerializeField] private float            spawnFrequencyLowLimit = 0.5f;
    
    [SerializeField] private Transform        enemyPoolTransform = null;
    
    public static event Action<List<Enemy>>   OnListChanged = null;

    private static List<Enemy>                enemiesOnScene = null;
    
    private static List<Enemy>                enemyPool = null;

    private static int                        enemiesLeftCount = 0;
    
    private bool                              isCreating = true;

    public void GlobalInit()
    {
        GameEvents.OnPlayerDefeated += StopCreate;
        GameEvents.OnPlayerWon += StopCreate;
    }

    public void StartCreating()
    {
        enemiesOnScene = new List<Enemy>();

        enemyPool = new List<Enemy>();
        
        enemiesLeftCount = SaveLoad.playerData.level * enemiesPerRound;

        StartCoroutine(EnemyCreating());
    }

    private  IEnumerator EnemyCreating()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points list is empty");
            yield break;
        }
        
        float _start_Delay = 1;
        
        yield return new WaitForSeconds(_start_Delay);
        
        isCreating = true;

        float _current_Spawn_Frequency = startSpawnFrequency;
        
        while (isCreating && enemiesLeftCount > 0)
        {
            int _point_Index = Random.Range(0, spawnPoints.Length);

            Transform _spawn_Point = spawnPoints[_point_Index];

            Enemy _enemy = ObjectPool.PoolInstantiate(prefab, _spawn_Point.position, _spawn_Point.rotation, enemyPool);

            enemiesOnScene.Add(_enemy);
            
            _enemy.transform.SetParent(null);
            
            _enemy.Init(ReturnToPool);

            enemiesLeftCount--;

            OnListChanged?.Invoke(enemiesOnScene);

            if (_current_Spawn_Frequency - spawnFrequencyStep >= spawnFrequencyLowLimit)
            {
                _current_Spawn_Frequency -= spawnFrequencyStep;
            }
            
            yield return new WaitForSeconds(_current_Spawn_Frequency);

            yield return null;
        }
    }

    private void ReturnToPool(Enemy _enemy)
    {
        ObjectPool.ReturnToPool(_enemy, enemyPool);

        _enemy.transform.SetParent(enemyPoolTransform);
        
        enemiesOnScene.Remove(_enemy);
        
        OnListChanged?.Invoke(enemiesOnScene);

        if (enemiesOnScene.Count == 0 && enemiesLeftCount == 0)
        {
            GameEvents.PlayerWon();
        }
    }

    private void StopCreate() => isCreating = false;
    
    private void OnDestroy()
    {
        GameEvents.OnPlayerDefeated -= StopCreate;
        GameEvents.OnPlayerWon -= StopCreate;
    }
}
