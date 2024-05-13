using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();

        GameEvents._On_Player_Defeated += Idle;
    }
    
    private void Run() => _Animator.SetBool("IsRunning", true);
    private void Idle() => _Animator.SetBool("IsRunning", false);

    private void OnDestroy() => GameEvents._On_Player_Defeated -= Idle;
}