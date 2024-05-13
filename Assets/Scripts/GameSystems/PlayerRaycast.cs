using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private MergeSystem _Merge_System;

    [SerializeField] private LayerMask _Player_Field;
    [SerializeField] private LayerMask _Player_Cell;
    
    private Tower _Current_Tower;
    private PlayerCell _Last_Cell;
    
    private Ray _Ray;
    private RaycastHit _Hit;
    
    private Camera _Camera;

    public void GlobalInit()
    {
        _Camera = Camera.main;

        GameEvents._On_Player_Defeated += DisableRaycast;
        GameEvents._On_Player_Won += DisableRaycast;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SelectTower();

        if (Input.GetMouseButton(0))
            MoveTower();

        if (Input.GetMouseButtonUp(0))
            PutTower();
    }
    
    private void SelectTower()
    {
        _Ray = _Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_Ray, out _Hit, 100))
        {
            if (_Hit.collider.TryGetComponent(out Tower _tower))
            {
                _Current_Tower = _tower;
                _Current_Tower.Deactivate();
                _Last_Cell = _tower.Cell;
            }
                
        }
    }
    
    private void MoveTower()
    {
        _Ray = _Camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(_Ray, out _Hit, 100, _Player_Field))
        {
            if (!_Current_Tower)
                return;
                
            _Current_Tower.transform.position = new Vector3(_Hit.point.x, _Current_Tower.transform.position.y, _Hit.point.z);
        }

    }

    private void PutTower()
    {
        if (!_Current_Tower)
            return;
        
        _Ray = _Camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(_Ray, out _Hit, 100, _Player_Cell))
        {
            if (_Hit.transform.TryGetComponent(out PlayerCell _cell))
            {
                if (_cell.IsEmpty)
                {
                    _Current_Tower.transform.position = _cell.SpawnPoint.position;
                    OccupiedCell(_Current_Tower ,_cell);
                }
                else
                {
                    if (_Current_Tower.Level == _cell.CurrentTower.Level && _Current_Tower != _cell.CurrentTower && !_Current_Tower.IsMaxLevel)
                    {
                        Tower _new_Tower = _Merge_System.Merge(_Current_Tower, _cell.CurrentTower, _Current_Tower.NextTower, _cell);
                        
                        OccupiedCell(_new_Tower ,_cell);
                    }
                    else
                        _Current_Tower.transform.position = _Last_Cell.SpawnPoint.position;
                }
                    
            }
        }
        else
            _Current_Tower.transform.position = _Last_Cell.SpawnPoint.position;

        if (_Current_Tower)
            _Current_Tower.Activate();
        
        _Current_Tower = null;
        _Last_Cell = null;
    }
    
    private void DisableRaycast() => gameObject.SetActive(false);

    private void OccupiedCell(Tower _tower ,PlayerCell _cell)
    {
        _cell.OccupiedCell(_tower);
        _tower.OccupiedCell(_cell);
        _Last_Cell.CleanCell();
    }
    
    private void OnDestroy()
    {
        GameEvents._On_Player_Defeated -= DisableRaycast;
        GameEvents._On_Player_Won -= DisableRaycast;
    }
    
}