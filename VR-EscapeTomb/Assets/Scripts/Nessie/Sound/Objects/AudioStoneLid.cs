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
            base.Start();

            rigiBod = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            StartCoroutine(checkMoving());

            if (isMoving)
            {
                audioSource.Play();
                Debug.Log("Moving");
            }
          

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
