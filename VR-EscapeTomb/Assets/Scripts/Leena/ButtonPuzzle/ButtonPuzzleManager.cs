using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ButtonPuzzle
{
    public class ButtonPuzzleManager : MonoBehaviour
    {
        public bool completed;

        [Header("Button Puzzle")]
        [Tooltip("All buttons in list should be childed to this object")]
        [SerializeField] private List<Buttons> buttons = new List<Buttons>();
        [Tooltip("Sequence required, must be same lenght as button quantity")]
        // public string correctSolution = "1234";
        public int[] correctSolution;
        public List<int> enetered = new List<int>();
        public List<int> checkNum = new List<int>();
        public string enteredSequence;
        [Tooltip("Activate light when puzzle completed")]
        [SerializeField] private Light lightToActivate;
        public int currentNum = 1;

        public bool received = false;

        void Start()
        {
            completed = false;
            received = false;

            if (correctSolution.Length > buttons.Count)
                Debug.LogError(name + "Correct sequence is too long");

            if (lightToActivate != null)
                lightToActivate.gameObject.SetActive(false);
        }

        void Update()
        {
            if (currentNum > buttons.Count)
            {
                completed = true;

                if (lightToActivate != null)
                    lightToActivate.gameObject.SetActive(true);
            }
        }

        public void AddNumber(int entry)
        {
            if (entry == currentNum)
            {
                currentNum++;
                return;
            }

            else
            {
                buttons.ForEach(button => button.reset = true);
                currentNum = 1;
                return;
            }
        }
    }
}

