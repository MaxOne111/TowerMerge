using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    [SerializeField] private TowerFactory towerFactory = null;
    
    public Tower Merge(Tower _tower_1, Tower _tower_2 ,Tower _next_Tower_Prefab, PlayerCell _cell)
    {
        if (!_next_Tower_Prefab)
        {
            return null;
        }
        
        _tower_1.DestroyTower();
        _tower_2.DestroyTower();

        Tower _new_Tower = towerFactory.Create(_next_Tower_Prefab , _cell);

        return _new_Tower;
    }
}