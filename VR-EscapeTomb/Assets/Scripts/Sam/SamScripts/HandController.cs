using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace Controllers
{ 
      public class HandController : MonoBehaviour
    {
        public InputDeviceCharacteristics controllerCharacteristics;
        private InputDevice targetDevice;
        public Animator handAnimator;
        public Collider[] handCollider;
        public GameObject currentObject;
        public GameObject pickUp;
        public float distance = 0.5f;
        public bool isTouching;
        public Transform target;

        void Start()
        {
            GetInputDevice();
        }

        void GetInputDevice()
        {
            List<InputDevice> devices = new List<InputDevice>();
            // checking for lift of devices, device must be chose from the list 
           InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
           
            if (devices.Count > 0)
            {
                targetDevice = devices[0];
            }
        }

        void UpdateHandAnimation()
        {

            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                handAnimator.SetFloat("Grip", gripValue);
            }
            else
            {
                handAnimator.SetFloat("Grip", 0);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
            if (!targetDevice.isValid)
            {
                GetInputDevice();
            }
            else
            {
                UpdateHandAnimation();
            }
            CheckPickUp();

            
                bool triggerValue;
                if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
                {
                    DisableHandCollider();
                    Debug.Log("Trigger button was pressed");
                    if (currentObject != null)
                        Drop();
                    PickUp();
                }
                else
                {
                    Drop();
                    EnableHandCollider();
                }
            
        }


        public void EnableHandCollider()
        {
            foreach (var item in handCollider)
            {
                item.enabled = true;
            }
        }

        public void DisableHandCollider()
        {
            foreach (var item in handCollider)
            {
                item.enabled = false;
            }
        }
        public void CheckPickUp()
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance))
            {
                if (hit.transform.tag == "InteractableObj")
                {
                    isTouching = true;
                    pickUp = hit.transform.gameObject;
                }
            }
            else
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
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        public void Drop()
        {
            currentObject.transform.parent = null;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject = null;
        }

    }
     
}


