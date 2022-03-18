using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Climbing {

    public class ClimbingCollider : MonoBehaviour
    {
        private Hand hand;

        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TOUCHING");
            gameObject.GetComponentInParent<Hand>().AddPoint(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.GetComponentInParent<Hand>().RemovePoint(other.gameObject);
        }
    }
}
