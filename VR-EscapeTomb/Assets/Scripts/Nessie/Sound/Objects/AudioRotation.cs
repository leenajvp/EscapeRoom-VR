using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class AudioRotation : AudioObjectBase
    {
        Rigidbody rigiBod;
       public float finalsPos;
        public float startPos;

        public bool isRotating;
        public override void Start()
        {
            base.Start();

            rigiBod = GetComponent<Rigidbody>();

            startPos = transform.eulerAngles.y;
            finalsPos = startPos;

            StartCoroutine(warmUp());
        }
       
        private void Update()
        {

            StartCoroutine(checkPos());
           

            if (startPos != finalsPos)
            {
                isRotating = true;
                audioSource.Play();
                
            }

            else if (startPos == finalsPos)
            {
                isRotating = false;
                audioSource.Stop();
            }
        }

        IEnumerator warmUp()
        {
            audioSource.enabled = false;
            yield return new WaitForSeconds(2.0f);
            audioSource.enabled = true;
        }

        IEnumerator checkPos()
        {
            startPos = transform.eulerAngles.y;
            yield return new WaitForSeconds(0.5f);
            finalsPos = transform.eulerAngles.y;
          

          
        }
    }
}
