using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPuzzle : MonoBehaviour
{
    public bool completed;

    private void OnTriggerEnter(Collider other)
    {
        ThrowBalls ball = other.gameObject.GetComponent<ThrowBalls>();

        if(ball)
            completed = true;
    }
}
