using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class DefeatWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton     restartButton = null;
    [SerializeField] private UIButton     quitButton = null;

    [SerializeField] private UIAnimator   animator = null;

    public void PlayAnimation() => animator.Play();

    public UIButton GetRestartButton => restartButton;
    
    public UIButton GetQuitButton => quitButton;
}