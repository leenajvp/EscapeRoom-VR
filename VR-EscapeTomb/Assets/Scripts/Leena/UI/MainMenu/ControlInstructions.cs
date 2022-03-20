using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControlInstructions : UIFade
{
    [SerializeField] private Canvas canvasToActivate = null;
    [SerializeField] private float activateAfterSeconds = 5.0f;
    public bool startGame;
    private List<InputDevice> rightHandedControllers = new List<InputDevice>();
    private bool triggerValue;

    protected override void Start()
    {
        base.Start();
        canvasToActivate = GetComponent<Canvas>();
    }

    public void StartGame()
    {
        startGame = true;
    }

    protected override void Update()
    {
        base.Update();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        var triggerButton = CommonUsages.triggerButton;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        if (startGame)
        {
            gameObject.SetActive(true);

            foreach (var controller in rightHandedControllers)
            {
                if (controller.TryGetFeatureValue(triggerButton, out triggerValue) && triggerValue)
                {
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
