using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIRaycast : MonoBehaviour
{
    private float triggerTimer = 0;
    bool lastState = false;
    bool triggerValue;
    private LineRenderer lineRenderer = null;
    private GameObject selectedObject = null;
    private List<InputDevice> rightHandedControllers = new List<InputDevice>();
    private FixTelep teleportation => FindObjectOfType<FixTelep>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        teleportation.gameObject.SetActive(false);
    }

    private void Update()
    {
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        var primaryButton = CommonUsages.primaryButton;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        lastState = triggerValue;

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

                    if (buttonHit)
                    {
                        Image image = buttonHit.GetComponent<Image>();
                        selectedObject = buttonHit.gameObject;
                        image.color = Color.grey;

                        if (controller.TryGetFeatureValue(primaryButton, out triggerValue) && triggerValue)
                        {
                            buttonHit.GetComponent<Button>().onClick.Invoke();
                        }
                    }

                    else if (!buttonHit && selectedObject)
                    {
                        selectedObject.GetComponent<Image>().color = Color.white;
                        selectedObject = null;
                    }

                    Slider volume = hit.collider.gameObject.GetComponent<Slider>();

                    if (volume)
                    {
                        if (controller.TryGetFeatureValue(primaryButton, out triggerValue) && triggerValue)
                        {
                            if (triggerValue != lastState)
                            {
                                if (volume.value != 1)
                                {
                                    volume.value += 0.2f;
                                }

                                else
                                {
                                    volume.value = 0;
                                }

                                triggerTimer = Time.time;
                            }
                        }
                    }

                    Toggle toggle = hit.collider.gameObject.GetComponent<Toggle>();

                    if (toggle)
                    {
                        if (Time.time < triggerTimer + 1f)
                            return;

                        if (controller.TryGetFeatureValue(primaryButton, out triggerValue) && triggerValue)
                        {
                            if (triggerValue != lastState)
                            {
                                toggle.isOn = !toggle.isOn;
                                lastState = triggerValue;
                            }
                        }
                    }

                    else
                        return;
                }
            }
        }
    }
}

