using System.Collections;
using UnityEngine;

namespace FixTeleportation
{
    public class TelPod : MonoBehaviour
    {
        public Material teleportMaterial;
        public Material normalMaterial;
        public Material busyMaterial;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Ray")
            {
                gameObject.GetComponent<Renderer>().material = teleportMaterial;
            }

            if (collider.gameObject.tag == "Player")
            {
                gameObject.GetComponent<Renderer>().material = busyMaterial;
            }
        }

        void OnTriggerExit(Collider collider)
        {
            gameObject.GetComponent<Renderer>().material = normalMaterial;
        }



    }


}

