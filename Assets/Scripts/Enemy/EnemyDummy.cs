using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    [SerializeField] private Animator dummyAnim;
    [SerializeField] private PlayerAim playerAim;

    private void Start()
    {
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && playerAim.isAttacking)
        {
            dummyAnim.SetTrigger("Hit");
        }

        if (other.gameObject.tag == "Bullet")
        {
            dummyAnim.SetTrigger("Hit");
        }

    }

}
