using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{


    public abstract void EnterState(StateMachine sm);
    public abstract void UpdateState(StateMachine sm);
    public abstract void ExitState(StateMachine sm); 
}
