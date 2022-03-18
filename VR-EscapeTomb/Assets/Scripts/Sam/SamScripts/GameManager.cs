using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public SlidingDoor door;
    public List<Papirus> Items = new List<Papirus>();
    [Header("QUEST4")]
    public Quest4 quest4;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void Update()
    {
      if(QuestManager.instance.CheckIfQuestComplete("StatueObjects"))
        {
            //door.MoveDoor();
            door.open = true;
        }
      if(QuestManager.instance.CheckIfQuestComplete("Papirus"))
        {

        }
      if(QuestManager.instance.CheckIfQuestComplete("WheelPuzzle"))
        {

        }
    }
   public void Add(Papirus papirus)
    {
        Items.Add(papirus);
    }
}
