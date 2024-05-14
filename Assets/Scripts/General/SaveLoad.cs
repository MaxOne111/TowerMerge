using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static PlayerData playerData = null;
    
    public void GlobalInit()
    {
        playerData = new PlayerData();
        
        Load();

        GameEvents.OnPlayerWon += LevelUp;
    }

    private void Load() => playerData.LoadData();

    private void Save() => playerData.SaveData();
    
    private void LevelUp()
    {
        playerData.level++;
        
        Save();
    }

    private void OnDestroy() => GameEvents.OnPlayerWon -= LevelUp;
}