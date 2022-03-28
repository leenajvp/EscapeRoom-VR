using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControlInstructions : UIFade
{
    public enum RequiredButton { trigger, grib };

    [Header("Instruction Panel")]
    [SerializeField] private bool activateFromStart = false;
    public RequiredButton instructedButton;
    [Tooltip("Wait time before actiovation when called")]
    [SerializeField] private float activateAfterSeconds = 5.0f;

    private List<InputDevice> rightHandedControllers = new List<InputDevice>();
    private bool triggerValue;

    protected override void Start()
    {
        base.Start();
        FadeOut();

        if (activateFromStart)
            StartCoroutine(ShowInstructions());
    }

    protected override void Update()
    {
        base.Update();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        var triggerButton = CommonUsages.triggerButton;
        var gribButton = CommonUsages.gripButton;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        if (fadeInOrOut == InOut.FadeIn)
        {
            foreach (var controller in rightHandedControllers)
            {
                if (instructedButton == RequiredButton.trigger)
                {
                    if (controller.TryGetFeatureValue(triggerButton, out triggerValue) && triggerValue)
                    {
                        FadeOut();
                    }
                }

                else
                {
                    if (controller.TryGetFeatureValue(gribButton, out triggerValue) && triggerValue)
                    {
                        FadeOut();
                    }
                }
            }
        }
    }

    public IEnumerator ShowInstructions()
    {
        yield return new WaitForSeconds(activateAfterSeconds);
        FadeIn();
    }
}
