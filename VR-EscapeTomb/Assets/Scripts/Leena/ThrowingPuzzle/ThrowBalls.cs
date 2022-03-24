using UnityEngine;

public class ThrowBalls : MonoBehaviour
{
    private bool held = false;
    private Vector3 velocity;
    private Rigidbody rb;
    private GameObject holdingHand;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        FixedJoint joint = GetComponent<FixedJoint>();

        if (joint != null)
        {
            holdingHand = joint.connectedBody.gameObject.GetComponent<HandTrackingPhysics>().palm.gameObject; // possibly do somenthing with centre of mass
            velocity = rb.velocity;
            held = true;
            rb.mass = 0;
        }

        if (joint == null && held)
        {
            if (velocity.y > 1f)
            {
                rb.drag = 1;
                Debug.Log("y slowed");
            }

            if (velocity.x > 1f)
            {
                rb.drag = 1;
                Debug.Log("x slowed");
            }

            if (velocity.z > 1f)
            {
                rb.drag = 2;
                Debug.Log("z slowed");
            }

            held = false;
        }
    }
}
