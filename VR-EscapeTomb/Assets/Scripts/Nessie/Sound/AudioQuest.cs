using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotationPuzzle;
using ButtonPuzzle;

namespace Audio {

    public class AudioQuest : AudioObjectBase
    {
    
        [Header("Audio")]
        public AudioSource PlayerAudioSource;
    

        [Header("Quests")]
        public QuestManager questMan;
        public ThrowPuzzle throwPuzzle;
        public RotationPuzzleMain rotaPuzzle;
        public WheelPuzzle wheelPuzzle;
        public ButtonPuzzleManager buttonPuzzle;
        public Quest4 quest4;

      

       [Header("Quest Sound Bools")] //Ensuring the sound only plays once
        [SerializeField] private bool Quest1Sound = false;
        [SerializeField] private bool throwPuzzleSound = false;
        [SerializeField] private bool rotaPuzzleSound = false;
        [SerializeField] private bool wheelPuzzleSound = false;
        [SerializeField] private bool buttonPuzzleSound = false;
        [SerializeField] private bool quest4Sound = false;

        public override void Start()
        {
           
        }

        private void Update()
        {
            checkQuestCompleteion();
        }
        void checkQuestCompleteion()
        {
            if (questMan.quest1Complete && !Quest1Sound)
            {
                PlayAudio();
                Quest1Sound = true;
                
            }

            if (throwPuzzle.completed && !throwPuzzleSound)
            {
                PlayAudio();
                throwPuzzleSound = true;
            }

            if (rotaPuzzle.isPuzzleCompleted && !rotaPuzzleSound)
            {
                PlayAudio();
                rotaPuzzleSound = true;
                
            }

            if (buttonPuzzle.completed && !buttonPuzzleSound)
            {
                PlayAudio();
                buttonPuzzleSound = true;
            }

            if (questMan.quest4Complete && !quest4Sound)
            {
                PlayAudio();
                quest4Sound = true;
            }
        }

        private void PlayAudio()
        {
            PlayerAudioSource.Play();
        }
     

    }


}
