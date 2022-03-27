using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class AudioPapyrus : AudioObjectBase
    {
        public GameObject papriusPickUp;
        public bool hasPlayed;
       

        public override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            CheckCollection();
        }

        private void CheckCollection()
        {
            if (papriusPickUp == null && !hasPlayed)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}
