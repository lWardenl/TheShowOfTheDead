using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;

    Vector3 forward, right;

    void Start()
    {

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        // -45 degrees from the world x axis
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;


    }

    // Update is called once per frame
    void Update()
    {

        // Movement
        if (Input.anyKey)
        {
            Move();
        }

    }

    void Move()
    {

        // Movement speed
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * walkSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * walkSpeed * Time.deltaTime *Input.GetAxis("Vertical");

        // Calculate what is forward
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //rotation
        transform.forward = heading;
        //movement
        transform.position += rightMovement;
        transform.position += upMovement;

    }
}
