using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Audio {
    public class AudioPlayer : MonoBehaviour
    {
        [Header("Other Classes")]
        public FixTelep teleporting;

        [Header("Audio")]
        public AudioSource playerAudioSource;
        public AudioClip teleportSelect;
        public AudioClip sandStep;

        private void Start()
        {

        }

        private void Update()
        {
            checkTeleport();
        }
        void CheckGrab()
        {


        }

        private void checkTeleport()
        {
          

                if (teleporting.isTeleporting && !playerAudioSource.isPlaying)
                {
                    playerAudioSource.PlayOneShot(sandStep);
                }


            


        }
    }
}
