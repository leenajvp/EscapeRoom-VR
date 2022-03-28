using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Climbing {

    public class ClimbingCollider : MonoBehaviour
    {
        private Hand hand;
        public LayerMask climbableLayer;
        

        private void Start()
        {

        }

        //Shpere collideres on the player hands so the hand isn't tiggering a climb point several times due to a mesh collider

        private void OnTriggerEnter(Collider other)
        {

      

            //Checking the object is on the correct layer before adding it to the climb point list
            if((climbableLayer.value & (1 << other.transform.gameObject.layer)) > 0 )
            {
              
                gameObject.GetComponentInParent<ControllerHands>().AddPoint(other.gameObject);
                
            }

          

        }

        private void OnTriggerExit(Collider other)
        {
        
            gameObject.GetComponentInParent<ControllerHands>().RemovePoint(other.gameObject);
           
        }
    }
}
