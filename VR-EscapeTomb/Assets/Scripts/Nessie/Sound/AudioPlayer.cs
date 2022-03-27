using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    public class AudioPlayer : AudioObjectBase
    {
        [Header("Other Classes")]
        public FixTelep teleporting;




        private void FixedUpdate()
        {
            checkTeleport();
        }


        private void checkTeleport()
        {


            if (teleporting.isTeleporting && !audioSource.isPlaying)
            {
                audioSource.Play();

                StartCoroutine(teleportTimer());

            }

        }

        IEnumerator teleportTimer()
        {
            yield return new WaitForSeconds(0.2f);
            teleporting.isTeleporting = false;
        }
    }
}
