using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSpikes : AudioObjectBase
    {
        public ThrowPuzzle throwingPuzzle;
        public bool hasPlayed;

        public override void Start()
        {
            base.Start();
            hasPlayed = false;
        }

        private void Update()
        {
            if (throwingPuzzle.completed && !hasPlayed)
            {
                audioSource.Play();
            }
        }

        
    }
}
