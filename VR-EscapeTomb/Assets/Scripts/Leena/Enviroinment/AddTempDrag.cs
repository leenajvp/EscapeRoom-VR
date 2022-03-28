using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTempDrag : MonoBehaviour
{
    Rigidbody rb;

    private void Start() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (transform.childCount != 0)
        {
            rb.drag = 250;
            rb.angularDrag = 200;
        }

        if (transform.childCount == 0)
        {
            rb.drag = 1;
            rb.angularDrag = 1;
        }
    }
}
