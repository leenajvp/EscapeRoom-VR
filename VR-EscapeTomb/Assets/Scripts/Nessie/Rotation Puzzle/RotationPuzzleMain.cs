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

           /* foreach(Rigidbody rigibod in rigiBods)
            {
                rigiBods[i].GetComponent<Rigidbody>();
            } */
        }

        private void Update()
        {

            CheckPuzzleCompletion();

            if(isPuzzleCompleted)
            {
                rigiBods[0].isKinematic = true;
                rigiBods[1].isKinematic = true;
            }
     
        }

      

        public void CheckPuzzleCompletion()
        {

            if (isMiddlePartCompleted && isTopPartCompleted)
            {
                isPuzzleCompleted = true;
            
            }

            else
            {
                isPuzzleCompleted = false;
               
            }
        } 
    }
}
