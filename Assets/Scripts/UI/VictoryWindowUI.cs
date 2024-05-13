using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class VictoryWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton _Continue_Button;
    [SerializeField] private UIButton _Quit_Button;
    
    [SerializeField] private UIAnimator _Animator;

    public void PlayAnimation() => _Animator.Play();
    
    public UIButton GetContinueButton => _Continue_Button;
    public UIButton GetQuitButton => _Quit_Button;
}