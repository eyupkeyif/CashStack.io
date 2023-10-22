using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] float radius;
    private CollectSM collectSM;
    private MovementSM movementSM;
    private Collect collect;
    AIController aIController;
    private List<Collider> moneyListInZone = new();
    Transform closestMoney;
    [SerializeField] LayerMask moneyLayer;
    private void Awake()
    {
        //SetupComponents();

    }

    //private void Start()
    //{
    //    StartCoroutine(CheckMoneyCoroutine());
    //    Debug.Log(movementSM.currentState);
    //}

    //public void SetupComponents()
    //{
    //    collect = GetComponent<Collect>();
    //    collectSM = gameObject.AddComponent<CollectSM>();
    //    movementSM = GetComponent<MovementSM>();
    //    aIController = GetComponent<AIController>();
    //}

    //private void Update()
    //{
    //}

    //private IEnumerator CheckMoneyCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        CheckTheMoney();

    //    }
      
    //}
    //public void CheckTheMoney()
    //{
    //    //int maxColliders = 10;

    //    Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

    //    foreach (var moneysInRange in colliders)
    //    {

    //        Money money = moneysInRange.GetComponent<Money>();

    //        Debug.Log(movementSM.currentState);
    //        if (money!=null && movementSM.currentState == movementSM.idleState)
    //        {
    //            movementSM.SwitchState(movementSM.movingState);
    //            aIController.GetMoneyPoint(moneysInRange.transform.position);
    //            break;
    //        }
    //    }
    //}

    ////private void MoneyCollector(int numOfMoneysInRange, Collider[] hitCollidersArr)
    ////{

    ////    if (numOfMoneysInRange>0)
    ////    {
    ////        for (int i = 0; i < numOfMoneysInRange; i++)
    ////        {
    ////            moneyListInZone.Add(hitCollidersArr[i]);
    ////        }

    ////        //DetectClosestMoney();

    ////        if (closestMoney!=null && movementSM.currentState == movementSM.idleState)
    ////        {
    ////            Debug.Log(closestMoney.position);
    ////            movementSM.SwitchState(movementSM.movingState);
    ////            aIController.targetPoint = closestMoney.position;
    ////        }
    ////    }
    ////}

    

    //public void DeliverMoney()
    //{

    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawWireSphere(transform.position, 15);

    //}
    ////private void DetectClosestMoney()
    ////{
    ////    float refDistance = Mathf.Infinity;

    ////    Transform closest = null;

    ////    foreach (Collider money in moneyListInZone)
    ////    {
    ////        if (money==null)
    ////        {
    ////            return;

    ////        }

    ////        Vector3 editedEnemyPos = new Vector3(transform.position.x, 0, transform.position.z);
    ////        Vector3 editedMoneyPos = new Vector3(money.transform.position.x, 0, money.transform.position.z);

    ////        float currentDistance = Vector3.Distance(editedEnemyPos, editedMoneyPos);

    ////        if (currentDistance<=refDistance)
    ////        {
    ////            refDistance = currentDistance;

    ////            closest = money.transform;
    ////        }
    ////    }

    ////    closestMoney = closest;
    //}
}
