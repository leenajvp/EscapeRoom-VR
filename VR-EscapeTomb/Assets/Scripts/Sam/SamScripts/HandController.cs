using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Controllers
{
    public class HandController : MonoBehaviour
    {
        public InputDeviceCharacteristics controllerCharacteristics;
        private InputDevice targetDevice;
        public Animator handAnimator;
        
        public bool isGrabbing;
        private bool triggerValue;

        private GameObject heldObject;
        [SerializeField] HandController controller;
        public float reachdistance = 0.1f, jointDistance = 0.05f;
        public LayerMask grabableLayer;
        public Transform palm;
        private Transform grabPoint, followTarget;
        private FixedJoint joint1, joint2;
        private Rigidbody body;
        void Start()
        {
            GetInputDevice();
            followTarget = controller.gameObject.transform;
            body = GetComponent<Rigidbody>();
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.mass = 20f;
            body.maxAngularVelocity = 20f;

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
                if (!isGrabbing)
                {
                    grab();
                    isGrabbing = true;
                }
            }

            else if (isGrabbing && gribStrenght < 0.8f)
            {
                isGrabbing = false;
                Release();
            }
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

          //  isGrabbing = false;
            followTarget = controller.gameObject.transform;
        }
       
    }
    /*
     * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Controllers
{
    public class HandController : MonoBehaviour
    {
        public InputDeviceCharacteristics controllerCharacteristics;
        private InputDevice targetDevice;
        public Animator handAnimator;
        
        public bool isGrabbing;
        private bool triggerValue;

        private GameObject heldObject;
        [SerializeField] HandController controller;
        public float reachdistance = 0.1f, jointDistance = 0.05f;
        public LayerMask grabableLayer;
        public Transform palm;
        private Transform grabPoint, followTarget;
        private FixedJoint joint1, joint2;
        private Rigidbody body;
        void Start()
        {
            GetInputDevice();
            followTarget = controller.gameObject.transform;
            body = GetComponent<Rigidbody>();
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.mass = 20f;
            body.maxAngularVelocity = 20f;

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

            if (!targetDevice.isValid)
            {
                GetInputDevice();
            }
            else
            {
                UpdateHandAnimation();
            }
            // getting value from gript Button 
            if (targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
            {
                grab();
            }
            else
            {
                Release();
            }
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
            isGrabbing = true;
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

            isGrabbing = false;
            followTarget = controller.gameObject.transform;
        }
       
    }
    
    
}













     */

}












