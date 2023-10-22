using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    //[HideInInspector]
    //public IdleState idleState = new IdleState();
    //[HideInInspector]
    //public Moving movingState= new Moving();
    //private AIController aI;

    //private void Awake()
    //{
    //    currentState = idleState;
    //    currentState.EnterState(this);
    //}
    //public override void Start()
    //{
    //    currentState = idleState;
    //    currentState.EnterState(this);
    //}

    //public override void Update()
    //{
    //    currentState.UpdateState(this);
    //}

    //public override void SwitchState(BaseState state)
    //{
    //    if (currentState==state || currentState==null)
    //    {
    //        return;
    //    }

    //    currentState.ExitState(this);
    //    currentState = state;

    //    state.EnterState(this);
    //}

    //public void Setup(AIController aiController)
    //{
    //    aI = aiController;
    //}
}
