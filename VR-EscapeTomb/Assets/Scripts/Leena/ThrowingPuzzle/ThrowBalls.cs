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
        float maxDarg = 1f;

        if (transform.childCount != 0)
        {
            velocity = rb.velocity;
            held = true;
        }

        if (transform.childCount == 0 && held)
        {
            if (velocity.z > -0.5f || velocity.z < -0.5f || velocity.x > -0.5f || velocity.x < -0.5f || velocity.y > 0.5f || velocity.y < -0.5f)
            {
                rb.mass += 5f;

                if (rb.drag <= maxDarg)
                    rb.drag += 0.1f;

                Debug.Log("speed");
            }

            if (velocity.z < -3f || velocity.x > -3f)
            {
                rb.drag += 0.5f * Time.deltaTime;
                rb.mass += 5 * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (held)
        {
            rb.drag = 0;
            rb.mass = 0;
        }
    }
}
