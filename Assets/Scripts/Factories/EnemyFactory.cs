using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : GenericFactory<Enemy>
{
    [SerializeField] private int _Enemies_Per_Round;
    
    [SerializeField] private Transform[] _Spawn_Points;

    [SerializeField] private float _Start_Spawn_Frequency;
    [SerializeField] private float _Spawm_Frequency_Step;
    [SerializeField] private float _Spawm_Frequency_Low_Limit;
    
    [SerializeField] private Transform _Enemy_Pool_Transform;
    
    public static event Action<List<Enemy>> _On_List_Changed;

    private static List<Enemy> _Enemies_On_Scene;
    
    private static List<Enemy> _Enemy_Pool;

    private static int _Enemies_Left_Count;
    
    private bool _Is_Creating = true;

    public void GlobalInit()
    {
        GameEvents._On_Player_Defeated += StopCreate;
        GameEvents._On_Player_Won += StopCreate;
    }

    public void StartCreating()
    {
        _Enemies_On_Scene = new List<Enemy>();

        _Enemy_Pool = new List<Enemy>();
        
        _Enemies_Left_Count = SaveLoad._Player_Data._Level * _Enemies_Per_Round;

        StartCoroutine(EnemyCreating());
    }

    private  IEnumerator EnemyCreating()
    {
        if (_Spawn_Points.Length == 0)
        {
            Debug.LogError("Spawn points list is empty");
            yield break;
        }
        
        float _start_Delay = 1;
        
        yield return new WaitForSeconds(_start_Delay);
        
        _Is_Creating = true;

        float _current_Spawn_Frequency = _Start_Spawn_Frequency;
        
        while (_Is_Creating && _Enemies_Left_Count > 0)
        {
            int _point_Index = Random.Range(0, _Spawn_Points.Length);

            Transform _spawn_Point = _Spawn_Points[_point_Index];

            Enemy _enemy = ObjectPool.PoolInstantiate(_Prefab, _spawn_Point.position, _spawn_Point.rotation, _Enemy_Pool);

            _Enemies_On_Scene.Add(_enemy);
            
            _enemy.transform.SetParent(null);
            
            _enemy.Init(ReturnToPool);

            _Enemies_Left_Count--;

            _On_List_Changed?.Invoke(_Enemies_On_Scene);
            
            if (_current_Spawn_Frequency - _Spawm_Frequency_Step >= _Spawm_Frequency_Low_Limit)
                _current_Spawn_Frequency -= _Spawm_Frequency_Step;

            yield return new WaitForSeconds(_current_Spawn_Frequency);

            yield return null;
        }
        
    }

    private void ReturnToPool(Enemy _enemy)
    {
        ObjectPool.ReturnToPool(_enemy, _Enemy_Pool);

        _enemy.transform.SetParent(_Enemy_Pool_Transform);
        
        _Enemies_On_Scene.Remove(_enemy);
        
        _On_List_Changed?.Invoke(_Enemies_On_Scene);
        
        if (_Enemies_On_Scene.Count == 0 && _Enemies_Left_Count == 0)
            GameEvents.OnPlayerWon();
    }

    private void StopCreate() => _Is_Creating = false;
    
    private void OnDestroy()
    {
        GameEvents._On_Player_Defeated -= StopCreate;
        GameEvents._On_Player_Won -= StopCreate;
    }
}
