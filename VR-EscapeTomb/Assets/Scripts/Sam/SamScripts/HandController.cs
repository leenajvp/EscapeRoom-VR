using System.Collections;
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
        public GameObject[] collidersOff;
        private GameObject currentObject;
        public float distance = 0.5f;
        public bool pickUpInHand;
        public Transform target;
        private bool triggerValue;
        void Start()
        {
            GetInputDevice();
        }

        void GetInputDevice()
        {
            List<InputDevice> devices = new List<InputDevice>();
            // checking for lift of devices, device must be chose from the list 
            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
            /*
             * for(int i = 0; i < devices.Count;i++)
            {
                targetDevice = devices[i];
            }
            */
            if (devices.Count > 0)
            {
                targetDevice = devices[0];
            }
        }

        void UpdateHandAnimation()
        {
            //Animation
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
            // checking for picking item
            CheckPickUp();

            if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
            {
                DisableHandCollider();
                if (currentObject != null)
                {
                    PickUp();
                }
            }
            else
            {
                StartCoroutine(Delay());
            }
        }
        public IEnumerator Delay()
        {
            Drop();
            yield return new WaitForSeconds(1);
            EnableHandCollider();
        }
        public void EnableHandCollider()
        {
            foreach (var item in collidersOff)
            {
                item.gameObject.SetActive(true);
            }
        }
        public void DisableHandCollider()
        {
            foreach (var item in collidersOff)
            {
                item.gameObject.SetActive(false);
            }
        }
        public void CheckPickUp()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (hit.transform.tag == "InteractableObj")
                {
                    currentObject = hit.transform.gameObject;
                }
            }
        }
        public void PickUp()
        {
            currentObject.transform.position = target.position;
            currentObject.transform.parent = target;
            currentObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            pickUpInHand = true;
        }
        public void Drop()
        {
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject = null;
            currentObject.transform.parent = null;
            pickUpInHand = false;
        }
    }
}


