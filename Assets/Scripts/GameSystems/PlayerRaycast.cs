using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRaycast : MonoBehaviour
{
   [SerializeField] private MergeSystem   mergeSystem = null;

    [SerializeField] private LayerMask    playerFieldLayer = 6;
    [SerializeField] private LayerMask    playerCellLayer = 7;
    
    private Tower        currentTower = null;
    
    private PlayerCell   lastCell = null;
    
    private Ray          ray;
    
    private RaycastHit   hit;
    
    private Camera       _camera = null;

    public void GlobalInit()
    {
        _camera = Camera.main;

        GameEvents.OnPlayerDefeated += DisableRaycast;
        GameEvents.OnPlayerWon += DisableRaycast;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTower();
        }
        
        if (Input.GetMouseButton(0))
        {
            MoveTower();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            PutTower();
        }
    }
    
    private void SelectTower()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.TryGetComponent(out Tower _tower))
            {
                currentTower = _tower;
                currentTower.Deactivate();
                
                lastCell = _tower.Cell;
            }
        }
    }
    
    private void MoveTower()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100, playerFieldLayer))
        {
            if (!currentTower)
            {
                return;
            }
            
            currentTower.transform.position = new Vector3(hit.point.x, currentTower.transform.position.y, hit.point.z);
        }
    }

    private void PutTower()
    {
        if (!currentTower)
        {
            return;
        }
        
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100, playerCellLayer))
        {
            if (hit.transform.TryGetComponent(out PlayerCell _cell))
            {
                if (_cell.IsEmpty)
                {
                    currentTower.transform.position = _cell.SpawnPoint.position;
                    
                    OccupiedCell(currentTower ,_cell);
                }
                else
                {
                    if (currentTower.Level == _cell.CurrentTower.Level && currentTower != _cell.CurrentTower && !currentTower.IsMaxLevel)
                    {
                        Tower _new_Tower = mergeSystem.Merge(currentTower, _cell.CurrentTower, currentTower.NextTower, _cell);
                        
                        OccupiedCell(_new_Tower ,_cell);
                    }
                    else
                    {
                        currentTower.transform.position = lastCell.SpawnPoint.position;
                    }
                }
            }
        }
        else
        {
            currentTower.transform.position = lastCell.SpawnPoint.position;
        }
        
        if (currentTower)
        {
            currentTower.Activate();
        }
        
        currentTower = null;
        
        lastCell = null;
    }
    
    private void DisableRaycast() => gameObject.SetActive(false);

    private void OccupiedCell(Tower _tower ,PlayerCell _cell)
    {
        _cell.OccupiedCell(_tower);
        
        _tower.OccupiedCell(_cell);
        
        lastCell.CleanCell();
    }
    
    private void OnDestroy()
    {
        GameEvents.OnPlayerDefeated -= DisableRaycast;
        GameEvents.OnPlayerWon -= DisableRaycast;
    }
    
}