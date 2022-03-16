﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questNames;
    public bool[] questsComplete;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        questsComplete = new bool[questNames.Length];
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
