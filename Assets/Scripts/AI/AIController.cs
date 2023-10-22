using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    Vector3 move;
    private StateMachine sm = new StateMachine();
    [SerializeField] float speed, movementLength;
    [SerializeField] float collectMultiplier;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask layerMask, obsyacleLayer;
    [SerializeField] public NavMeshAgent agent;
    public Vector3 targetPoint;
    Money money;
    [SerializeField] Animator charAnimator;
    Collect collect;
    bool canMove;
    //public NavMeshPath path = new NavMeshPath();
    private void Awake()
    {
        SetupComponents();
        sm.Init(this,charAnimator,collect);

    }
    private void Start()
    {
      
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {

        sm.currentState.UpdateState(sm);

        

    }

    private void SetupComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        sm = gameObject.AddComponent<StateMachine>();
        rb = GetComponent<Rigidbody>();
        collect = GetComponent<Collect>();
        charAnimator = GetComponent<Animator>();

    }
   


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 10);



    }

}
