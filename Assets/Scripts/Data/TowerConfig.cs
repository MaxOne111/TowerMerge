using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "TowerConfig", fileName = "TowerConfig")]
public class TowerConfig : ScriptableObject
{
    [SerializeField] private int     level = 1;
    [SerializeField] private Tower   nextTower = null;
    [SerializeField] private bool    isMaxLevel = false;

    public int Level => level;
    
    public Tower NextTower => nextTower;
    
    public bool IsMaxLevel => isMaxLevel;
}