﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixTelep : MonoBehaviour
{
    public static FixTelep Instance;
    public GameObject CameraXR;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private LineRenderer laserCor;
    [SerializeField] private int laserSteps = 25;
    [SerializeField] private float laserSegmentDistance = 1f;
    [SerializeField] private float dropPerSegment = 1f;
    [SerializeField] private float teleportGap = 1f;
    [SerializeField] private Material teleportMaterial;
    [SerializeField] private Material normalMaterial;

    private float teleportTime;
    private bool triggerValue;
    private bool fillPod;
    private float startTime;

    public GameObject[] pods;
    public GameObject[] shaders;
    void Awake()
    {
        laser.positionCount = laserSteps;
    }

    void Start()
    {
        Instance = this;
        pods = GameObject.FindGameObjectsWithTag("Teleport");
        //shaders = GameObject.FindGameObjectsWithTag("Shader");
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
                RaycastHit rayhit;
                Vector3 origin = transform.position;
                laser.SetPosition(0, origin);
                for (int i = 0; i < laserSteps - 1; i++)
                {
                    Vector3 offset = (transform.forward + (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;
                    if (Physics.Raycast(origin, offset, out rayhit, laserSegmentDistance))
                    {
                        for (int j = i + 1; j < laser.positionCount; j++)
                        {
                            laser.SetPosition(j, rayhit.point);
                        }
                        if (rayhit.transform.gameObject.tag == "Teleport")
                        {
                            laser.material.color = teleportMaterial.color;
                            StartCoroutine(TeleportCorutine());
                        }
                        else
                        {
                            laser.material.color = normalMaterial.color;
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
                laser.SetPosition(0, transform.position);
                //pod restart color
                foreach (GameObject pod in pods)
                {
                    fillPod = false;
                }
                foreach (GameObject shader in shaders)
                {
                    shader.SetActive(false);
                }
                teleportTime = 0f;
                laser.gameObject.SetActive(false);
                return;
            }
        }
    }
    void RestartTeleportTime()
    {
        teleportTime = 0f;
    }
    public IEnumerator TeleportCorutine()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        laserCor.SetPosition(0, origin);
        for (int i = 0; i < laserSteps - 1; i++)
        {
            Vector3 offset = (transform.forward + (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;

            if (Physics.Raycast(origin, offset, out hit, laserSegmentDistance))
            {
                for (int j = i + 1; j < laser.positionCount; j++)
                {
                    laserCor.SetPosition(j, hit.point);
                }

                if (hit.transform.gameObject.tag == "Teleport" && triggerValue)
                {
                    //pod color change
                    var selection = hit.transform;
                    var selectionRender = selection.Find("Shader").gameObject;
                    var fillRender = selection.Find("FillUP").gameObject;
                    float speed = 2f;
                    if (selection != null)
                    {
                        fillPod = true;
                        {
                            if (fillPod)
                            {
                                fillRender.gameObject.GetComponent<Image>().fillAmount += Mathf.MoveTowards(0, 1, speed * Time.deltaTime);
                                selectionRender.SetActive(true);
                            }
                        }
                    }
                    yield return new WaitForSeconds(2);
                    teleportTime = teleportTime + 1f * Time.deltaTime;
                    if (teleportTime >= teleportGap)
                    {
                        CameraXR.transform.position = hit.transform.position ;
                        RestartTeleportTime();
                    }
                    //laserCor.gameObject.SetActive(false);

                    yield return new WaitForSeconds(1);

                   // laserCor.gameObject.SetActive(false);
                    triggerValue = false;
                    laserCor.SetPosition(0, transform.position);
                    fillRender.gameObject.GetComponent<Image>().fillAmount -= Mathf.MoveTowards(1, 0, speed * Time.deltaTime);
                    selectionRender.SetActive(false);
                }
            }
            else
            {
                laserCor.SetPosition(i + 1, origin + offset);
                origin += offset;
            }
        }
    }
}

/*
 using System.Collections;
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
    [SerializeField] private Material teleportMaterial;
    [SerializeField] private Material normalMaterial;

    private float teleportTime;
    private bool triggerValue;
    private bool fillPod;
    private float startTime;

    public GameObject[] pods;
    public GameObject[] shaders;
    void Awake()
    {
        laser.positionCount = laserSteps;
    }

    void Start()
    {
        Instance = this;
        pods = GameObject.FindGameObjectsWithTag("Teleport");
        //shaders = GameObject.FindGameObjectsWithTag("Shader");
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
                StartCoroutine(TeleportCorutine());
                RaycastHit rayhit;
                Vector3 origin = transform.position;
                laser.SetPosition(0, origin);
                for (int i = 0; i < laserSteps - 1; i++)
                {
                    Vector3 offset = (transform.forward + (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;
                    if (Physics.Raycast(origin, offset, out rayhit, laserSegmentDistance))
                    {
                        for (int j = i + 1; j < laser.positionCount; j++)
                        {
                            laser.SetPosition(j, rayhit.point);
                        }
                        if (rayhit.transform.gameObject.tag == "Teleport")
                        {
                            laser.material.color = teleportMaterial.color;
                        }
                        else
                        {
                            laser.material.color = normalMaterial.color;
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
                //pod restart color
                foreach (GameObject pod in pods)
                {
                    fillPod = false;
                }
                foreach (GameObject shader in shaders)
                {
                    shader.SetActive(false);
                }
                teleportTime = 0f;
                laser.gameObject.SetActive(false);
                return;
            }
        }
    }
    void RestartTeleportTime()
    {
        teleportTime = 0f;
    }
    public IEnumerator TeleportCorutine()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;

        if (Physics.Raycast(origin, transform.forward, out hit, laserSegmentDistance))
        {
            if (hit.transform.gameObject.tag == "Teleport" && triggerValue)
            {
                //pod color change
                var selection = hit.transform;
                var selectionRender = selection.Find("Shader").gameObject;
                var fillRender = selection.Find("FillUP").gameObject;
                float speed = 2f;
                if (selection != null)
                {
                    fillRender.gameObject.GetComponent<Image>().fillAmount += Mathf.MoveTowards(0, 1, speed * Time.deltaTime);
                    fillRender.gameObject.GetComponent<Image>().fillAmount = 1;
                    selectionRender.SetActive(true);
                    yield return new WaitForSeconds(1);
                    fillRender.gameObject.GetComponent<Image>().fillAmount -= Mathf.MoveTowards(1, 0, speed * Time.deltaTime);
                    Debug.Log("FillAmmount" + fillRender.gameObject.GetComponent<Image>().fillAmount);
                    selectionRender.SetActive(false);
                }
                teleportTime = teleportTime + 1f * Time.deltaTime;
                if (teleportTime >= teleportGap)
                {
                    CameraXR.transform.position = hit.transform.position;

                    RestartTeleportTime();
                }
            }
            else
            {
                fillPod = false;
            }
        }
    }
}

*/


