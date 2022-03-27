using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ButtonPuzzle;

namespace Audio {
    public class AudioButtons : AudioObjectBase
    {
        public Buttons button;

        public bool soundPlayed;

        private float soundLength;

        public override void Start()
        {
            base.Start();

            soundLength = audioSource.clip.length;

             button = GetComponent<Buttons>();
        }

        private void checkPush()
        {
            if (button.pressed && !soundPlayed)
            {
                StartCoroutine(pushSound());

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            else if (button.reset)
            {
                soundPlayed = false;
            }
        }

        IEnumerator pushSound()
        {
                yield return new WaitForSeconds(soundLength);
                audioSource.Stop();
        }

    }
}
