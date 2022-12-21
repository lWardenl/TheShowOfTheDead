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
       rb.velocity = transform.forward * bulletSpeed * Time.deltaTime;        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
