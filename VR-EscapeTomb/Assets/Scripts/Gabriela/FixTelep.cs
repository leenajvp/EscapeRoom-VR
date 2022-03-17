using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixTelep : MonoBehaviour
{
    public static FixTelep Instance;
    public GameObject CameraXR;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private int laserSteps = 25;
    [SerializeField] private float laserSegmentDistance = 1f;
    [SerializeField] private float dropPerSegment = 1f;
    [SerializeField] private float teleportGap = 1f;

    private float teleportTime;
    private bool triggerValue;
    private bool fillPod;
    private float startTime;

    [SerializeField] private Material teleportMaterial;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private GameObject[] pods;

    void Awake()
    {
        laser.positionCount = laserSteps;
    }

    void Start()
    {
        Instance = this;
        pods = GameObject.FindGameObjectsWithTag("Teleport");
        laser = GetComponentInChildren<LineRenderer>();
        laser.material.color = normalMaterial.color;

        startTime = Time.time;
        teleportTime = 0f;
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
                            laser.material.color = teleportMaterial.color;

                            //pod color change
                            var selection = hit.transform;
                            // var selectionRender = selection.GetComponent<Renderer>();
                            var selectionRender = selection.GetComponent<Canvas>().GetComponentInChildren<Image>().GetComponentInChildren<Image>();

                            //Animator anim = selection.GetComponent<Animator>();

                            if (selectionRender != null)
                            {
                               // selectionRender.material.color = teleportMaterial.color;
                               // selectionRender.material.color = startColor;

                                fillPod = true;
                                {
                                    if (fillPod == true)
                                    {
                                        //selectionRender.fillAmount = 0;
                                        float speed = 1f;

                                        selectionRender.fillAmount += Mathf.MoveTowards(0f, 1f, speed * Time.deltaTime);

                                        //float t = (Time.deltaTime - startTime) * speed;

                                        //selectionRender.material.color = Color.Lerp(startColor, endColor, t);
                                    }
                                }

                                //anim.SetBool("isTelep", true);

                                /*
                                selectionRender.material.color = startColor;
                                fillPod = true;
                                {
                                    if (fillPod == true)
                                    {
                                        float speed = 1f;
                                        float t = (Time.time - startTime) * speed;
                                        selectionRender.material.color = Color.Lerp(startColor, endColor, t);
                                    }
                                }

                                */
                            }

                            teleportTime = teleportTime + 1f * Time.deltaTime;

                            if (teleportTime >= teleportGap)
                            {
                                CameraXR.transform.position = hit.transform.position;
                                // anim.SetBool("isTelep", false);
                                // selectionRender.material.color = normalMaterial.color;
                                RestartTeleportTime();
                            }
                            return;
                        }

                        else
                        {
                            laser.material.color = normalMaterial.color;

                            //pod restart color
                            foreach (GameObject pod in pods)
                            {
                                pod.GetComponent<Renderer>().material.color = normalMaterial.color;

                                fillPod = false;
                            }
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

            else
            {
                teleportTime = 0f;
                laser.gameObject.SetActive(false);
            }
        }
    }

    void RestartTeleportTime()
    {
        teleportTime = 0f;
    }


}

