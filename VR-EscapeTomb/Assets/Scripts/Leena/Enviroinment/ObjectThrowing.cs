using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    private bool held = false;
    private Vector3 velocity;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != 0)
        {
            held = true;
            velocity = rb.velocity;
        }

        if(held && transform.childCount == 0)
        {
            if(velocity.x > 2)
            {
                rb.AddForce(Vector3.up*1);
            }
            held = false;
        }
    }
}
