using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverState : BaseState
{
    Vector3 deliverPoint;
    public override void EnterState(StateMachine sm)
    {
        sm.enemyAnimator.SetBool("isRunning", true);
        //Vector2 randomPoint = Random.insideUnitCircle.normalized * 27f;
        //deliverPoint = randomPoint;
        //SetNextPoint(sm.aiController.gameObject.transform);
        
            sm.aiController.agent.SetDestination(SetNextPoint());
        sm.aiController.StartCoroutine(DeliverMoney(sm));

        
        //else
        //{
        //    sm.SwitchState(sm.idleState);
        //}
        //sm.aiController.StartCoroutine(DeliverMoney(sm));
        //sm.aiController.StartCoroutine(DeliverMoney(sm));
        Debug.Log("delver" + deliverPoint) ;
    }

    public override void UpdateState(StateMachine sm)
    {
        //if (Vector3.Distance(sm.aiController.gameObject.transform.position,deliverPoint)<25f)
        //{
        //    sm.SwitchState(sm.idleState);
        //}
        sm.collect.MoneysList();
        if (sm.aiController.agent.hasPath==false)
        {
            Debug.Log("has path" + sm.aiController.agent.hasPath + "Path pending: " + sm.aiController.agent.pathPending + "Path: " + sm.aiController.agent.pathStatus);
            //sm.aiController.agent.ResetPath();
            //SetNextPoint(sm.aiController.gameObject.transform);
            sm.aiController.agent.SetDestination(SetNextPoint());


            Debug.Log(sm.aiController.agent.SetDestination(SetNextPoint()));
        }
        else
        {

        }
    }

    public override void ExitState(StateMachine sm)
    {
        sm.enemyAnimator.SetBool("isRunning", false);
        sm.aiController.StopAllCoroutines();
        Debug.Log("exit deliver");

    }

    public Vector3 SetNextPoint()
    {
        Vector2 randPoint = Random.insideUnitCircle.normalized * 27;
        deliverPoint = randPoint;

        return deliverPoint;
    }

    public IEnumerator DeliverMoney(StateMachine sm)
    {
        while (true)
        {
            if (sm.collect.MoneysList().Count==0)
            {

                Debug.Log("we must exit from deliver state");
                sm.SwitchState(sm.idleState);
            }
            yield return new WaitForFixedUpdate();

        }
    }
}
