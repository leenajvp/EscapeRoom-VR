using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    [Header("Toggle Audio On/Off")]
    [SerializeField] private Toggle audioToggle;

    [Header("Toggle Timer On/Off")]
    [SerializeField] private Toggle timerToggle;

    [Header("Sliders to Manager AudioMixer")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    //NESSIE - AUDIO
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

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

        if (!PlayerPrefs.HasKey("mVol"))
        {
            PlayerPrefs.SetInt("mVol", 1);
            sfxSlider.value = 1;
        }

        if (!PlayerPrefs.HasKey("sfxVol"))
        {
            PlayerPrefs.SetFloat("sfxVol", 1);
            sfxSlider.value = 1;
        }

        float currentSFX = PlayerPrefs.GetFloat("sfxVol");
        float currentMusic = PlayerPrefs.GetFloat("mVol");

        musicSlider.value = currentMusic;
        sfxSlider.value = currentSFX;

        CheckPrefs("SoundSettings", audioToggle);
        CheckPrefs("Timer", timerToggle);
    }

    private void Update()
    {
        SoundSettings();
        TimerSettings();

        //This version of Unity has a possible bug on slider single value so as work around sliders are checked on update
        SetSFXLevel(sfxSlider.value);
        SetMusicLevel(musicSlider.value);
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
        // NESSIE
        sfxMixer.SetFloat("Volume", sfxLvl); // change to correct names from  audio mixer 
        PlayerPrefs.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        // NESSIE
        musicMixer.SetFloat("Volume", musicLvl);  // change to correct names from  audio mixer 
        PlayerPrefs.SetFloat("mVol", musicLvl);
    }
}
