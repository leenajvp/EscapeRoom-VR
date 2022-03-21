using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Climbing;


public class Hand : MonoBehaviour
{
    [Header("Controllers")]
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    public bool triggerValue;
  //  public LayerMask climbingLayer;

    [Header("Animation")]
    public Animator handAnimator;

    [Header("Climber")]
    public Climber climber = null;
    public Vector3 Delta { private set; get; } = Vector3.zero;
    private Vector3 lastPosition = Vector3.zero;
    private GameObject currentPoint = null;
    public GameObject finalPoint;
    public Transform teleportPoint;
    public List<GameObject> contactPoints = new List<GameObject>();

    void GetInputDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // checking for list of devices, device must be chose from the list 
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    private void Awake()
    {

    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    

    private void Update()
    {
        //trigger button - replace float with boolean value

        if(targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
        {
          
             GrabPoint();
        }

        else
        {
           // Debug.Log("RELEASED"); 
            ReleasePoint();
            
        }


    }

    
    private void FixedUpdate()
    {
        lastPosition = transform.position;

        if (!targetDevice.isValid)
        {
            GetInputDevice();
        }

        else
        {
            UpdateHandAnim();
        }  
    }

    private void LateUpdate()
    {
        Delta = lastPosition - transform.position;
    
    }


    //Updating the Animation for the hands
    private void UpdateHandAnim()
    {
        //If the player is holding the grip - set the animatiom - if not no animation
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }

        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    private void GrabPoint()
    {
        currentPoint = Utility.GetNearest(transform.position, contactPoints);

        if (currentPoint)
        {
          //  climber.SetHand(this);
        }

        if(currentPoint == finalPoint)
        {
            climber.transform.position = teleportPoint.transform.position;
        }
    }

    public void ReleasePoint()
    {
        if (currentPoint)
        {
         //  climber.ClearHand(this);
            
        }

        currentPoint = null;
    }

 /*   private void OnTriggerEnter(Collider other)
    {
        AddPoint(other.gameObject);
    }*/
    

   public void AddPoint(GameObject newObject)
    {
        //Will be replaced with checking for layer/layer mask - Tag is just for testing 
        /* if (newObject.CompareTag("ClimbPoint"))
         {
             contactPoints.Add(newObject);
         }*/

        contactPoints.Add(newObject);
       // Debug.Log("Point Added");
    }

  /* private void OnTriggerExit(Collider other)
    {
        RemovePoint(other.gameObject);
    } */

    public void RemovePoint(GameObject newObject)
    {
      //  if (newObject.CompareTag("ClimbPoint"))
        //{
        contactPoints.Remove(newObject);
       // Debug.Log("Point Removed");
        //}
    }



}

