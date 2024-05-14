using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetingSystem : MonoBehaviour
{
    private List<Tower>   towers = new List<Tower>();
    private List<Enemy>   targets = new List<Enemy>();
    
    public void GlobalInit()
    {
        EnemyDetection.OnTargetsReady += GetTargets;
        TowerFactory.OnListChanged += TowersUpdate;
    }

    private void GetTargets(List<Enemy> _targets)
    {
        targets = _targets;
        
        TowerTargets();
    }

    private void TowersUpdate(List<Tower> _towers)
    {
        towers = _towers;
        
        TowerTargets();
    }

    private void TowerTargets()
    {
        if (towers.Count == 0)
        {
            return;
        }
        
        if (targets.Count == 0)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Target = null;
            }
            
            return;
        }
        
        if (towers.Count > targets.Count)
        {
            int i = towers.Count - 1;
            int j = 0;
            while (i >= 0)
            {
                towers[i].Target = targets[j].transform;
                i--;
                j++;

                if (j >= targets.Count - 1)
                {
                    j = 0;
                }
            }
            
            return;
        }

        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].Target = targets[i].transform;
        }
    }
    
    private void OnDestroy()
    {
        EnemyDetection.OnTargetsReady -= GetTargets;
        TowerFactory.OnListChanged -= TowersUpdate;
    }
}