using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandTrackingPhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public GameObject palm;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // position
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        //rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 roatationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * roatationAxis;
        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
