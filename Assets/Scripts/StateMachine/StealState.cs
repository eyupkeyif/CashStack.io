using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealState : BaseState
{
    int randomMoney;
    float randomness;
    Vector3 targetPoint;
    public override void EnterState(StateMachine sm)
    {
        sm.enemyAnimator.SetBool("isRunning", true);
        Debug.Log("steal");
        randomness = Random.Range(0, 10);
        //Debug.Log(randomness);
        randomMoney= Random.Range(0, sm.idleState.otherCollect.MoneysList().Count-1);


        if (randomness<4)
        {
            if (sm.idleState.otherCollect.MoneysList().Count!=0 && sm.idleState.otherCollect.MoneysList()[randomMoney]!=null)
            {

                Vector3 moneyPoint = sm.idleState.otherCollect.MoneysList()[randomMoney].transform.position;
                targetPoint = new Vector3(moneyPoint.x, sm.aiController.gameObject.transform.position.y, moneyPoint.z);

                sm.aiController.StartCoroutine(CatchHimIfYouCan(sm));

            }
          

        }
        else
        {
            sm.SwitchState(sm.idleState);
        }
    }

    public override void UpdateState(StateMachine sm)
    {
        //sm.idleState.otherCollect.MoneysList()[randomMoney] ||

        if (CheckTheDistance(sm) > 50||  sm.aiController.agent.hasPath==false || sm.idleState.otherCollect==null)
        {
            Debug.Log("enemy ran away" + CheckTheDistance(sm) + "owner: " + sm.idleState.otherCollect + " hasPAth: " + sm.aiController.agent.hasPath);
            sm.SwitchState(sm.idleState);
        }
        //else
        //{
        //    return;
        //}

      
    }

    public override void ExitState(StateMachine sm)
    {
        sm.enemyAnimator.SetBool("isRunning", false);
        sm.aiController.StopAllCoroutines();
        Debug.Log("exit steal");

    }

    public IEnumerator CatchHimIfYouCan(StateMachine sm)
    {


        while (Vector3.Distance(sm.aiController.gameObject.transform.position, targetPoint) < 1f)
        {
        
                        sm.aiController.agent.SetDestination(targetPoint);
            //if (Vector3.Distance(sm.aiController.gameObject.transform.position,targetPoint)>20)
            //{
            //    sm.SwitchState(sm.idleState);
            //}
            yield return new WaitForFixedUpdate();

        }

     

    }

    public float CheckTheDistance(StateMachine sm)
    {
       float distance= Vector3.Distance(sm.aiController.gameObject.transform.position, sm.idleState.otherCollect.gameObject.transform.position);
        return distance;
    }
}
