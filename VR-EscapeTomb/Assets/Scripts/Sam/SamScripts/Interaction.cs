using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Interaction : MonoBehaviour
{
    private InputDevice device;
    public InputDeviceCharacteristics controllerCharacteristics;
    public bool isTouching;
    public GameObject currentObject;
    public GameObject pickUp;
    public Transform target;
    public bool triggerValue;
    public float distance = 2f;
    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // checking for lift of devices, device must be chose from the list 
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

    }
    public void FixedUpdate()
    {
        CheckPickUp();

       if(isTouching)
        {
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
            {
                Debug.Log("Trigger button was pressed");
                if (currentObject != null)
                    Drop();
                PickUp();
            }else
            {
                Drop();
            }
        }
        
    }
    public void CheckPickUp()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance))
        {
            if(hit.transform.tag == "InteractableObj")
            {
                isTouching = true;
                pickUp = hit.transform.gameObject;
            }
        }else
        {
            isTouching = false;
        }
    }
    public void PickUp()
    {
        currentObject = pickUp;
        currentObject.transform.position = target.position;
        currentObject.transform.parent = target;
        currentObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
       // currentObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void Drop()
    {
        currentObject.transform.parent = null;
        // currentObject.GetComponent<Rigidbody>().isKinematic = false;
        currentObject = null;
    }
   /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractableObj")
        {
            PickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Drop();
    }
     */
}

