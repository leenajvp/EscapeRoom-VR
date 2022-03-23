using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControlInstructions : UIFade
{
    [SerializeField] private float activateAfterSeconds = 5.0f;
    public bool startGame;
    private List<InputDevice> rightHandedControllers = new List<InputDevice>();
    private bool triggerValue;

    protected override void Update()
    {
        base.Update();
        FadeIn();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        var triggerButton = CommonUsages.triggerButton;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            foreach (var controller in rightHandedControllers)
            {
                Debug.Log(controller);
                if (controller.TryGetFeatureValue(triggerButton, out triggerValue) && triggerValue)
                {
                    gameObject.SetActive(true);
                    FadeOut();
                }
            }
        }
    }

    public IEnumerator ShowInstructions()
    {
        yield return new WaitForSeconds(activateAfterSeconds);
        gameObject.SetActive(true);
    }
}
