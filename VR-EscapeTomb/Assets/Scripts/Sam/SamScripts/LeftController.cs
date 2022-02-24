using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Controllers.Left
{
    public class LeftController : MonoBehaviour
    {
        private GameObject touchingObj;
        public bool isTouching;
        public bool trigerValue;
        private bool Value;
        [SerializeField]
        private Animator h_Animator;
        private const string ANIMATOR_GRIP = "Grip";
        private float h_GripTarget = 1.0f;
        private float h_CurGrip = 0.0f;
        public float h_speed = 8.0f;
        GameObject hand;

        private void Start()
        {
            hand.transform.Rotate(0.0f, 0.0f, 75f);
        }

        // Update is called once per frame
        void Update()
        {
            var leftHandController = new List<InputDevice>();
            var disiredCharacteristicsL = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(disiredCharacteristicsL, leftHandController);
            foreach (var device in leftHandController)
            {

                Vector3 position;
                if (device.TryGetFeatureValue(CommonUsages.devicePosition, out position))
                {
                    transform.localPosition = position;
                }
                Quaternion orientation;
                if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out orientation))
                {
                    transform.localRotation = orientation;
                }


                if (device.TryGetFeatureValue(CommonUsages.gripButton, out trigerValue) && trigerValue)
                {
                    Debug.Log("Trigger button was pressed");
                    if (touchingObj != null)
                    {
                        touchingObj.transform.position = transform.position;
                        touchingObj.transform.rotation = transform.rotation;
                    }
                }
                // checking if grip button is used.
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out Value))
                {
                    Debug.Log("print" + Value);
                    // if the button is used then the animation will start playing 
                    if (Value != false)
                    {
                        h_CurGrip = Mathf.MoveTowards(h_CurGrip, 1f, Time.deltaTime * h_speed);
                        h_Animator.SetFloat(ANIMATOR_GRIP, h_CurGrip);

                    }
                    else
                    {
                        h_CurGrip = Mathf.MoveTowards(h_CurGrip, 0f, Time.deltaTime * h_speed);
                        h_Animator.SetFloat(ANIMATOR_GRIP, h_CurGrip);
                    }
                }
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            touchingObj = other.gameObject;
            isTouching = true;
        }

        private void OnTriggerExit(Collider other)
        {
            touchingObj = null;
            isTouching = false;
        }
    }

}

