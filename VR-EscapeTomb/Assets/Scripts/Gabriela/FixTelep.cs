using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTelep : MonoBehaviour
{
    public static FixTelep Instance;

    [SerializeField] public LineRenderer laser;
    [SerializeField] private int laserSteps = 25;
    [SerializeField] private float laserSegmentDistance = 1f, dropPerSegment = 1f;

    [SerializeField] Material freeMaterial;
    [SerializeField] Material busyMaterial;

    public GameObject CameraXR;

    private float teleportTimer;
    private bool triggerValue;


    void Awake()
    {
        laser.positionCount = laserSteps;
    }

    void Start()
    {
        Instance = this;
        laser = GetComponentInChildren<LineRenderer>();
    }

    void FixedUpdate()
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

            //fix teleportation logic             

            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                teleportTimer += Time.deltaTime;

                if (teleportTimer > 0.5f)
                {
                    laser.gameObject.SetActive(true);
                    TeleportTarget();
                }
            }

            else
            {
                teleportTimer = 0f;
                laser.gameObject.SetActive(false);
            }
        }
    }


    private void TeleportTarget()
    {
        laser.gameObject.SetActive(true);

        RaycastHit hit;
        Vector3 origin = transform.position;
        laser.SetPosition(0, origin);

        for (int i = 0; i < laserSteps - 1; i++)
        {
            Vector3 offset = (transform.forward + (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;

            if (Physics.Raycast(origin, offset, out hit, laserSegmentDistance))
            {
                for (int j = i + 1; j < laser.positionCount; j++)
                {
                    laser.SetPosition(j, hit.point);
                }

                if (hit.transform.gameObject.tag == "Teleport")
                {
                    CameraXR.transform.position = hit.transform.position;
                    return;
                }

                else
                {
                    laser.material.color = freeMaterial.color;
                    return;
                }
            }

            else
            {
                laser.SetPosition(i + 1, origin + offset);
                origin += offset;
            }
        }
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
    }

}

