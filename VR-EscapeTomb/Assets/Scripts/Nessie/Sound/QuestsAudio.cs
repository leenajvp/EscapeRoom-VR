using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource PlayerAudioSource;

    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Quest Sound Bools")] //Ensuring the sound only plays once
    [SerializeField] private bool q1Play, Q2Play, Q3Play, Q4Play, Q5Play, Q6Play;

    private void Start()
    {
        q1Play = false;
        Q2Play = false;
        Q3Play = false;
        Q4Play = false;
        Q5Play = false;
        Q6Play = false;
    }
    private void Update()
    {
        checkQuests();
    }

    private void checkQuests()
    {
        if (gameManager.q1 && !q1Play)
        {
            PlayAudio();
            q1Play = true;

        }

        if (gameManager.q2 && !Q2Play)
        {
            PlayAudio();
            Q2Play = true;
        }

        if (gameManager.q3 && !Q3Play)
        {
            PlayAudio();
            Q3Play = true;

        }

        if (gameManager.q4 && !Q4Play)
        {
            PlayAudio();
            Q4Play = true;
        }

        if (gameManager.q5 && !Q5Play)
        {
            PlayAudio();
            Q5Play = true;
        }

        if (gameManager.q6 && !Q6Play)
        {
            PlayAudio();
            Q6Play = true;
        }

        else
        {
          //  PlayerAudioSource.Stop();
        }

    }

    private void PlayAudio()
    {
        PlayerAudioSource.Play();
    }
}
