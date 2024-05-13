using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private List<Tower> _Towers;
    [SerializeField] private List<Enemy> _Targets;
    
    public void GlobalInit()
    {
        EnemyDetection._On_Targets_Ready += GetTargets;
        TowerFactory._On_List_Changed += TowersUpdate;
    }

    private void GetTargets(List<Enemy> _targets)
    {
        _Targets = _targets;
        
        TowerTargets();
    }

    private void TowersUpdate(List<Tower> _towers)
    {
        _Towers = _towers;
        
        TowerTargets();
    }

    private void TowerTargets()
    {
        if (_Towers.Count == 0)
            return;

        if (_Targets.Count == 0)
        {
            for (int i = 0; i < _Towers.Count; i++)
                _Towers[i].Target = null;
            
            return;
        }
        
        if (_Towers.Count > _Targets.Count)
        {
            int i = _Towers.Count - 1;
            int j = 0;
            while (i >= 0)
            {
                _Towers[i].Target = _Targets[j].transform;
                i--;
                j++;

                if (j >= _Targets.Count - 1)
                    j = 0;
            }
            
            return;
        }

        for (int i = 0; i < _Towers.Count; i++)
            _Towers[i].Target = _Targets[i].transform;
    }
    
    
    private void OnDestroy()
    {
        EnemyDetection._On_Targets_Ready -= GetTargets;
        TowerFactory._On_List_Changed -= TowersUpdate;
    }
}