using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioStoneLid : AudioObjectBase
    {
        public Rigidbody rigiBod;
        public bool isMoving;


        public override void Start()
        {
            audioSource.enabled = false;
            base.Start();

            rigiBod = GetComponent<Rigidbody>();

            StartCoroutine(warmUp());
        }

        private void Update()
        {
            StartCoroutine(checkMoving());

            if (isMoving)
            {
                audioSource.Play();
              //  Debug.Log("Moving");
            }
          

        }

        IEnumerator warmUp()
        {
            audioSource.enabled = false;
            yield return new WaitForSeconds(2.0f);
            audioSource.enabled = true;
        }

        IEnumerator checkMoving()
        {
            Vector3 startPos = transform.position;
            yield return new WaitForSecondsRealtime(0.01f);
            Vector3 finalPos = transform.position;

            if (startPos.x != finalPos.x || startPos.y != finalPos.y || startPos.z != finalPos.z)
            {
                isMoving = true;
                
            }

            else
            {
                isMoving = false;
            }
        }
    }
}
