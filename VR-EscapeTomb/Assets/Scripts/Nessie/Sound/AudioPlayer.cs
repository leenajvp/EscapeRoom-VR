using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    public class AudioPlayer : AudioObjectBase
    {
        [Header("Other Classes")]
        public FixTelep teleporting;
        public AudioClip SandStep;



        private void FixedUpdate()
        {
            checkTeleport();
        }
       

        private void checkTeleport()
        {
          

        if (teleporting.isTeleporting && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("TELEPORT");
            }

        if (!teleporting.isTeleporting)
            {
                audioSource.Stop();
            }



        }

         
    }
}
