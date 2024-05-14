using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake() => GameEvents.OnPlayerDefeated += Idle;

    private void Run() => animator.SetBool("IsRunning", true);
    
    private void Idle() => animator.SetBool("IsRunning", false);

    private void OnDestroy() => GameEvents.OnPlayerDefeated -= Idle;
}