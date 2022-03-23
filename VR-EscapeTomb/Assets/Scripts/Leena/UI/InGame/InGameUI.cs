using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{

    public class InGameUI : MonoBehaviour
    {
        public enum Functions { GetClue, QuitGame };
        [Header("Button Function")]
        public Functions function;

        [Header("Button Settings")]
        [SerializeField] private GameObject trigger;
        [Tooltip("Speed that the button returns to default position")]
        [SerializeField] float resetSpeed = 2.0f;

        [Header("Quit Game Settings")]
        [SerializeField] private GameObject exitDoor;
        [SerializeField] private GameObject exitTeleportation;
        [SerializeField] private float secondsToCancel = 30.0f;

        [Header("Statue puzzle and hide doors")]
        [SerializeField] private QuestManager statuePuzzle;
        [SerializeField] private List<Hints> quest1Clues = new List<Hints>();

        [Header("Throw puzzle and clue object")]
        [SerializeField] private ThrowPuzzle throwPuzzle;
        [SerializeField] private List<Hints> quest2Clues = new List<Hints>();

        [Header("Button puzzle and clue object")]
        [SerializeField] private ButtonPuzzle.ButtonPuzzleManager buttonPuzzle;
        [SerializeField] private Hints buttonPuzzleClue;

        [Header("Rotation puzzle and clue objects")]
        [SerializeField] private RotationPuzzle.RotationPuzzleMain rotPuzzle;
        [SerializeField] private List<Hints> quest4Clues = new List<Hints>();

        [Header("Climbing steps")]
        [SerializeField] private GameManager finalPuzzle;
        [SerializeField] private List<Hints> quest5Clues = new List<Hints>();

        private bool quest5;
        private bool pressed = false, reset = false;
        private float timer = 0;
        private Vector3 defaultPos;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            reset = false;
            defaultPos = transform.position;
            exitTeleportation.SetActive(false);
        }

        void Update()
        {
            if (reset)
            {
                rb.isKinematic = true;
                timer += Time.deltaTime * 0.5f;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, defaultPos.x, Time.deltaTime * resetSpeed), transform.position.y, Mathf.Lerp(transform.position.z, defaultPos.z, Time.deltaTime * resetSpeed));

                if (timer >= 0.5f)
                {
                    rb.isKinematic = false;
                    reset = false;
                    timer = 0;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == trigger && !pressed)
            {
                rb.isKinematic = true;
                pressed = true;

                if (function == Functions.GetClue)
                {
                    GetClue();
                    StartCoroutine(GlowTimer());
                    reset = true;
                }

                else
                {
                    ExitGame();
                    StartCoroutine(CancelExit());
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject == trigger && pressed)
            {
                pressed = false;
            }
        }

        private void ExitGame()
        {
            exitDoor.GetComponent<SlidingDoor>().MoveDoor();
            exitTeleportation.SetActive(true);
            rb.isKinematic = true;
        }

        private void GetClue()
        {
            //Check quest 1 item puzzle
            if (!statuePuzzle.quest1Complete)
            {
                foreach (Hints hide in quest1Clues)
                {
                    if (!hide.gameObject.GetComponent<ObjectHides>().open)
                    {
                        hide.playerNeedsHint = true;
                        break;
                    }
                }

                return;
            }

            //Check quest 2 throw puzzle
            if (!throwPuzzle.completed)
            {
                quest2Clues.ForEach(hint => hint.playerNeedsHint = true);

                return;
            }

            //Check quest 3 button puzzle
            if (!buttonPuzzle.completed)
            {
                buttonPuzzleClue.playerNeedsHint = true;
                return;
            }

            //Check quest 4 rotation puzzle
            if (!rotPuzzle.isPuzzleCompleted)
            {
                quest4Clues.ForEach(hint => hint.playerNeedsHint = true);
                return;
            }

            //Check quest 5 (final) climbing puzzle

            if (!finalPuzzle.gameComplete)
            {
                quest4Clues.ForEach(hint => hint.playerNeedsHint = true);
            }
            
        }

        private IEnumerator GlowTimer()
        {
            yield return new WaitForSeconds(60);

            quest1Clues.ForEach(hint => hint.playerNeedsHint = false);
            quest2Clues.ForEach(hint => hint.playerNeedsHint = false);
            buttonPuzzleClue.playerNeedsHint = true;
            quest4Clues.ForEach(hint => hint.playerNeedsHint = false);
            quest5Clues.ForEach(hint => hint.playerNeedsHint = false);
        }

        private IEnumerator CancelExit()
        {
            yield return new WaitForSeconds(secondsToCancel);
            reset = true;
            exitDoor.GetComponent<SlidingDoor>().MoveDoor();
            exitTeleportation.SetActive(true);

        }
    }
}

