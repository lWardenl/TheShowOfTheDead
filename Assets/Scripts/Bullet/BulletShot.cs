using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Rigidbody rb;


    // Update is called once per frame
    void Update()
    {
       transform.position += transform.forward * bulletSpeed * Time.deltaTime;        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
