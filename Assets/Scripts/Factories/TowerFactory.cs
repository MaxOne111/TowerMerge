using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerFactory : GenericFactory<Tower>
{
    [SerializeField] private List<PlayerCell> _Player_Cells;

    [SerializeField] private ProjectilePool _Projectile_Pool;
    
    [SerializeField] private float _Spawn_Frequency;

    [SerializeField] private AudioClip _Tower_Sound;

    public static event Action<List<Tower>> _On_List_Changed ;

    private List<Tower> _Towers = new List<Tower>();

    private PlayerCell _Empty_Cell;
    
    private bool _Is_Creating = true;

    private AudioSource _Audio_Source;

    private void Awake() => _Audio_Source = GetComponent<AudioSource>();

    public void GlobalInit()
    {
        GameEvents._On_Player_Defeated += StopCreate;
        GameEvents._On_Player_Won += StopCreate;
    }
    
    public void StartCreating() => StartCoroutine(TowerCreating());

    public Tower Create(Tower _prefab, PlayerCell _cell)
    {
        Tower _tower = Instantiate(_prefab ,_cell.SpawnPoint.position, _cell.SpawnPoint.rotation);
            
        _tower.Init(DestroyTower, _Projectile_Pool.Pool, _Projectile_Pool.PoolTransform);
            
        _Towers.Add(_tower);
            
        _cell.OccupiedCell(_tower);
        
        _tower.OccupiedCell(_cell);

        _On_List_Changed?.Invoke(_Towers);
        
        _Audio_Source.PlayOneShot(_Tower_Sound);

        return _tower;
    }
    
    private IEnumerator TowerCreating()
    {
        float _start_Delay = 1;
        
        yield return new WaitForSeconds(_start_Delay);

        _Is_Creating = true;
        
        while (_Is_Creating)
        {
            _Empty_Cell = GetEmptyCell();

            if (!_Empty_Cell)
            {
                StartCreating();
                yield break;
            }

            if (!_Is_Creating)
                yield break;
            
            Create(_Prefab , _Empty_Cell);
            
            yield return new WaitForSeconds(_Spawn_Frequency);

            yield return null;
        }
    }

    private PlayerCell GetEmptyCell()
    {
        for (int i = 0; i < _Player_Cells.Count; i++)
        {
            if (_Player_Cells[i].IsEmpty)
                return _Player_Cells[i];
        }

        return null;
    }
    

    private void DestroyTower(Tower _tower)
    {
        _Towers.Remove(_tower);
        _On_List_Changed?.Invoke(_Towers);
        Destroy(_tower.gameObject);
    }
    
    private void StopCreate() => _Is_Creating = false;

    private void OnDestroy()
    {
        GameEvents._On_Player_Defeated -= StopCreate;
        GameEvents._On_Player_Won -= StopCreate;
    }
    
}