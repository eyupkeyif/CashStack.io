using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class Money : MonoBehaviour
{
    public Collect collector;
    //StealSystem stealSystem;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject deliverParticle,deliverParticle2;
    [SerializeField] LayerMask layerMask;
    
    //[SerializeField] float purpleR, purpleG, purpleB;
    //[SerializeField] float yellowR, yellowG, yellowB;
    //[SerializeField] float redR, redG, redB;
    //[SerializeField] float blueR, blueG, blueB;

    [SerializeField] Color redEnemy, purpleEnemy, bluePlayer, yellowEnemy;
    public bool isStolen=false;
    public bool canSteal = false;
    [SerializeField] float destroyTimer;
    public MMF_Player scalingFeedback;
    public MMF_Player rotatingFeedback;
    public MMF_Player deliveringFeedback;
    //public ParticleSystem deliverParticle;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (layerMask == (layerMask | (1 << other.gameObject.layer)))
    //    {
    //        collector = other.gameObject.GetComponent<Collect>();
    //        Debug.Log(other.gameObject.name);
    //        if (GetOwner()!=collector)
    //        {
    //            Debug.Log("stealSystem");
    //            Collect collector2 = other.gameObject.GetComponent<Collect>();
    //            stealSystem = other.gameObject.GetComponent<StealSystem>();
    //            if (stealSystem)
    //            {
    //                //Debug.Log("stealSystemActive");
    //                stealSystem.StealMoney(this,collector,collector2);
    //                //StartCoroutine(AfterStealInvisibility(3, this));
    //            }

    //        }
    //        else
    //        {


    //                Debug.Log("money is not inside of the list"+ GetOwner().gameObject.name);
    //                if (this != collector.MoneysList().Contains(this) && collector.MoneysList().Count < collector.capacity)
    //                {

    //                    Debug.Log("money does not have an owner");
    //                    collector.MoneysList().Add(this);
    //                    SetOwner(collector);
    //                    collector.moneyCount++;
    //                }



    //        }
    //    }
    //}
    private void Start()
    {
        //scalingFeedback?.Play(transform.position);
    }
    public GameObject StolenMoney()
    {
        return gameObject;
    }

    public void SetOwner(Collect collect)
    {
        collector = collect;
        if (collector)
        {
            canSteal = true;
        }
        else
        {
            canSteal = false;
        }
    }

    public Collect GetOwner()
    {
        return collector;
    }
    

    public void StealParticle()
    {
        particle.Play();
    }

    public IEnumerator StealPosition(float timer)
    {
        //Debug.Log("steal working");
        float time = 0;
        float x = Random.Range(10, 50);
        float z = Random.Range(10, 50);
        Vector3 randomPos = new Vector3(x, 0, z);
        while (time<=timer)
        {
            Vector3 finalPosition = transform.position + randomPos;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, finalPosition, time / timer);
            transform.position = smoothPosition;
            time += Time.deltaTime;

            
        }

        yield return new WaitForFixedUpdate();
    }

    public void CoroutineStarter(float timer,Transform deliveryPoint)
    {
        //deliverParticle.transform.position = Vector3.zero;

        StartCoroutine(DeliverMoney(timer,deliveryPoint));
        //collector.totalMoney++;

        GameObject particle = Instantiate(deliverParticle, deliveryPoint);
        DollarExplosion dollarExplosion = particle.GetComponent<DollarExplosion>();
        ColorChange(dollarExplosion);
        
        particle.transform.position = deliveryPoint.position;
        if (deliverParticle2)
        {
            GameObject particle2 = Instantiate(deliverParticle2, deliveryPoint);

            particle2.transform.position = deliveryPoint.position;
            Destroy(particle2, destroyTimer);
               
        }
            Destroy(particle, 3f);
        
        deliveringFeedback?.PlayFeedbacks();

    }

    public void DistrcctMoneyStarter(float timer,Transform thief)
    {
        StartCoroutine(DistractMoney(timer, thief));
        rotatingFeedback?.PlayFeedbacks();
        //scalingFeedback?.PlayFeedbacks();
    }

    public IEnumerator DeliverMoney(float timer,Transform deliveryPoint)
    {
        float time = 0;

        while (time < timer)
        {
            transform.position = Vector3.Lerp(transform.position, deliveryPoint.position + new Vector3(0, 5, 0), time / timer);

                
            
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            
        }


    }



    public IEnumerator DistractMoney(float timer, Transform thief)
    {
        float time = 0;
        Vector2 randomDirection = Random.insideUnitCircle.normalized * 15;
        Vector3 randPosition = new Vector3(transform.position.x,0,transform.position.z) +  new Vector3(randomDirection.x,0,randomDirection.y);
        while (time < timer)
        {
           
            transform.position = Vector3.Lerp(transform.position,randPosition, time / timer);
          

            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void ColorChange(DollarExplosion dollar)
    {
        if (collector.gameObject.CompareTag("Purple"))
        {
            dollar.ChangeMoneyColor(purpleEnemy);
            
        }
        else if (collector.gameObject.CompareTag("Yellow"))
        {
            dollar.ChangeMoneyColor(yellowEnemy);
        }
        else if (collector.gameObject.CompareTag("Red"))
        {
            dollar.ChangeMoneyColor(redEnemy);
        }
        else if (collector.gameObject.CompareTag("Blue"))
        {
            dollar.ChangeMoneyColor(bluePlayer);
        }
        
    }
}
