using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{

    //[Header("Master AudioMixer")]
   // [SerializeField] private AudioMixer audioMixer;

    [Header("Toggle Audio On/Off")]
    [SerializeField] private Toggle audioToggle;

    [Header("Toggle Clues On/Off")]
    [SerializeField] private Toggle clueToggle;

    [Header("Timer Clues On/Off")]
    [SerializeField] private Toggle timerToggle;

    [Header("Sliders to Manager AudioMixer")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        audioToggle.isOn = true;

        if (PlayerPrefs.GetInt("SoundSettings") == 0)
        {
            audioToggle.isOn = true;
        }

        else if (PlayerPrefs.GetInt("SoundSettings") == 1)
        {
            audioToggle.isOn = false;
        }
    }

    private void Update()
    {
        SoundSettings();
        TimerSettings();
        ClueSettings();
    }


    public void TimerSettings()
    {
        if (timerToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Timer", 0); // timer ff
        }

        else if (timerToggle.isOn == true)
        {
            PlayerPrefs.SetInt("Timer", 1); // timer on
        }
    }

    public void ClueSettings()
    {
        if (clueToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Clues", 0); // Clues ff
        }

        else if (clueToggle.isOn == true)
        {
            PlayerPrefs.SetInt("Clues", 1); // Clues on
        }
    }

    public void SoundSettings()
    {
        if (audioToggle.isOn == true)
        {
            PlayerPrefs.SetInt("SoundSettings", 0); // sound on
            AudioListener.volume = 1;
        }

        else if (audioToggle.isOn == false)
        {
            PlayerPrefs.SetInt("SoundSettings", 1); // sound off
            AudioListener.volume = 0;
        }
    }

    public void SetSFXLevel(float sfxLvl)
    {
       // audioMixer.SetFloat("sfxVol", sfxLvl); // change to correct names from  audio mixer
        PlayerPrefs.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        // audioMixer.SetFloat("mVol", musicLvl);  // change to correct names from  audio mixer
        PlayerPrefs.SetFloat("mVol", musicLvl);
    }
}
