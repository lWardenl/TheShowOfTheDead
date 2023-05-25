using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private int swordDamage = 20;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private Animator anim;

    private float timeOut;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
    }

    private void Update()
    {
        if (timeOut > 0) { timeOut -= Time.deltaTime; }
        else if (timeOut < 0) { timeOut = 0; }


        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
    public void Attack()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && playerAim.isAttacking && timeOut == 0)
        {
            timeOut = 1.5f;
            health -= swordDamage;
            anim.SetTrigger("isHit");
            print("damaged");
        }
    }
}
