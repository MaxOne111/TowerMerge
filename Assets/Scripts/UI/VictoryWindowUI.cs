using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class VictoryWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton     continueButton = null;
    [SerializeField] private UIButton     quitButton = null;
    
    [SerializeField] private UIAnimator   animator = null;

    public void PlayAnimation() => animator.Play();
    
    public UIButton GetContinueButton => continueButton;
    
    public UIButton GetQuitButton => quitButton;
}