using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotationPuzzle;
using ButtonPuzzle;

namespace Audio {

    public class AudioQuest : MonoBehaviour
    {
    
        [Header("Audio")]
        public AudioSource PlayerAudioSource;
        public AudioClip correctPuzzle;

        [Header("Quests")]
        public QuestManager questMan;
        public ThrowPuzzle throwPuzzle;
        public RotationPuzzleMain rotaPuzzle;
        public WheelPuzzle wheelPuzzle;
        public ButtonPuzzleManager buttonPuzzle;
        public Quest4 quest4;

        [Header("Quest Sound Bools")]
        [SerializeField] private bool Quest1Sound = false;
        [SerializeField] private bool throwPuzzleSound = false;
        [SerializeField] private bool rotaPuzzleSound = false;
        [SerializeField] private bool wheelPuzzleSound = false;
        [SerializeField] private bool buttonPuzzleSound = false;
        [SerializeField] private bool quest4Sound = false;

        private void Update()
        {
            checkQuestCompleteion();
        }
        void checkQuestCompleteion()
        {
            if (questMan.quest1Complete && !Quest1Sound)
            {
                PlayerAudioSource.PlayOneShot(correctPuzzle);
                Quest1Sound = true;
                Debug.Log("PLAYING SOUND");
            }

            if (throwPuzzle.completed && !throwPuzzleSound)
            {
                PlayerAudioSource.PlayOneShot(correctPuzzle);
                throwPuzzleSound = true;
            }

            if (rotaPuzzle.isPuzzleCompleted && !rotaPuzzleSound)
            {
                PlayerAudioSource.PlayOneShot(correctPuzzle);
                rotaPuzzleSound = true;
            }

            //INSERT WHEEL PUZZLE?

            if (buttonPuzzle.completed && !buttonPuzzleSound)
            {
                PlayerAudioSource.PlayOneShot(correctPuzzle);
                buttonPuzzleSound = true;
            }

            /* if (quest4.part1 && quest4.part2 && quest4.part3 && !quest4Sound)
             {
                 PlayerAudioSource.PlayOneShot(correctPuzzle);
                 Debug.Log("Q4 Comp");
                 quest4Sound = true;
             } */

        }
    }

}
