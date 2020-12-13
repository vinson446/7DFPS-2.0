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

    [SerializeField] GunManager gunManager = null;
    [SerializeField] Movement_CC movement = null;


    Animator _animator = null;

    private void Awake()
    {
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
