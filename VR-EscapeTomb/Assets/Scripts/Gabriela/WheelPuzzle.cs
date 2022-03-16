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

    //private Transform rightRotation;
    // public GameObject rightHand;
    // private Transform rightHandOriginalParent;
    // private bool rightHandOnWheel;
    //  public Transform[] snappPositions;
    //  public float currentWheelRotation;
    //  public Transform directionalObject;

    void Start()
    {
        canRotate = true;
        numberShown = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerHand")
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
            wheel.transform.rotation = Quaternion.Slerp(rot_old, rot_new, t);
            yield return new WaitForSeconds(1f);
        }

        wheel.transform.rotation = rot_new;
        canRotate = true;

        numberShown += 1;

        if (numberShown > 6)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown);
    }


    /*

    void Update()
    {
        RealseHandFromWheel();

        ConvertHandRotationToWheelRotation();

        currentWheelRotation = -transform.rotation.eulerAngles.z;
    }

    void RealseHandFromWheel()
    {
        if (rightHandOnWheel == true) //button up
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHandOnWheel = false;
        }

        if (rightHandOnWheel == false)
        {
            transform.parent = null;
        }
    }

    void ConvertHandRotationToWheelRotation()
    {
        if (rightHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            this.transform.parent = directionalObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (rightHandOnWheel == false ) //and button get down
            {
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
            }
        }
    }
    void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        var shortestDistance = Vector3.Distance(snappPositions[0].position, hand.transform.position);
        var bestSnapp = snappPositions[0];

        foreach (var snappPosition in snappPositions)
        {
            if (snappPosition.childCount == 0)
            {
                var distance = Vector3.Distance(snappPosition.position, hand.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnapp = snappPosition;
                }
            }
        }

        originalParent = hand.transform.parent;

        hand.transform.parent = bestSnapp.transform;
        hand.transform.position = bestSnapp.transform.position;

        handOnWheel = true;
    }

    */

}
