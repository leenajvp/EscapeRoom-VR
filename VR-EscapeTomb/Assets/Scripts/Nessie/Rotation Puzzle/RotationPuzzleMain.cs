using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotationPuzzle {
    public class RotationPuzzleMain : MonoBehaviour
    {
        [Header("Completion Bools")]

        public bool isPuzzleCompleted;
        public bool isMiddlePartCompleted;
        public bool isTopPartCompleted;

        public Rigidbody[] rigiBods;
        int i;

        private void Start()
        {
            rigiBods = GetComponentsInChildren<Rigidbody>();

         
        }

        private void Update()
        {

            CheckPuzzleCompletion();
     
        }

      

        public void CheckPuzzleCompletion()
        {

            if (isMiddlePartCompleted && isTopPartCompleted)
            {
                isPuzzleCompleted = true;
                rigiBods[0].isKinematic = true;
                rigiBods[1].isKinematic = true;

            }

            else
            {
                isPuzzleCompleted = false;
                rigiBods[0].isKinematic = false;
                rigiBods[1].isKinematic = false;

            }
        } 
    }
}
