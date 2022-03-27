using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    public class AudioPlayer : AudioObjectBase
    {
        [Header("Other Classes")]
        public FixTelep teleporting;

       

        private void Update()
        {
            checkTeleport();
        }

        private void checkTeleport()
        {
          

        if (teleporting.isTeleporting && !audioSource.isPlaying)
            {
                audioSource.Play();
            }


            


        }
    }
}
