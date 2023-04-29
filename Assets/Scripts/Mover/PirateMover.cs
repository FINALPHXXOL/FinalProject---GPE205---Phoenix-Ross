using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMover : Mover
{
    // Variable to hold the Rigidbody Component
    public Rigidbody rb;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float turnSpeed)
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
