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
        float maxDarg = 1.5f;

        if (transform.childCount != 0)
        {
            velocity = rb.velocity;
            held = true;
        }

        if (transform.childCount == 0 && held)
        {
            if (velocity.z > -0.5f || velocity.z < -0.5f || velocity.x > -0.5f || velocity.x < -0.5f || velocity.y > 0.5f || velocity.y < -0.5f)
            {
                rb.mass += 0.5f;

                if (rb.drag <= maxDarg)
                    rb.drag += 0.1f;

                Debug.Log("speed");
            }
        }
    }
}
