using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class PauseWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton _Play;
    [SerializeField] private UIButton _Quit_Button;
    
    [SerializeField] private UIAnimator _Animator;

    public void PlayAnimation(PlayDirection _direction) => _Animator.Play(_direction);

    public UIButton GetPlayButton => _Play;
    public UIButton GetQuitButton => _Quit_Button;
}