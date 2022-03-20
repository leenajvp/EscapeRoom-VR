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
        public string correctSolution = "1234";
        public string enteredSequence;
        private int currentNum = 0;

        void Start()
        {
            completed = false;

            if (correctSolution.Length > buttons.Count)
                Debug.LogError(name + "Correct sequence is too long");
        }

        void Update()
        {
            bool isEqual = Enumerable.SequenceEqual(correctSolution, enteredSequence);

            if (isEqual)
                completed = true;
        }

        public void AddNumber(string entry)
        {
            for (int i = currentNum; i < correctSolution.Length; i++)
            {
                if (entry == correctSolution[i].ToString())
                {
                    enteredSequence += entry;
                    currentNum++;
                    break;
                }

                else
                {
                    buttons.ForEach(button => button.reset = true);
                    currentNum = 0;
                    enteredSequence = "";
                    break;
                }
            }
        }
    }
}

