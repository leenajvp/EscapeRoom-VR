using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardFaceUI : MonoBehaviour
{

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
