using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class AudioPapyrus : AudioObjectBase
    {
        public GameManager gameManager;
        


        public override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            
        }

        private void CheckCollection()
        {
            if (gameManager.papirusTeleport)
            {
                audioSource.Play();
            }
        }
    }
}
