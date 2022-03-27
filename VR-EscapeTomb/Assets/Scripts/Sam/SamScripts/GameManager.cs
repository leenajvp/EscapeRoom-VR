﻿using System.Collections;
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

    private bool q1, q2, q3, q4, q5, q6;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartGame();
        SetSounds();
        gameComplete = false;

       // foreach (GameObject t in exitGameTeleports)
          //  t.gameObject.SetActive(false);
    }
    public void Update()
    {
        if (QuestManager.instance.questsComplete[1])
        {
            if (!q1)
            {
                door[0].MoveDoor();
                q1 = true;
            }

        }
        if (QuestManager.instance.questsComplete[2])
        {
            if (!q2)
            {
                for (int i = 0; i < teleportsInSecondRoom.Length; i++)
                {
                    teleportsInSecondRoom[i].SetActive(true);
                }
                q2 = true;
            }

        }
        if (QuestManager.instance.questsComplete[3])
        {
            if (!q3)
            {
                for (int i = 1; i < hiddenSpotsSecondLevel.Length; i++)
                {
                    hiddenSpotsSecondLevel[i].unlocked = true;
                }
                q3 = true;
            }
        }
        if (QuestManager.instance.questsComplete[4])
        {
            if (!q4)
            {
                door[1].MoveDoor();
                altarTeleport.gameObject.SetActive(true);
                if (papirusTeleport)
                {
                    for (int i = 0; i < papirus.Length; i++)
                        papirus[i].SetActive(true);
                }
                q4 = true;
            }

        }
        if (QuestManager.instance.questsComplete[5])
        {
            if (!q5)
            {
                hiddenSpotsSecondLevel[0].unlocked = true;
                q5 = true;
            }
        }
        if (QuestManager.instance.questsComplete[6])
        {
            if (!q6)
            {
                door[2].MoveDoor();
                gameComplete = true;
                foreach (GameObject t in exitGameTeleports)
                    gameObject.SetActive(true);
            }

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
        q1 = false;
        q2 = false;
        q3 = false;
        q4 = false;
        q5 = false;
        q6 = false;
    }

    private void SetSounds()
    {
        if (PlayerPrefs.GetInt("SoundSettings") == 0)
            AudioListener.volume = 0;

        else
            AudioListener.volume = 1;
    }



}
