using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameUI          gameUI = null;
    
    [SerializeField] private PauseWindowUI   pauseWindow = null;

    private UIButton                         pause = null;
    private UIButton                         play = null;
    private UIButton                         quit = null;

    private void Awake()
    {
        Continue();
        
        pause = gameUI.GetPauseButton;
        play = pauseWindow.GetPlayButton;
        quit = pauseWindow.GetQuitButton;

        pause.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Pause);
        play.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Continue);
        quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(Quit);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        
        gameUI.ShowPauseWindow();
    }

    private void Continue()
    {
        Time.timeScale = 1;
        
        gameUI.HidePauseWindow();
    }

    private void Quit() => Application.Quit();

    private void OnDestroy()
    {
        pause.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Pause);
        play.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Continue);
        quit.behaviours.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveListener(Quit);
    }
}