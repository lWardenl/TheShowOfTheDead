using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    [SerializeField] private Animator dummyAnim;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private GameObject pressNToNext;

    private void Start()
    {
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
        pressNToNext.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && playerAim.isAttacking)
        {
            dummyAnim.SetTrigger("Hit");
            pressNToNext.SetActive(true);
        }

        if (other.gameObject.tag == "Bullet")
        {
            dummyAnim.SetTrigger("Hit");
            pressNToNext.SetActive(true);
        }

    }

}
