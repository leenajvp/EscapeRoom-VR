using System;
using System.Collections;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    [SerializeField] private GameObject wheel;
    [SerializeField] private float targetRot_z = 60;
    [SerializeField] private float turnSpeed = 0.5f;

    private bool canRotate;
    private float t;
    private int numberShown = 6;

    void Start()
    {
        canRotate = true;
        numberShown = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (canRotate == true)
            {
                StartCoroutine(Turn());
            }
        }
    }

    private IEnumerator Turn()
    {
        canRotate = false;

        Quaternion rot_old = wheel.transform.rotation;
        wheel.transform.Rotate(0, 0, targetRot_z);
        Quaternion rot_new = wheel.transform.rotation;

        for (t = 0.0f; t <= 1.0f; t += (turnSpeed * Time.deltaTime))
        {
            wheel.transform.rotation = Quaternion.Slerp(rot_old, rot_new, t).normalized;
            yield return new WaitForSeconds(1f);
        }

        wheel.transform.rotation = rot_new;
        canRotate = true;

        numberShown += 1;

        if (numberShown > 5)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown);
    }
}
