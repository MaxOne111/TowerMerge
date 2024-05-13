using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static PlayerData _Player_Data;
    
    public void GlobalInit()
    {
        _Player_Data = new PlayerData();
        
        Load();

        GameEvents._On_Player_Won += LevelUp;
    }

    private void Load() => _Player_Data.LoadData();

    private void Save() => _Player_Data.SaveData();
    
    private void LevelUp()
    {
        _Player_Data._Level++;
        Save();
    }

    private void OnDestroy() => GameEvents._On_Player_Won -= LevelUp;
}