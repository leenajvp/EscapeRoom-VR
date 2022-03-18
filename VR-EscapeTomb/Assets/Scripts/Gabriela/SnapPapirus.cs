using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPapirus : MonoBehaviour
{
    public GameObject correctForm;
    private bool moving;
    private bool finish;
    private Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.localPosition;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            OnMove();
        }
    }

    private void OnMove()
    {
        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f && 
            Mathf.Abs (this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f)
        {
            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moving = true;
        }
        else
            moving = false;
    }

    private void OnTriggerExit(Collider other)
    {
        moving = false;  
    }
}
