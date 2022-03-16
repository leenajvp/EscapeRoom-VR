using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public SlidingDoor door;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }
    public void Update()
    {
      if(QuestManager.instance.CheckIfQuestComplete("Puzzle1"))
        {
            door.open = true;
        }
    }
    



}
