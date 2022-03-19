using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if((climbableLayer.value & (1 << other.transform.gameObject.layer)) > 0 )
            {
                Debug.Log("TOUCHING");
                gameObject.GetComponentInParent<Hand>().AddPoint(other.gameObject);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.GetComponentInParent<Hand>().RemovePoint(other.gameObject);
        }
    }
}
