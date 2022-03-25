using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    public class AudioPlayer : MonoBehaviour
    {
        public ControllerHands leftController;
        public ControllerHands rightController;

        private void Start()
        {
           
        }
        void CheckGrab()
        {
           if (leftController.isGrabbing || rightController.isGrabbing)
            {
                Debug.Log("GRABING");
            }

        }


    }
}
