using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public GameObject door1;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void Update()
    {
        if(QuestManager.instance.questsComplete[2])
        {

        }
    }
    public void Quest1Complete()
    {
        door1.GetComponent<SlidingDoor>();
        
    }

}
