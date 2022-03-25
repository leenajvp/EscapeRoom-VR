using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotationPuzzle;

namespace Audio{

    public class AudioPuzzles : MonoBehaviour
    {
        [Header("Audio Sources - Puzzles")]
        public AudioSource rotationPuzzleSource;
        public AudioSource wheelPuzzleSource;
        public AudioSource spikesSource;

        [Header("Audio Clips")]
        public AudioClip rotation;
        public AudioClip spikesLower;

        [Header("Audio Bools")]
        public bool spikesPlayed;
        public bool pap1Collected;
        public bool pap2Collected;
        public bool pap3Collected;

        float secondsToPlay = 0f;

        [Header("Throwing Puzzle")]
        public ThrowPuzzle throwingPuzzle;

        [Header("Rotation Puzzle")]
        // public RotaionPuzzleRings topRing;
        public RotaionPuzzleRings midRing;
        public GameObject midRing1;
        public Rigidbody TRingRigid;
        public float TopRinglastPos;

        [Header("Wheel Puzzle")]
        public Rigidbody wheel1;
        public Rigidbody wheel2;
        public Rigidbody wheel3;




        private void Start()
        {
            //TopRinglastPos = TRingRigid.transform.rotation.eulerAngles.y;
            // rotationPuzzleSource.enabled = false;
        }
        private void FixedUpdate()
        {
            SpikeCheck();
        }

        /*  private void checkTopRing()
          {

              if (TRingRigid.transform.rotation.eulerAngles.y != TopRinglastPos)
              {
                  if (secondsToPlay == 0.0f)
                  {
                      StartCoroutine(PlayEffectRotation());
                  }

                  else if (secondsToPlay < 0.1f)
                  {
                      secondsToPlay = 0.1f;
                  }


              }

          }

          IEnumerator PlayEffectRotation()
          {
              secondsToPlay = 0.1f;
              rotationPuzzleSource.PlayOneShot(rotation);

              while (secondsToPlay > 0.0f)
              {
                  yield return null;
                  secondsToPlay -= Time.deltaTime;
                  Debug.Log("Playing Sound");
              }

              secondsToPlay = 0.0f;
              rotationPuzzleSource.Stop();
          } */

        void SpikeCheck()
        {
            if (throwingPuzzle.completed && !spikesPlayed)
            {
                Debug.Log("SPIKES BYE");
                spikesSource.PlayOneShot(spikesLower);
                spikesPlayed = true;
            }
        }



    }
}
