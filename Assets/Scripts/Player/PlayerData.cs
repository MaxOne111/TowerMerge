using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlayerData
{
    public int level = 1;

    public void SaveData() => PlayerPrefs.SetInt("Level", level);

    public void LoadData() => level = PlayerPrefs.GetInt("Level", 1);
}