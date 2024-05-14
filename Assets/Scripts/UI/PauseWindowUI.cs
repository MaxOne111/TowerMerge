using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseWindowUI : MonoBehaviour
{
    [SerializeField] private UIButton     play = null;
    [SerializeField] private UIButton     quitButton = null;
    
    [SerializeField] private UIAnimator   animator = null;

    public void PlayAnimation(PlayDirection _direction) => animator.Play(_direction);

    public UIButton GetPlayButton => play;
    
    public UIButton GetQuitButton => quitButton;
}