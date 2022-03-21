using UnityEngine;

public class SnapPapirus : MonoBehaviour
{
    public enum SocketNumber { Socket1, Socket2, Socket3 }
    public SocketNumber socketNumber;

    public GameObject correctForm;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 19)
        {
            if (socketNumber == SocketNumber.Socket1)
            {
                transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
                transform.rotation = correctForm.transform.rotation;
                rb.isKinematic = true;
                correctForm.gameObject.SetActive(false);
            }

            else if (socketNumber == SocketNumber.Socket2)
            {
                transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
                transform.rotation = correctForm.transform.rotation;
                rb.isKinematic = true;
                correctForm.gameObject.SetActive(false);
            }

            else if (socketNumber == SocketNumber.Socket3)
            {
                transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
                transform.rotation = correctForm.transform.rotation;
                rb.isKinematic = true;
                correctForm.gameObject.SetActive(false);
            }
        }
    }
}
