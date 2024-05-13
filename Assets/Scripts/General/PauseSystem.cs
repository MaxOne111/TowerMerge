using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameUI _Game_UI;
    
    [SerializeField] private PauseWindowUI _Pause_Window;

    private UIButton _Pause;
    private UIButton _Play;
    private UIButton _Quit;

    private void Awake()
    {
        Continue();
        
        _Pause = _Game_UI.GetPauseButton;
        _Play = _Pause_Window.GetPlayButton;
        _Quit = _Pause_Window.GetQuitButton;

        _Pause.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Pause);
        _Play.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Continue);
        _Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _Game_UI.ShowPauseWindow();
    }

    private void Continue()
    {
        Time.timeScale = 1;
        _Game_UI.HidePauseWindow();
    }

    private void Quit() => Application.Quit();

    private void OnDestroy()
    {
        _Pause.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Pause);
        _Play.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Continue);
        _Quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
    }
}