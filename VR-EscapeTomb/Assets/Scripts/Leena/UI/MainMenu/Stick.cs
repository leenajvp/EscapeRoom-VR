using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stick : MonoBehaviour
{
    public bool isLit;
    public GameObject fire;
    public float burnTime = 10.0f;

    private AudioSource sfx;
    private bool hitFire = false;
    private bool soundPlaying = false;

    private float soundLenght;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        soundLenght = sfx.clip.length;
        fire.SetActive(false);
        isLit = false;
    }

    void Update()
    {
        SoundEffects();

        if (isLit)
        {
            hitFire = true;
            fire.SetActive(true);
        }

        else
            fire.SetActive(false);
    }

    public IEnumerator BurnTimer()
    {
        yield return new WaitForSeconds(burnTime);
        isLit = false;
        soundPlaying = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLit)
        {
            IDiegeticUI light = other.gameObject.GetComponent<IDiegeticUI>();

            if (light != null)
                light.GetEvent();
        }
    }

    private void SoundEffects()
    {
        if (hitFire && !soundPlaying)
        {
            StartCoroutine(SoundTimer());

            if (!sfx.isPlaying)
                sfx.Play();

            soundPlaying = true;
        }
    }

    private IEnumerator SoundTimer()
    {
        yield return new WaitForSeconds(soundLenght);

        hitFire = false;
        sfx.Stop();
    }

}
