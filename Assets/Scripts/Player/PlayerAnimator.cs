using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    const string IdleState = "Idle_Rifle";
    const string WalkState = "Walk_Rifle";
    const string RunState = "Run_Rifle";
    const string FireState = "Fire_Rifle";

    GunManager gunManager;
    Movement_CC movement;

    Animator _animator = null;

    private void Awake()
    {
        gunManager = GetComponent<GunManager>();
        movement = GetComponent<Movement_CC>();
        _animator = GetComponent<Animator>();
    }

    public void OnStartIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    public void OnStartWalk()
    {
        _animator.CrossFadeInFixedTime(WalkState, .2f);
    }

    public void OnStartRun()
    {
        _animator.CrossFadeInFixedTime(RunState, .2f);
    }

    public void OnStartFiring()
    {
        _animator.CrossFadeInFixedTime(FireState, .2f);
    }

    private void OnEnable()
    {
        gunManager.OnIdle += OnStartIdle;
        gunManager.OnFire += OnStartFiring;
        movement.OnIdle += OnStartIdle;
        movement.OnWalk += OnStartWalk;
        movement.OnRun += OnStartRun;
    }

    private void OnDisable()
    {
        gunManager.OnIdle -= OnStartIdle;
        gunManager.OnFire -= OnStartFiring;
        movement.OnIdle -= OnStartIdle;
        movement.OnWalk -= OnStartWalk;
        movement.OnRun -= OnStartRun;
    }

}
