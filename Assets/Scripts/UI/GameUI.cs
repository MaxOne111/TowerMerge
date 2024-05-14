using System;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DefeatWindowUI    defeatWindow = null;
    
    [SerializeField] private VictoryWindowUI   victoryWindow = null;
    
    [SerializeField] private PauseWindowUI     pauseWindow = null;
    
    [SerializeField] private UIButton          pauseButton = null;

    [SerializeField] private TextMeshProUGUI   currentLevel = null;
    
    public UIButton GetPauseButton => pauseButton;
    
    private void Awake()
    {
        GameEvents.OnPlayerDefeated += ShowDefeatWindow;
        GameEvents.OnPlayerWon += ShowVictoryWindow;
    }

    private void Start() => ShowCurrentLevel();

    private void ShowDefeatWindow()
    {
        defeatWindow.gameObject.SetActive(true);
        defeatWindow.PlayAnimation();
    }

    private void ShowVictoryWindow()
    {
        victoryWindow.gameObject.SetActive(true);
        victoryWindow.PlayAnimation();
    }

    public void ShowPauseWindow()
    {
        pauseWindow.gameObject.SetActive(true);
        pauseWindow.PlayAnimation(PlayDirection.Forward);
    }

    private void ShowCurrentLevel() => currentLevel.text = $"Level: {SaveLoad.playerData.level}";

    public void HidePauseWindow() => pauseWindow.PlayAnimation(PlayDirection.Reverse);

    private void OnDestroy()
    {
        GameEvents.OnPlayerDefeated -= ShowDefeatWindow;
        GameEvents.OnPlayerWon -= ShowVictoryWindow;
    }
}