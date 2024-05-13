using UnityEngine;

[CreateAssetMenu(menuName = "TowerConfig", fileName = "TowerConfig")]
public class TowerConfig : ScriptableObject
{
    [SerializeField] private int _Level;
    [SerializeField] private Tower _Next_Tower;
    [SerializeField] private bool _Is_Max_Level;

    public int Level => _Level;
    public Tower NextTower => _Next_Tower;
    public bool IsMaxLevel => _Is_Max_Level;
}