using System;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DefeatWindowUI _Defeat_Window;
    [SerializeField] private VictoryWindowUI _Victory_Window;
    [SerializeField] private PauseWindowUI _Pause_Window;
    
    [SerializeField] private UIButton _Pause_Button;

    [SerializeField] private TextMeshProUGUI _Current_Level;
    public UIButton GetPauseButton => _Pause_Button;
    
    private void Awake()
    {
        GameEvents._On_Player_Defeated += ShowDefeatWindow;
        GameEvents._On_Player_Won += ShowVictoryWindow;
    }

    private void Start() => ShowCurrentLevel();

    private void ShowDefeatWindow()
    {
        _Defeat_Window.gameObject.SetActive(true);
        _Defeat_Window.PlayAnimation();
    }

    private void ShowVictoryWindow()
    {
        _Victory_Window.gameObject.SetActive(true);
        _Victory_Window.PlayAnimation();
    }

    public void ShowPauseWindow()
    {
        _Pause_Window.gameObject.SetActive(true);
        _Pause_Window.PlayAnimation(PlayDirection.Forward);
    }

    private void ShowCurrentLevel() => _Current_Level.text = $"Level: {SaveLoad._Player_Data._Level}";

    public void HidePauseWindow() => _Pause_Window.PlayAnimation(PlayDirection.Reverse);

    private void OnDestroy()
    {
        GameEvents._On_Player_Defeated -= ShowDefeatWindow;
        GameEvents._On_Player_Won -= ShowVictoryWindow;
    }
}