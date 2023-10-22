using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] List<Money> moneys;
    [SerializeField] Transform deliveryPoint;
    [SerializeField] float speed;
    bool canDeliver = false;
    [SerializeField] DeliverParticle deliverObjParticle;
    private void Start()
    {
        //StartCoroutine(DelayDelivery(3));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
           Collect collect = other.gameObject.GetComponent<Collect>();
            //collect.totalMoney+= collect.MoneysList().Count;
            Debug.Log(collect.SetTotalMoney());
            //if (collect.MoneysList().Count != 0)
            //{
            //    int listCount = collect.MoneysList().Count;
            //    for (int i = 0; i < listCount; i++)
            //    {

            //        //Vector3 offset = new Vector3(0, 5, 0);
            //        //collect.MoneysList()[0].transform.position = deliveryPoint.position + offset;
            //        //collect.StopFollow(collect.MoneysList()[i],deliveryPoint);
            //        moneys.Add(collect.MoneysList()[0]);
            //        collect.MoneysList().Remove(collect.MoneysList()[0]);
            //        Debug.Log(collect.GetMoneyCount());
            //        //Vector3 smoothPosition = Vector3.Lerp(moneys[i].transform.position, deliveryPoint.position, 0.125f);


            //}

            StartCoroutine(AddMoneytotheDelivery(collect,1));

            //moneys[0].transform.position = Vector3.MoveTowards(transform.position, deliveryPoint.position, 5 * Time.fixedDeltaTime);

            //}


        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Collect collect = other.gameObject.GetComponent<Collect>();
            //collect.totalMoney+= collect.MoneysList().Count;
            Debug.Log(collect.SetTotalMoney());

            if (collect.MoneysList().Count!=0)
            {
                for (int i = 0; i < collect.MoneysList().Count; i++)
                {
                    collect.MoneysList()[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
            
        }
    }



    public void FixedUpdate()
    {
        

    }



    IEnumerator AddMoneytotheDelivery(Collect collect, float timer)
    {
      
            if (collect.MoneysList() != null && collect.MoneysList().Count != 0)
            {
           
                int listCount = collect.MoneysList().Count;
            int totalMoneyCount = collect.moneyCount;

                for (int i = 0; i < listCount; i++)
                {
                collect.MoneysList()[0].CoroutineStarter(timer, deliveryPoint);

                //moneys.Add(collect.MoneysList()[0]);
                LevelManager.Instance.totalMoneyList.Remove(collect.MoneysList()[0].gameObject);
                collect.MoneysList().Remove(collect.MoneysList()[0]);

                //moneys[0].gameObject.GetComponent<BoxCollider>().enabled = false;
                if (totalMoneyCount==collect.capacity)
                {
                    collect.totalMoney += 2;
                }
                else if (totalMoneyCount==collect.capacity/2f)
                {
                   //collect.totalMoney += 1.5f;
                }
                else
                {
                    collect.totalMoney++;
                }
    
              
                collect.onMoneyDeliver?.Invoke();

              
                Debug.Log("delivered moneY: " + collect.totalMoney);
                //if (Vector3.Distance(moneys[0].gameObject.transform.position,deliveryPoint.position)<10)
                //{
                //    Debug.Log("deliver particle");
                //    deliverObjParticle.StartParticle();

                //}
                moneys.Clear();
                collect.moneyCount--;
                collect.onMoneyCollect?.Invoke();

                    //Debug.Log("yeaay");
                    yield return new WaitForSeconds(0.2f);

                    //Debug.Log(collect.GetMoneyCount());
                }
         
              


        }
        //Debug.Log("how many trigger");




            
    }
            
   
}
