using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : BaseState
{
    bool isMoneyCollected;
    public override void EnterState(StateMachine sm)
    {
        //Debug.Log(sm.currentState + " " + sm.idleState.money.gameObject.transform.position) ;
        sm.enemyAnimator.SetBool("isRunning", true);
        Debug.Log("collect");
        sm.aiController.StartCoroutine(CollectMoney(sm));
        sm.aiController.agent.SetDestination(sm.idleState.money.gameObject.transform.position);


    }

    public override void UpdateState(StateMachine sm)
    {
        if (isMoneyCollected || sm.idleState.money.GetOwner()!=null || sm.aiController.agent.hasPath==false)
        {
            sm.SwitchState(sm.idleState);
        }
       
        
        
        //else if (Vector3.Distance(sm.aiController.gameObject.transform.position, sm.idleState.money.gameObject.transform.position) > 25f)
        //{
        //    sm.SwitchState(sm.idleState);
        //}


    }

    public override void ExitState(StateMachine sm)
    {
        sm.enemyAnimator.SetBool("isRunning", false);
        //sm.aiController.StopCoroutine(CollectMoney(sm));
        sm.aiController.StopAllCoroutines();
        isMoneyCollected = false;
        Debug.Log("exit collect");
        //sm.aiController.agent.ResetPath();
    }

    public IEnumerator CollectMoney(StateMachine sm)
    {
        while (true)
        {
            if (sm.idleState.money == sm.collect.MoneysList().Contains(sm.idleState.money))
            {
                Debug.Log("2");
                isMoneyCollected = true;
                Debug.Log(isMoneyCollected);
            }


            yield return new WaitForSeconds(0.1f);
        }
        
        //while (true)
        //{
        //    if (true)
        //    {
        //        sm.aiController.agent.SetDestination(sm.idleState.money.gameObject.transform.position);

        //    }

        //    yield return new WaitForFixedUpdate();

        //}


    }
}
