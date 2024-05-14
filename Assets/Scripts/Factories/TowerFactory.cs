using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class TowerFactory : GenericFactory<Tower>
{
    [SerializeField] private List<PlayerCell>   playerCells = null;

    [SerializeField] private ProjectilePool     projectilePool = null;
    
    [SerializeField] private float              spawnFrequency = 3f;

    [SerializeField] private AudioClip          towerSound = null;

    [SerializeField] private AudioSource        audioSource = null;
    public static event Action<List<Tower>>     OnListChanged = null;

    private List<Tower>                         towers = new List<Tower>();

    private PlayerCell                          emptyCell = null;
    
    private bool                                isCreating = true;

    public void GlobalInit()
    {
        GameEvents.OnPlayerDefeated += StopCreate;
        GameEvents.OnPlayerWon += StopCreate;
    }
    
    public void StartCreating() => StartCoroutine(TowerCreating());

    public Tower Create(Tower _prefab, PlayerCell _cell)
    {
        Tower _tower = Instantiate(_prefab ,_cell.SpawnPoint.position, _cell.SpawnPoint.rotation);
            
        _tower.Init(DestroyTower, projectilePool.Pool, projectilePool.PoolTransform);
            
        towers.Add(_tower);
            
        _cell.OccupiedCell(_tower);
        
        _tower.OccupiedCell(_cell);

        OnListChanged?.Invoke(towers);
        
        audioSource.PlayOneShot(towerSound);

        return _tower;
    }
    
    private IEnumerator TowerCreating()
    {
        float _start_Delay = 1;
        
        yield return new WaitForSeconds(_start_Delay);

        isCreating = true;
        
        while (isCreating)
        {
            emptyCell = GetEmptyCell();

            if (!emptyCell)
            {
                StartCreating();
                yield break;
            }

            if (!isCreating)
            {
                yield break;
            }
            
            Create(prefab , emptyCell);
            
            yield return new WaitForSeconds(spawnFrequency);

            yield return null;
        }
    }

    private PlayerCell GetEmptyCell()
    {
        for (int i = 0; i < playerCells.Count; i++)
        {
            if (playerCells[i].IsEmpty)
            {
                return playerCells[i];
            }
        }

        return null;
    }
    
    private void DestroyTower(Tower _tower)
    {
        towers.Remove(_tower);
        
        OnListChanged?.Invoke(towers);
        
        Destroy(_tower.gameObject);
    }
    
    private void StopCreate() => isCreating = false;

    private void OnDestroy()
    {
        GameEvents.OnPlayerDefeated -= StopCreate;
        GameEvents.OnPlayerWon -= StopCreate;
    }
    
}