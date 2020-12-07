using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : State
{
    protected MeleeSM stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = GetComponent<MeleeSM>();
    }
}
