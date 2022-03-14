using System.Collections;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private float targetRot_z = 60;
    [SerializeField] private float turnSpeed = 1f;

    private bool canRotate;
    private float t;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hands")
        {
            canRotate = true;
            {
                if (canRotate == true)
                {
                    StartCoroutine(Turn());
                }
            }
        }
    }

    private IEnumerator Turn()
    {
        Quaternion rot_old = wheel.transform.rotation;
        wheel.transform.Rotate(0, 0, targetRot_z);
        Quaternion rot_new = wheel.transform.rotation;

        for (t = 0.0f; t <= 1.0f; t += (turnSpeed * Time.deltaTime))
        {
            wheel.transform.rotation = Quaternion.Slerp(rot_old, rot_new, t);
            yield return null;
        }

        wheel.transform.rotation = rot_new;
        canRotate = false;
    }

}
