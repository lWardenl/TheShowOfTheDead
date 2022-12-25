using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] private float attackInterval;
    [SerializeField] private UnityEvent onClick;

    private float attackResetTimer = 0;
    private bool _isAttacking;
    private bool isAttacking { 
        get { 
            return _isAttacking; 
        } 
        set {
            if (_isAttacking != value)
                anim.SetBool("isAttacking", value);
            _isAttacking = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackResetTimer > 0)
            attackResetTimer -= Time.deltaTime;
        else
            isAttacking = false;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        attackResetTimer = attackInterval;
        isAttacking = true;
        onClick?.Invoke();
    }
}
