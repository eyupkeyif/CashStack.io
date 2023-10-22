using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    EnemyBase enemyBase;
   
    [SerializeField] CameraManager cameraManager;
    [SerializeField] List<Money> moneys;
    public delegate void MoneyCollectDelegate();
    public MoneyCollectDelegate onMoneyCollect;
    public delegate void MoneyDeliverDelegate();
    public MoneyDeliverDelegate onMoneyDeliver;
    [SerializeField] float speed;
    [SerializeField] public int capacity;
    public bool isCollected = false;
    public int moneyCount;
    public int totalMoney;
    Vector3 lastPosition;

    private void Awake()
    {
        //onMoneyCollect += ViewController.instance.UpdateProgressBar;
        //onMoneyCollect += ViewController.instance.UpdateComboBar;
        SetupComponents();

    }
    private void Start()
    {
        StartCoroutine(FollowCoroutine());
    }

    protected virtual void SetupComponents()
    {
        
        capacity = 20;
     
        moneyCount = 0;
        totalMoney = 0;

    }
    private void FixedUpdate()
    {
        lastPosition = transform.position - transform.forward * 4f + new Vector3(0, 6, 0);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Money money = other.gameObject.GetComponent<Money>();
            if (money != moneys.Contains(money)&& money.GetOwner()==null)
            {
                if (moneys.Count < capacity)
                {
                   
                    moneys.Add(money);

                    money.scalingFeedback?.PlayFeedbacks();
                    money.SetOwner(this);
                    isCollected = true;
                    OnCollectMoney();

                }


            }
            if (money == moneys.Contains(money))
            {
                isCollected = false;
                money.SetOwner(this);
            }

            if (money.GetOwner()!=null && money.GetOwner()!=this)
            {
                StealSystem stealSystem = money.GetOwner().gameObject.GetComponent<StealSystem>();

                if (stealSystem)
                {
                    stealSystem.StealMoney(money, money.GetOwner(), this);
                    for (int i = 0; i < moneys.Count; i++)
                    {
                        stealSystem.StartImmuneToSteal(moneys[i]);
                    }
                }
            }


        }
    }

   

    private IEnumerator FollowCoroutine()
    {
        while (true)
        {
            if (moneys.Count != 0)
            {


                for (int i = 0; i < moneys.Count; i++)
                {


                    Vector3 offset = new Vector3(0, 6, 0);
                    moneys[0].transform.position = transform.position - transform.forward * 4 + offset;
                    Vector3 smoothRotate = Vector3.Lerp(moneys[0].transform.forward, transform.forward * 4, 0.125f);
                    moneys[0].transform.rotation = Quaternion.LookRotation(smoothRotate);
                    if (i != 0)
                    {
                        moneys[i].transform.position = moneys[i - 1].transform.position - moneys[i - 1].transform.forward * 4f;
                        Vector3 smoothRotate2 = Vector3.Lerp(moneys[i].transform.forward, moneys[i - 1].transform.forward * 4, 0.125f);

                        moneys[i].transform.rotation = Quaternion.LookRotation(smoothRotate2, Vector3.up);


                    }



                }    //moneys[0].transform.position = lastPosition;

                    //if (moneys.Count>0)
                    //{


                    //    Money _money = moneys[0];
                    //    moneys.RemoveAt(0);
                    //    moneys.Add(_money);

                    //}



                }
            yield return new WaitForFixedUpdate();


        }

    }



    


    public List<Money> MoneysList()
    {
        return moneys;
    }



    public int GetMoneyCount()
    {
        return moneyCount;
    }
    public int SetTotalMoney()
    {

        return totalMoney;

    }

   public void OnCollectMoney()
    {
        moneyCount++;
        onMoneyCollect?.Invoke();
        Debug.Log(moneyCount);

    }
}
