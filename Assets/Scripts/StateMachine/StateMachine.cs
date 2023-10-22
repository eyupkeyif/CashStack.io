using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;
    public AIController aiController;
    public Collect collect;
    public IdleState idleState = new IdleState();
    public CollectState collectState = new CollectState();
    public DeliverState deliverState = new DeliverState();
    public StealState stealState = new StealState();
    public Animator enemyAnimator;

    //private void Awake()
    //{
    //    currentState = collectState;

    //    currentState.EnterState(this);
    //}
    public void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);

        //Debug.Log(currentState.ToString());
    }

    public void UpdateState()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        if (currentState == state)
        {
            return;
        }

        currentState.ExitState(this);
        currentState = state;

        state.EnterState(this);
        //Debug.Log(currentState);
    }

    public void Init(AIController ai,Animator animator,Collect enemyCollect)
    {
        aiController = ai;
        enemyAnimator = animator;
        collect = enemyCollect;
    }
}
