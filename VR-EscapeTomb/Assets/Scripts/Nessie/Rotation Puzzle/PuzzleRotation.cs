using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace RotationPuzzle
{
    public class PuzzleRotation : MonoBehaviour
    {
        public bool PuzzleCompleted = false;
        public bool middleCompleted;
        public Rigidbody rigiBod;

        public Quaternion topTargetRota;

  public Quaternion middleTargetRota;

        /*  public HandController[] handCont;
          public RotationPuzzleHandles[] handles;


          private int i;
          private int c; */

        private void Start()
        {

            rigiBod = GetComponent<Rigidbody>();

            //INITAL PLAN - If the player is collding with the handle and grabbing the puzzle can be moved - else there can be no movement

          //  handCont = FindObjectsOfType<HandController>();


          /*  foreach (HandController handController in handCont)
            {
                handCont[i].GetComponent<HandController>();
            } */

          //  handles = FindObjectsOfType<RotationPuzzleHandles>();

         /*   foreach (RotationPuzzleHandles rotaHandles in handles)
            {
                handles[c].GetComponent<RotationPuzzleHandles>();
            } */



        }

        private void Update()
        {
           if (PuzzleCompleted)
            {
                rigiBod.isKinematic = true;
            }

           else
            {
                rigiBod.isKinematic = false;
            }

           if(transform.rotation == topTargetRota)
            {
                middleCompleted = true;
            }
   

 
        }
    }
}
