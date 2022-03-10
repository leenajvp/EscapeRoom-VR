using System.Collections.Generic;
using UnityEngine;

public class UIRay : MonoBehaviour
{
    public GameObject CameraXR;

    [SerializeField] private LineRenderer laser;
    [SerializeField] private Transform laserAim;
    [SerializeField] private float maxDistance;

    void Start()
    {
      //  laser = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        foreach (var controller in rightHandedControllers)
        {
            Vector3 position;
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out position))
            {
                this.transform.localPosition = position;
            }

            Quaternion orientation;
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out orientation))
            {
                this.transform.localRotation = orientation;
            }

            //UI Ray 
            bool triggerValue;

            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                laser.gameObject.SetActive(true);

                Vector3 origin = transform.position;
                RaycastHit hit;

                if (Physics.Raycast(origin, transform.forward, out hit, maxDistance))
                {
                    laser.SetPosition(0, origin);
                    laser.SetPosition(1, hit.point);

                    if (hit.transform.gameObject.tag == "UI")
                    {
                        //Destroy(hit.transform.gameObject);
                    }
                }

            }
            else
            {
                laser.gameObject.SetActive(false);
            }

        }
    }
}

