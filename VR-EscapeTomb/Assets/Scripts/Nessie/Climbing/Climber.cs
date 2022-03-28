using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Climbing
{
    public class Climber : MonoBehaviour
    {
        public float gravity = 45.0f;
        public float sensitivity = 45.0f;

       
        public ControllerHands currentHand = null;
        private CharacterController controller = null;

        private void Awake()
        {

            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            calcMovement();
        }

        //Calculation for the players movement as to whether they are climbing or going to be falling.
        private void calcMovement()
        {
            Vector3 movement = Vector3.zero;

            if (currentHand)
            {
                movement += currentHand.Delta * sensitivity;

            }

            if (movement == Vector3.zero)
            {
                movement.y -= gravity * Time.deltaTime;
            }


            //Move the player controller
            controller.Move(movement * Time.deltaTime);
        }

        //Checking what current hand is being used to climb
        public void SetHand( ControllerHands hand)
        {
            if (currentHand)
            {
                currentHand.ReleasePoint();
                currentHand = null;
            }

            currentHand = hand;
        }

        //return the hand to null
        public void ClearHand()
        {
            currentHand = null;
        }

       

    }
}
