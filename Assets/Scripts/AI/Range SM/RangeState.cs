using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : State
{
    protected RangeSM stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = GetComponent<RangeSM>();
    }
}
