using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource PlayerAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource endGameAudioSource;

    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Quest Sound Bools")] //Ensuring the sound only plays once
    [SerializeField] private bool q1Play, Q2Play, Q3Play, Q4Play, Q5Play, Q6Play, gameCompletePlay;

    private void Start()
    {
        
        musicAudioSource.Play();
        endGameAudioSource.Stop();

        q1Play = false;
        Q2Play = false;
        Q3Play = false;
        Q4Play = false;
        Q5Play = false;
        Q6Play = false;
        gameCompletePlay = false;
    }
    private void Update()
    {
        checkQuests();
        EndGameCheck();
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


    }

    private void PlayAudio()
    {
        PlayerAudioSource.Play();
    }

    private void EndGameCheck()
    {
        if (gameManager.gameComplete && !gameCompletePlay)
        {

            changeMusic();

        }
    }

    private void changeMusic()
    {
        musicAudioSource.Pause();
        endGameAudioSource.Play();

        gameCompletePlay = true;
        Debug.Log("Changing");
        StartCoroutine(EndGameTimeCheck());

    }

    IEnumerator EndGameTimeCheck()
    {
        yield return new WaitForSeconds(13f); 
        endGameAudioSource.Stop();
        musicAudioSource.UnPause();
        

    }
}
