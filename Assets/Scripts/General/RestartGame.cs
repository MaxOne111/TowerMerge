using System;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Runtime.UIManager;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private DefeatWindowUI _Defeat_Window;
    
    [SerializeField] private VictoryWindowUI _Victory_Window;
    
    private readonly string _Scene_Name = "Game";
    
    private UIButton _Restart;
    private UIButton _Continue;
    private UIButton _Defeat_Quit;
    private UIButton _Victory_Quit;
    
    private void Awake()
    {
        _Restart = _Defeat_Window.GetRestartButton;
        _Continue = _Victory_Window.GetContinueButton;
        
        _Defeat_Quit = _Defeat_Window.GetQuitButton;
        _Victory_Quit = _Victory_Window.GetQuitButton;
        
        _Restart.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Restart);
        _Continue.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Restart);
        
        _Defeat_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
        _Victory_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
    }

    private void Restart() => SceneManager.LoadScene(_Scene_Name);

    private void Quit() => Application.Quit();

    private void OnDestroy()
    {
        _Restart.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Restart);
        _Continue.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Restart);
        
        _Defeat_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
        _Victory_Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
    }
}