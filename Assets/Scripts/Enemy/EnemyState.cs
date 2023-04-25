using UnityEngine;
using StateManagment;
using UnityEngine.AI;
using System.Threading;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int health = 100;
    [SerializeField] int damage = 50;
    [SerializeField] int bulletDamage = 25;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;

    private bool isAttacking;
    private float timeOut;
    Vector3 direction;

    private enum myStates { Chasing, Attacking, Dying };

    //You can use anything you want as the state definition
    StateMachine<myStates> machine = new StateMachine<myStates>();

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        //Add a state (first) with a tick function being FirstStateTick
        machine.AddState(myStates.Chasing, FirstStateTick, true);
        machine.AddState(myStates.Attacking, SecondStateTick);
        machine.AddState(myStates.Dying, ThirdStateTick);


        //Add a transition from state FIRST to SECOND with the condition written as a lambda expression
        //(Lambda expressions are especially useful for transition conditions)
        machine.AddTransition(myStates.Chasing, myStates.Attacking, () => Vector3.Distance(playerTransform.position,transform.position) < 4f);
        machine.AddTransition(myStates.Attacking, myStates.Chasing, () => Vector3.Distance(playerTransform.position,transform.position) > 4f);
        machine.AddTransition(myStates.Attacking, myStates.Dying, () => health <= 0f);
        machine.AddTransition(myStates.Chasing, myStates.Dying, () => health <= 0f);
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
    }

    

    void FirstStateTick()
    {
        isAttacking = false;

        agent.SetDestination(playerTransform.position - (playerTransform.position - transform.position).normalized * 4f);

        anim.SetFloat("Speed", moveSpeed);
        anim.SetBool("isAttacking", isAttacking);
    }

    void SecondStateTick()
    {
        anim.SetFloat("Speed", moveSpeed);
        rb.velocity = Vector3.zero;
        isAttacking = true;
        anim.SetBool("isAttacking", isAttacking);
    }

    void ThirdStateTick()
    {
        health = 0;
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        //Update the state machine
        machine.Tick();
    }

    private void Update()
    {
        if (timeOut > 0) { timeOut -= Time.deltaTime; }
        else if (timeOut < 0) { timeOut = 0; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && playerAim.isAttacking && timeOut == 0)
        {
            timeOut = 1.5f;
            health -= damage;
            anim.SetTrigger("isHit");
            print("damaged");
        }

        if (other.gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
            anim.SetTrigger("isHit");
        }

    }
}