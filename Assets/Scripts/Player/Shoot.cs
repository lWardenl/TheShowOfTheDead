using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Animator diverAnim;
    [SerializeField] private UnityEvent onClick;

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke();
            diverAnim.SetTrigger("Shoot");
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            
        }
    }
}
