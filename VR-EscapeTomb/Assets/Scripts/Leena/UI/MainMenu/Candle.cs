using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Stick stick = other.gameObject.GetComponent<Stick>();
        if (stick)
        {
            stick.isLit = true;
            StartCoroutine(stick.BurnTimer());
        }
    }
}
