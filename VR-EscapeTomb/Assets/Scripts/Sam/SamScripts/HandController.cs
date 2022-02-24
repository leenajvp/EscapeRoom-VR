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
        public bool isTouching;
        public GameObject touchingobj;
        private Collider[] handCollider;
        void Start()
        {
            GetInputDevice();
            handCollider = GetComponentsInChildren<Collider>();
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
        void FixedUpdate()
        {
            if (!targetDevice.isValid)
            {
                GetInputDevice();
            }
            else
            {
                UpdateHandAnimation();
            }
            bool triggerValue;
            if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue))
            {
                if(isTouching)
                {
                    touchingobj.transform.position = transform.position;
                    touchingobj.transform.rotation = transform.rotation;
                }

            }
        }


        public void OnTriggerEnter(Collider other)
        {
            isTouching = true;
            Debug.Log("isTouch" + isTouching);
            touchingobj = other.gameObject;
            DisableHandCollider();

        }

        private void OnTriggerExit(Collider other)
        {
            touchingobj = null;
            EnableHandCollider();
            isTouching = false;

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

    }
}


