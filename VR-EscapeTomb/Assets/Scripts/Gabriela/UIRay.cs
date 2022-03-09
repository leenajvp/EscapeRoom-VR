using System.Collections.Generic;
using UnityEngine;

public class UIRay : MonoBehaviour
{
    public GameObject CameraXR;
    [SerializeField] private GameObject canvas;

    [SerializeField] private LineRenderer laser;
    [SerializeField] private Transform rayCenter;
    [SerializeField] private Transform laserAim;
    [SerializeField] private float maxDistance;

    private bool canvasActive;

    void Start()
    {
        canvasActive = true;
        canvas.SetActive(true);
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

            if (canvasActive == true)
            {
                if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    if (triggerValue)
                    {
                        laser.enabled = true;

                        Ray teleportRay = new Ray(rayCenter.transform.position, rayCenter.transform.forward);
                        RaycastHit hit;

                        if (Physics.Raycast(teleportRay, out hit, maxDistance))
                        {
                            laser.SetPosition(0, laserAim.transform.position);
                            laser.SetPosition(1, hit.point);

                            if (hit.transform.gameObject.tag == "Button")
                            {
                                //canvas.SetActive(false);
                            }
                        }

                        else
                        {
                            laser.enabled = false;
                        }
                    }
                }
            }

        }
    }
}

