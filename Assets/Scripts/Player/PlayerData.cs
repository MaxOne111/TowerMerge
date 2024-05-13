using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int _Level;

    public void SaveData() => PlayerPrefs.SetInt("Level", _Level);

    public void LoadData() => _Level = PlayerPrefs.GetInt("Level", 1);
}