using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    const string IdleState = "Idle";
    const string WalkState = "Walk";
    const string RunState = "Run";
    const string FireState = "Fire";
    const string ReloadState = "Reload";

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
        _animator.CrossFadeInFixedTime(FireState, 0f);
    }

    public void OnStartReload()
    {
        _animator.CrossFadeInFixedTime(ReloadState, 0f);
    }

    private void OnEnable()
    {
        gunManager.OnIdle += OnStartIdle;
        gunManager.OnFire += OnStartFiring;
        gunManager.OnReload += OnStartReload;
        movement.OnIdle += OnStartIdle;
        movement.OnWalk += OnStartWalk;
        movement.OnRun += OnStartRun;
    }

    private void OnDisable()
    {
        gunManager.OnIdle -= OnStartIdle;
        gunManager.OnFire -= OnStartFiring;
        gunManager.OnReload -= OnStartReload;
        movement.OnIdle -= OnStartIdle;
        movement.OnWalk -= OnStartWalk;
        movement.OnRun -= OnStartRun;
    }

}
