using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRotation : MonoBehaviour
{
    [SerializeField] private Transform stick;
    private Transform p;


    void FixedUpdate()
    {
       gameObject.transform.position = stick.position;
    }


}
