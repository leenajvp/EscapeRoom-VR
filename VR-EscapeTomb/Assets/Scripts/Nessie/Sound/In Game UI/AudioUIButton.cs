using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;


namespace Audio {
    public class AudioUIButton : AudioObjectBase
    {
        public InGameUI inGameUI;


        public override void Start()
        {
            base.Start();
            inGameUI = GetComponent<InGameUI>();
        }

        private void Update()
        {
            checkPush();
        }


        void checkPush()
        {

            if (inGameUI.reset)
            {
                
                audioSource.Play();
            }

        }
    }
}
