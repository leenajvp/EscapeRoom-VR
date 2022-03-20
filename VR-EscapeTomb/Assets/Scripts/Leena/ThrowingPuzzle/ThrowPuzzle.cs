using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPuzzle : MonoBehaviour
{
    public bool completed;

    private void OnTriggerEnter(Collider other)
    {
        TheowBalls ball = other.gameObject.GetComponent<TheowBalls>();

        if(ball)
            completed = true;
    }
}
