using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotationPuzzle;
using ButtonPuzzle;

public class QuestManager : MonoBehaviour
{
    public string[] questNames;
    public bool[] questsComplete;
    public static QuestManager instance;
    public ThrowPuzzle throwPuzzle;
    public ButtonPuzzleManager buttonPuzzle;
    public RotationPuzzleMain rotationPuzzle;
    [Header("QUEST1")]
    public int RequiredAmmount;
    public bool quest1Complete;
    [Header("QUEST4")]
    public Quest4 quest4;

    public int currentQuest;

    //NESSIE - AUDIO
    [Header("Audio")]
    public bool quest4Complete;

   
    // Start is called before the first frame update
    void Start()
    {
        quest1Complete = false;
        quest4Complete = false;
        instance = this;
        questsComplete = new bool[questNames.Length];
    }
    private void Update()
    {
        if (RequiredAmmount == 3)
        {
            quest1Complete = true;
            MarkQuestIfComplete("StatueObjects");
        }
        if(quest4.part1 && quest4.part2 && quest4.part3)
        {
            quest4Complete = true;
            MarkQuestIfComplete("Papirus");
        }
        if(buttonPuzzle.completed)
        {
            MarkQuestIfComplete("ButtonPuzzle");
        }
        if(rotationPuzzle.isPuzzleCompleted)
        {
            MarkQuestIfComplete("WheelPuzzle");
        }
        if (throwPuzzle.completed)
        {
            MarkQuestIfComplete("ThrowPuzzle");
        }
    }
    public int GetQuestNumber(string questToFind)
    {
        // check for the quest 
        for(int i =0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }
        // if the quest is not found go back to quest at position 0, so the first position in the array is empty
        Debug.LogError("Quest " + questToFind + " not found");
        return 0;
    }
    public bool CheckIfQuestComplete(string questToCheck)
    {
        if(GetQuestNumber(questToCheck) !=0)
        {
            return questsComplete[GetQuestNumber(questToCheck)];
        }
        return false;

    }
    public void MarkQuestIfComplete(string questToMark)
    {
        questsComplete[GetQuestNumber(questToMark)] = true;

        
       
    }
    public void MarkQuestIncomplete(string questName)
    {
        questsComplete[GetQuestNumber(questName)] = false;
    }

  
   

}
