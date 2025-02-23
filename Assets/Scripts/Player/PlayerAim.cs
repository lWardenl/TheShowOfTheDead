using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator playerAnim;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    public bool isAttacking => !(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Run"));
    private bool isBlocking => (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Block"));

    private void Awake()
    {
        mainCamera = Camera.main;
        direction = transform.forward;
    }   
    // Update is called once per frame
    void Update()
    {
        /*if (isAttacking || isBlocking)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 0.3f);*/
    }

    public void Aim()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 mousePosition = hit.point;

            direction = mousePosition - transform.position;

            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
