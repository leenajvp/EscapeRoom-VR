﻿using Climbing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Controllers
{

    public class ControllerHands : MonoBehaviour
    {
        //Sam - Controller/Grabbing
        public InputDeviceCharacteristics controllerCharacteristics;
        private InputDevice targetDevice;
        public Animator handAnimator;

        public bool isGrabbing;
        public static ControllerHands controllers;
        private GameObject heldObject;
        [SerializeField] ControllerHands controller;
        public float reachdistance = 0.1f, jointDistance = 0.05f;
        public LayerMask grabableLayer;
        public Transform palm;
        private Transform grabPoint, followTarget;
        private FixedJoint joint1, joint2;
        private Rigidbody body;
        public GameObject handsForCliming;

        //Nessie - Climbing
        [Header("Climbing - Scripts")]
        public ClimbingCollider climbCol;
        public Climber climber = null;
        public Vector3 Delta { private set; get; } = Vector3.zero;
        private Vector3 lastPosition = Vector3.zero;

        [Header("Climbing - Points")]
        public List<GameObject> contactPoints = new List<GameObject>();
        private GameObject currentPoint = null;
        public GameObject finalPoint;

        [Header("Climbing - Teleport")]
        public Transform teleportPoint;

        void Start()
        {
            controllers = this;
            GetInputDevice();
            followTarget = controller.gameObject.transform;
            body = GetComponent<Rigidbody>();
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.mass = 20f;
            body.maxAngularVelocity = 20f;

            //CLIMBING
            climbCol = GetComponentInChildren<ClimbingCollider>();
            lastPosition = transform.position;

        }

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

        void UpdateHandAnimation()
        {
            //Animation
            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {

                handAnimator.SetFloat("Grip", gripValue);
            }
            else
            {

                handAnimator.SetFloat("Grip", 0);

            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            lastPosition = transform.position;

            if (!targetDevice.isValid)
            {
                GetInputDevice();
            }
            else
            {
                UpdateHandAnimation();
            }
            // getting value from gript Button 
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gribStrenght);


            if (gribStrenght > 0.6f)
            {
                GrabPoint();


                if (!isGrabbing)
                {
                    grab();
                    isGrabbing = true;
                }


            }

            else if (isGrabbing && gribStrenght < 0.9f)
            {
                isGrabbing = false;
                Release();
            }

            else if (gribStrenght < 0.9f)
            {
                ReleasePoint();

            }
        }

        private void LateUpdate()
        {
            Delta = lastPosition - transform.position;
        }

        void grab()
        {
            // Sends a speare with coliders that check if there is any
            if (isGrabbing || heldObject) return;
            Collider[] grabableColliders = Physics.OverlapSphere(palm.position, reachdistance, grabableLayer);
            if (grabableColliders.Length < 1) return;

            var objectTograb = grabableColliders[0].transform.gameObject;
            var objectBody = objectTograb.GetComponent<Rigidbody>();

            if (objectBody != null)
            {
                heldObject = objectBody.gameObject;
            }
            else
            {
                objectBody = objectTograb.GetComponentInParent<Rigidbody>();
                if (objectBody != null)
                {
                    heldObject = objectBody.gameObject;
                }
                else
                {
                    return;
                }
            }
            StartCoroutine(GrabObject(grabableColliders[0], objectBody));
        }
        private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
        {
            // isGrabbing = true;
            // Grab point
            grabPoint = new GameObject().transform;
            grabPoint.position = collider.ClosestPoint(palm.position);
            grabPoint.parent = heldObject.transform;
            // Move hand to grab object
            followTarget = grabPoint;
            while (grabPoint != null && Vector3.Distance(grabPoint.position, palm.position) > jointDistance && isGrabbing)
            {
                yield return new WaitForEndOfFrame();
            }
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            targetBody.velocity = Vector3.zero;
            targetBody.angularVelocity = Vector3.zero;

            targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            targetBody.interpolation = RigidbodyInterpolation.Interpolate;
            // Attach joints 
            joint1 = gameObject.AddComponent<FixedJoint>();
            joint1.connectedBody = targetBody;
            joint1.breakForce = float.PositiveInfinity;
            joint1.breakTorque = float.PositiveInfinity;

            joint1.connectedMassScale = 1;
            joint1.massScale = 1;
            joint1.enableCollision = false;
            joint1.enablePreprocessing = false;

            joint2 = heldObject.AddComponent<FixedJoint>();
            joint2.connectedBody = body;
            joint2.breakForce = float.PositiveInfinity;
            joint2.breakTorque = float.PositiveInfinity;

            joint2.connectedMassScale = 1;
            joint2.massScale = 1;
            joint2.enableCollision = false;
            joint2.enablePreprocessing = false;

            followTarget = controller.transform.gameObject.transform;
        }

        public void Release()
        {
            if (joint1 != null) Destroy(joint1);
            if (joint2 != null) Destroy(joint2);
            if (grabPoint != null) Destroy(grabPoint.gameObject);

            if (heldObject != null)
            {
                var targetBody = heldObject.GetComponent<Rigidbody>();
                targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                targetBody.interpolation = RigidbodyInterpolation.None;
                heldObject = null;
            }

            // isGrabbing = false;
            followTarget = controller.gameObject.transform;
        }



        //CLIMBING
        private void GrabPoint()
        {
            currentPoint = Utility.GetNearest(transform.position, contactPoints);

            if (currentPoint)
            {
                climber.SetHand(this);
                Debug.Log("grabbing point");

            }

            if (currentPoint == finalPoint)
            {
                climber.transform.position = teleportPoint.transform.position;
                climber.ClearHand();
            }
        }

        public void ReleasePoint()
        {
            if (currentPoint)
            {
                climber.ClearHand();

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
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 17)
            {

                handsForCliming.SetActive(true);
            }
        }

    }
}

