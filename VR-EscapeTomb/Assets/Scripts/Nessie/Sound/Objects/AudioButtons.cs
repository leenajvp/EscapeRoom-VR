using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ButtonPuzzle;

namespace Audio {
    public class AudioButtons : AudioObjectBase
    {
        public Buttons button;

        public bool soundPlayed;

        public override void Start()
        {
            base.Start();

           button = GetComponent<Buttons>();
        }

        private void checkPush()
        {
            if (button.pressed)
            {
                if (!soundPlayed)
                {
                    audioSource.Play();
                    soundPlayed = true;
                }
            }

            else if (button.reset)
            {
                soundPlayed = false;
            }
        }

    }
}
