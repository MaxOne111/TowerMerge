using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCell : MonoBehaviour
{
    [SerializeField] private bool        isEmpty = true;
    
    [SerializeField] private Transform   spawnPoint = null;

    private Tower                        currentTower = null;
    
    public bool IsEmpty => isEmpty;
    
    public Transform SpawnPoint => spawnPoint;
    
    public Tower CurrentTower => currentTower;

    public void CleanCell()
    {
        isEmpty = true;
        currentTower = null;
    }

    public void OccupiedCell(Tower _tower)
    {
        isEmpty = false;
        currentTower = _tower;
    }
}