using UnityEngine;
using StateManagment;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int health = 100;
    [SerializeField] int damage = 50;
    [SerializeField] int bulletDamage = 25;

    [SerializeField] private PlayerAim playerAim;


    Vector3 direction;

    private enum myStates { Chasing, Attacking, Dying };

    //You can use anything you want as the state definition
    StateMachine<myStates> machine = new StateMachine<myStates>();

    void Awake()
    {

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
    }

    

    void FirstStateTick()
    {
        direction = Vector3.Scale((playerTransform.position - transform.position), new Vector3(1, 0, 1)).normalized;
        rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void SecondStateTick()
    {
        rb.velocity = Vector3.zero;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && playerAim.isAttacking)
        {
            health -= damage;
        }

        if (other.gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
        }

    }
}