using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }   
    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
            Aim();
    }

    private void Aim()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 mousePosition = hit.point;

            Vector3 direction = mousePosition- transform.position;

            direction.y = 0;

            transform.forward = direction;
        }
    }
}
