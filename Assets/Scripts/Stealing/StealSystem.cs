using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealSystem : MonoBehaviour
{
    [SerializeField] Collect collect;

    private void Start()
    {
        
    }
    public void StealMoney(Money money,Collect otherCollect,Collect enemyCollect)
    {
        int listMoney = otherCollect.MoneysList().Count;
        int index = otherCollect.MoneysList().IndexOf(money);
            if (money)
            {
         
                    if (money.canSteal)
                    {
                        if (money.GetOwner() == otherCollect)
                        {
                            for (int j = index; j < listMoney; j++)
                            {

                                //money.CoroutineStarter();
                            if (index>=0)
                            {
                                otherCollect.MoneysList()[index].StealParticle();
                                //StartCoroutine(AfterStealInvisibility(otherCollect.MoneysList()[index]));
                            
                            //Vector2 randPoint = Random.insideUnitCircle.normalized * 5;
                            //Vector3 randomPosition = enemyCollect.gameObject.transform.forward + new Vector3(randPoint.x, 0, randPoint.y);
                            otherCollect.MoneysList()[index].DistrcctMoneyStarter(.5f, enemyCollect.gameObject.transform);
                            //Debug.Log("transform.forward: " + enemyCollect.gameObject.transform.forward);
                            otherCollect.MoneysList()[index].SetOwner(null);
                            otherCollect.MoneysList().Remove(otherCollect.MoneysList()[index]);
                           
                                otherCollect.moneyCount = otherCollect.MoneysList().Count;
                            otherCollect.onMoneyCollect?.Invoke();
                            }



                        }

                        }

                    }
                    //money.isStolen=true;
                
            }
        
    }
    public IEnumerator AfterStealInvisibility(Money money)
    {
        if (money)
        {
            money.GetComponent<BoxCollider>().enabled = false;

            //Debug.Log(money.GetComponent<BoxCollider>().enabled);
            yield return new WaitForSeconds(2);

            money.GetComponent<BoxCollider>().enabled = true;
            //Debug.Log(money.GetComponent<BoxCollider>().enabled);

        }



    }

    public IEnumerator ImmunetoSteal(Money money)
    {
        money.GetComponent<BoxCollider>().enabled = false;

        //Debug.Log(money.GetComponent<BoxCollider>().enabled);
        yield return new WaitForSeconds(1);

        money.GetComponent<BoxCollider>().enabled = true;
        //Debug.Log(money.GetComponent<BoxCollider>().enabled);

    }

    public void StartImmuneToSteal(Money money)
    {
        StartCoroutine(ImmunetoSteal(money));
    }


}
