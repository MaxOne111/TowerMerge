using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class DefeatWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton _Restart_Button;
    [SerializeField] private UIButton _Quit_Button;

    [SerializeField] private UIAnimator _Animator;

    public void PlayAnimation() => _Animator.Play();

    public UIButton GetRestartButton => _Restart_Button;
    public UIButton GetQuitButton => _Quit_Button;
}