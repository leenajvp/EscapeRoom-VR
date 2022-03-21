using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPapirus : MonoBehaviour
{
    public GameObject correctForm;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Papirus3Socet")
        {
            transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
            transform.rotation = correctForm.transform.rotation;
            rb.isKinematic = true;

        }
   
    }
}
