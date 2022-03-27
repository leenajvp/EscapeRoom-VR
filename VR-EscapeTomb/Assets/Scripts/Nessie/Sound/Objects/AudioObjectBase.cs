using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
   
    public class AudioObjectBase : MonoBehaviour
    {
        [Header("Audio Source")]
        public AudioSource audioSource;

        public virtual void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
}
