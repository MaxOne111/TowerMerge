using System;
using UnityEngine;

public class PlayerCell : MonoBehaviour
{
    [SerializeField] private bool _Is_Empty = true;
    [SerializeField] private Transform _Spawn_Point;

    [SerializeField] private Tower _Current_Tower;
    
    public bool IsEmpty => _Is_Empty;
    public Transform SpawnPoint => _Spawn_Point;
    public Tower CurrentTower => _Current_Tower;

    public void CleanCell()
    {
        _Is_Empty = true;
        _Current_Tower = null;
    }

    public void OccupiedCell(Tower _tower)
    {
        _Is_Empty = false;
        _Current_Tower = _tower;
    }
}