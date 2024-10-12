using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; 

    private AudioSource audioSource;

    [Range(0, 1)]
    public float masterVolume = 1.0f; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = masterVolume; 
    }

    public void playSound(AudioClip sound, float volumeScale)
    {

        audioSource.PlayOneShot(sound, volumeScale);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
