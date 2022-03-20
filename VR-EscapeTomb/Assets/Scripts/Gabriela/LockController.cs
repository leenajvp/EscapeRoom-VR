using UnityEngine;

public class LockController : MonoBehaviour
{
    private int[] result, corretCombination;

    // Start is called before the first frame update
    void Start()
    {
        result = new int[] { 0, 0, 0 };
        corretCombination = new int[] { 0, 5, 4 };
        WheelPuzzle.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel1":
                result[0] = number;
                break;
            case "wheel2":
                result[1] = number;
                break;
            case "wheel3":
                result[2] = number;
                break;
        }

        if (result[0] == corretCombination[0] && result[1] == corretCombination[1] && result[2] == corretCombination[2])
        {
            Debug.Log("Correct");
            QuestManager.instance.MarkQuestIfComplete("RotationPuzzle");
        }
    }

    private void OnDestroy()
    {
        WheelPuzzle.Rotated -= CheckResults;
    }
}
