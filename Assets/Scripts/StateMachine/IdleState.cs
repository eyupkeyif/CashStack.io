using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    bool isMoneyDetected;
    Vector3 randPosition;
    public Money money;
    public Rigidbody rb;
    float randomness;
    public Collect otherCollect;
    public override void EnterState(StateMachine sm)
    {
        //sm.enemyAnimator.GetParameter(0).defaultBool = true;
        sm.enemyAnimator.SetBool("isRunning", true);
        sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform));
        sm.aiController.StartCoroutine(CheckTheDistance(sm.aiController.gameObject.transform, sm));
        sm.aiController.StartCoroutine(CheckTheMoney(sm.aiController.transform, sm.collect));
        sm.aiController.StartCoroutine(CheckThePeople(sm.aiController.gameObject.transform, sm));

        //Debug.Log(SetNextPoint(sm.aiController.gameObject.transform.position));
        //Debug.Log(sm.currentState);
        Debug.Log("idle" + sm.aiController.agent.name+SetNextPoint(sm.aiController.gameObject.transform) + "" + sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform))) ;
        
         randomness = Random.Range(0, 10);

    }

    public override void UpdateState(StateMachine sm)
    {
        //CheckTheDistance(sm.aiController.gameObject.transform, sm);
        //CheckTheMoney(sm.aiController.gameObject.transform, sm.collect);
        //CheckThePeople(sm.aiController.gameObject.transform, sm);
        DeliverDecider(sm);
        //CheckTheDistance(sm.aiController.gameObject.transform,sm);
        if (isMoneyDetected && money.GetOwner()==null)
        {
            sm.SwitchState(sm.collectState);
        }
        //Debug.Log(SetNextPoint(sm.aiController.gameObject.transform));
    }

    public override void ExitState(StateMachine sm)
    {
        //sm.aiController.StopCoroutine(CheckTheDistance(sm.aiController.gameObject.transform, sm));
        //sm.aiController.StopCoroutine(CheckTheMoney(sm.aiController.transform, sm.collect));
        //sm.aiController.StopCoroutine(CheckThePeople(sm.aiController.gameObject.transform, sm));

        sm.aiController.StopAllCoroutines();
        sm.enemyAnimator.SetBool("isRunning", false);
        Debug.Log("exit from idle");
        isMoneyDetected = false;
        //sm.aiController.agent.ResetPath();
    }

    public Vector3 SetNextPoint(Transform enemy)
    {
        Vector2 randPoint = Random.insideUnitCircle.normalized * 100;
        randPosition = new Vector3(randPoint.x, enemy.position.y,randPoint.y);

        return randPosition;
    }

    public IEnumerator CheckTheDistance(Transform player,StateMachine sm)
    {
        //Debug.Log(sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform)));
        while (true)
        {
            if (Vector3.Distance(player.position, randPosition) < 0.1f)
            {
                Debug.Log("we^ve arrived");
                sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform));

            }
            if (sm.aiController.agent.hasPath == false)
            {
                sm.aiController.agent.autoRepath = true;
                //sm.aiController.agent.ResetPath();
                //sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform));
                sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform));


                Debug.Log("autorepath" + sm.aiController.agent.hasPath);

               
            }
            else
            {
                sm.SwitchState(sm.idleState);
            }
            yield return new WaitForSeconds(0.1f);

        }

        //if (sm.aiController.agent.pa)
        //{
        //    sm.aiController.agent.SetDestination(SetNextPoint(sm.aiController.gameObject.transform));

        //}

        //else if (Vector3.Distance(player.position, randPosition) > 0.5f)
        //{
        //    //Debug.Log("stucked");
        //}

    }

    public IEnumerator CheckTheMoney(Transform enemy,Collect collect)
    {

        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(enemy.position, 20f);
            //Debug.Log(enemy.position);
            foreach (var moneysInRange in colliders)
            {

                money = moneysInRange.GetComponent<Money>();

                //if (money)
                //{
                //    //Debug.Log(money.gameObject.name);
                //}
                if (money != null && money.GetOwner() == null)
                {
                    isMoneyDetected = true;
                    Debug.Log("Money detected! " + money.gameObject.name);

                    break;
                }
                else
                {

                    isMoneyDetected = false;
                    //Debug.Log("there is no Money " + isMoneyDetected);
                    break;
                }

            }
            yield return new WaitForSeconds(0.1f);



        }
           
    }

    public IEnumerator CheckThePeople(Transform enemy,StateMachine sm)
    {

        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(enemy.position, 10f);

            foreach (var agentInRange in colliders)
            {
                rb = agentInRange.GetComponent<Rigidbody>();

                if (rb != null && rb != sm.aiController.gameObject.GetComponent<Rigidbody>())
                {
                    otherCollect = rb.gameObject.GetComponent<Collect>();
                    //Debug.Log("people detected");
                    if (otherCollect.MoneysList().Count != 0 && sm.collect.MoneysList().Count < otherCollect.MoneysList().Count)
                    {
                        sm.SwitchState(sm.stealState);
                        break;

                    }
                    else
                    {
                        break;

                    }
                }
        
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DeliverDecider(StateMachine sm)
    {
        if (sm.collect.MoneysList().Count>5 && randomness>5)
        {
            sm.SwitchState(sm.deliverState);
        }
        else if (sm.collect.MoneysList().Count == sm.collect.capacity)
        {
            sm.SwitchState(sm.deliverState);
        }
       

    }

   


}
