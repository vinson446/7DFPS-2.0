using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSM : StateMachine
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeState<RangeCheckState>();
    }
}
