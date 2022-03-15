using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaycast : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    public GameObject selectedObject = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        foreach (var controller in rightHandedControllers)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 30))
            {
                lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
                lineRenderer.SetPosition(1, hit.point);

                if (hit.collider)
                {
                    Button buttonHit = hit.collider.gameObject.GetComponent<Button>();
                    Image image = hit.collider.gameObject.GetComponent<Image>();

                    if (buttonHit)
                    {
                        selectedObject = buttonHit.gameObject;
                        image.color = Color.grey;

                        bool triggerValue;

                        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue)
                        {
                            buttonHit.GetComponent<Button>().onClick.Invoke();
                        }
                    }

                    else if (!buttonHit && selectedObject)
                    {
                        selectedObject.GetComponent<Image>().color = Color.white;
                        selectedObject = null;
                    }

                    else
                        return;
                }
            }
        }
    }
}

