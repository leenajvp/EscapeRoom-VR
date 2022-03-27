using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public Transform playerStartPosition;
    public bool isTesting;
    public SlidingDoor[] door;
    public ObjectHides[] hiddenSpotsFirstLevel;
    public ObjectHides[] hiddenSpotsSecondLevel;
    public GameObject[] teleportsInSecondRoom;
    public GameObject[] exitGameTeleports;
    public GameObject altarTeleport;
    public bool papirusTeleport;
    public GameObject[] papirus;
    public List<Papirus> Items = new List<Papirus>();
    public bool gameComplete = false;

    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartGame();
        SetSounds();
        gameComplete = false;

        foreach (GameObject t in exitGameTeleports)
           t.gameObject.SetActive(false);
    }
    public void Update()
    {
        if (QuestManager.instance.questsComplete[1])
        {
            door[0].open = true;
            
        }
        if (QuestManager.instance.questsComplete[2])
        {
            for (int i = 0; i < teleportsInSecondRoom.Length; i++)
            {
                teleportsInSecondRoom[i].SetActive(true);
            }
        }
        if (QuestManager.instance.questsComplete[3])
        {
            for (int i = 1; i < hiddenSpotsSecondLevel.Length; i++)
            {
                hiddenSpotsSecondLevel[i].unlocked = true;
            }
        }
        if (QuestManager.instance.questsComplete[4])
        {
            door[1].open = true;
            altarTeleport.gameObject.SetActive(true);
            if(papirusTeleport)
            {
                for (int i = 0; i < papirus.Length; i++)
                    papirus[i].SetActive(true);
            }
        }
        if (QuestManager.instance.questsComplete[5])
        {
            hiddenSpotsSecondLevel[0].unlocked = true;
        }
        if(QuestManager.instance.questsComplete[6])
        {
            door[2].open = true;

            gameComplete = true;
            foreach(GameObject t in exitGameTeleports)
                gameObject.SetActive(true);
        }
    }
    public void Add(Papirus papirus)
    {
        Items.Add(papirus);
    }
    public void StartGame()
    {
        
        if (!isTesting)
        {
            player.transform.position = playerStartPosition.transform.position;
        }
        papirusTeleport = false;
        for (int i = 0; i < hiddenSpotsFirstLevel.Length; i++)
        {
            hiddenSpotsFirstLevel[i].unlocked = true;
        }
        for (int i = 0; i < papirus.Length; i++)
        {
            papirus[i].gameObject.SetActive(false);
        }
    }

    private void SetSounds()
    {
        if(PlayerPrefs.GetInt("SoundSettings") == 0)
            AudioListener.volume = 0;

        else
            AudioListener.volume = 1;
    }

 

   
}
