using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]

    public class AudioBallCollision : AudioObjectBase
    {
        [Header("Layer Masks")]
        public LayerMask floorLayer;
        public LayerMask handslayer;




        public override void Start()
        {
            base.Start();
       
        }

       



        private void OnCollisionEnter(Collision collision)
        {
            if ((handslayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                audioSource.enabled = true;
                Debug.Log("Audio is Active");
            }

            if ((floorLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Debug.Log("Ball Landing");
                audioSource.Play();
            }


        }
    }
}
