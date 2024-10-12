using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Profiling;


public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    } 

    public void ChangeVolume(float level)
    {
        audioMixer.SetFloat("volume", level);
    }


    
}
