using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioStoneLid : AudioObjectBase
    {
        public Rigidbody rigiBod;
        public bool isMoving;
        public AudioClip thud;

        public override void Start()
        {
            base.Start();
            audioSource.enabled = false;
            rigiBod = GetComponent<Rigidbody>();
            StartCoroutine(warmUp());
        }

        private void OnCollisionStay(Collision collision)
        {
            if (rigiBod.velocity.x != 0 || rigiBod.velocity.z != 0)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            else
                audioSource.Stop();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (rigiBod.velocity.x != 0 || rigiBod.velocity.z != 0)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            else
                audioSource.Stop();

            //COULD HAVE A THUD SOUND HERE
            if (gameObject.transform.childCount == 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(thud);
                }
            }

        }

        private void Update()
        {
            StartCoroutine(checkMoving());

            if (gameObject.transform.childCount == 0)
            {
                audioSource.Stop();
            }

            if (isMoving)
            {
                // audioSource.Play();
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
