using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableClimbing : MonoBehaviour
{
    public GameObject[] handcoliders;
    // Start is called before the first frame update
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 17)
        {
            for (int i = 0; i < handcoliders.Length; i++)
            {
                handcoliders[i].SetActive(true);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            for (int i = 0; i < handcoliders.Length; i++)
            {
                handcoliders[i].SetActive(false);
            }
        }
    }
}
