using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    //[Header("Master AudioMixer")]
    // [SerializeField] private AudioMixer audioMixer; // NESSIE REMOVE THIS COMMENT WHEN AUDIOMIXER DONE

    [Header("Toggle Audio On/Off")]
    [SerializeField] private Toggle audioToggle;

    [Header("Toggle Timer On/Off")]
    [SerializeField] private Toggle timerToggle;

    [Header("Sliders to Manager AudioMixer")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SoundSettings"))
        {
            PlayerPrefs.SetInt("SoundSettings", 1);
            audioToggle.isOn = true;
            AudioListener.volume = 1;
        }

        if (!PlayerPrefs.HasKey("Timer"))
        {
            PlayerPrefs.SetInt("Timer", 1);
            timerToggle.isOn = true;
        }

        CheckPrefs("SoundSettings", audioToggle);
        CheckPrefs("Timer", timerToggle);
    }

    private void Update()
    {
        SoundSettings();
        TimerSettings();
    }

    public static void CheckPrefs(string playerpref, Toggle toggle)
    {
        if (PlayerPrefs.GetInt(playerpref) == 0)
        {
            toggle.isOn = false;
        }

        else
        {
            toggle.isOn = true;
        }
    }

    public void TimerSettings()
    {
        if (timerToggle.isOn == false)
            PlayerPrefs.SetInt("Timer", 0); // timer off

        else
            PlayerPrefs.SetInt("Timer", 1); // timer on
    }

    public void SoundSettings()
    {
        if (audioToggle.isOn == false)
        {
            PlayerPrefs.SetInt("SoundSettings", 0); // sound on
            AudioListener.volume = 0;
        }

        else if (audioToggle.isOn == true)
        {
            PlayerPrefs.SetInt("SoundSettings", 1); // sound off
            AudioListener.volume = 1;
        }
    }

    public void SetSFXLevel(float sfxLvl)
    {
        // audioMixer.SetFloat("sfxVol", sfxLvl); // change to correct names from  audio mixer // NESSIE
        PlayerPrefs.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        // audioMixer.SetFloat("mVol", musicLvl);  // change to correct names from  audio mixer // NESSIE
        PlayerPrefs.SetFloat("mVol", musicLvl);
    }
}
