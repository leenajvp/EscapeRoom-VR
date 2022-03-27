using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(LineRenderer))]
public class UIRaycast : MonoBehaviour
{
    [Header("UI Sounds")]
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource sliderClick;
    [SerializeField] private AudioSource toggleSound;

    [Header("Haptic feedback")]
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float strenght = 0.1f;
    public bool hapticOn = false;


    private float triggerTimer = 0;
    private bool lastState = false;
    private bool triggerValue;
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
            var controllerInput = controller.TryGetFeatureValue(primaryButton, out triggerValue) && triggerValue;
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

                        if (controllerInput)
                        {
                            buttonHit.GetComponent<Button>().onClick.Invoke();
                            buttonClick.Play();
                        }
                    }

                    else if (!buttonHit && selectedObject)
                    {
                        selectedObject.GetComponent<Image>().color = Color.white;
                        selectedObject = null;
                    }

                    Slider sliderHit = hit.collider.gameObject.GetComponent<Slider>();

                    if (sliderHit)
                    {
                        if (controllerInput)
                        {
                            if (triggerValue != lastState)
                            {
                                if (!sliderHit.GetComponent<VolumeBar>().background)
                                    sliderClick.Play();

                                if (sliderHit.value != 1)
                                    sliderHit.value += 0.2f;

                                else
                                    sliderHit.value = 0;

                                triggerTimer = Time.time;
                            }
                        }
                    }

                    Toggle toggleHit = hit.collider.gameObject.GetComponent<Toggle>();

                    if (toggleHit)
                    {
                        if (Time.time < triggerTimer + 1f)
                            return;

                        if (controllerInput)
                        {
                            if (triggerValue != lastState)
                            {
                                toggleSound.Play();
                                toggleHit.isOn = !toggleHit.isOn;
                                lastState = triggerValue;
                            }
                        }
                    }

                    if (sliderHit || buttonHit || toggleHit)
                    {
                        if (!hapticOn)
                        {
                            rightHandedControllers.ForEach(c => c.SendHapticImpulse(0, strenght, 0.01f));
                            hapticOn = true;
                        }

                    }

                    else if (!sliderHit || !buttonHit || !toggleHit)
                    {
                        hapticOn = false;
                    }

                }


            }
        }
    }
}

