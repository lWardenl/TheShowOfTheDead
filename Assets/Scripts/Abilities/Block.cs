using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private UnityEvent onClick;

    private bool isBlocking;

    void Update()
    {
        Blocking();
    }

    private void Blocking()
    {
        if(Input.GetMouseButton(1)) 
        {
            isBlocking= true;
            anim.SetBool("isBlocking", isBlocking);
            onClick?.Invoke();
        }
        if(Input.GetMouseButtonUp(1))
        {
            isBlocking= false;
            anim.SetBool("isBlocking", isBlocking);
        }
    }
}
