using System;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Runtime.UIManager;
using UnityEngine.Serialization;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private DefeatWindowUI    defeatWindow = null;
    
    [SerializeField] private VictoryWindowUI   victoryWindow = null;
    
    private readonly string                    sceneName = "Game";
    
    private UIButton                           restart = null;
    private UIButton                           _continue = null;
    private UIButton                           defeat_Quit = null;
    private UIButton                           victory_Quit = null;
    
    private void Awake()
    {
        restart = defeatWindow.GetRestartButton;
        _continue = victoryWindow.GetContinueButton;
        
        defeat_Quit = defeatWindow.GetQuitButton;
        victory_Quit = victoryWindow.GetQuitButton;
        
        restart.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Restart);
        _continue.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Restart);
        
        defeat_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
        victory_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
    }

    private void Restart() => SceneManager.LoadScene(sceneName);

    private void Quit() => Application.Quit();

    private void OnDestroy()
    {
        restart.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Restart);
        _continue.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Restart);
        
        defeat_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
        victory_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
    }
}