using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPap : MonoBehaviour
    {
        public GameManager gameManager;

        public AudioSource PapAudioSource;

        public bool PapSound;

        private void Update()
        {
            if (gameManager.papirusTeleport && !PapSound)
            {
                PapAudioSource.Play();
            }
        }
    }
}
