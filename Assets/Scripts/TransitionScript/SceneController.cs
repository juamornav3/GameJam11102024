using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public event EventHandler OnChange;
    public int sceneId;
    private Animator animator;
    public int duration;
    public AudioClip buttonAudio;
    public bool itsChanging = false;
   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.None;
      
    }

    public void ChangeSceneSound(AudioClip sound, int id)
    {
        if (sound != null)
        {
            SoundManager.instance.playSound(sound, 1f);
        }
        ChangeScene(id);
    }
    public void ChangeSceneWithSound()
    {
        if (buttonAudio != null)
        {
            SoundManager.instance.playSound(buttonAudio, 0.6f);
        }
        ChangeScene();
    }

    public void ChangeScene(int id)
    {
        sceneId = id;
        ChangeScene();
    }
    public void ChangeScene()
    {
        itsChanging = true;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(TransitionScene());
    }

    public void SolvePuzzle(AudioClip audio, bool finished)
    {
        itsChanging = true;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(TransitionPuzzle(audio, finished));
    }
    IEnumerator TransitionScene()
    {
        OnChange?.Invoke(this, EventArgs.Empty);
        animator.SetTrigger("Init");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneId);
    }

    IEnumerator TransitionPuzzle(AudioClip audio, bool finished)
    {
        OnChange?.Invoke(this, EventArgs.Empty);
        animator.SetTrigger("Init");
        yield return new WaitForSeconds(5f);
        if (finished)
        {
            animator.SetTrigger("Correct");
            SoundManager.instance.playSound(audio, 1f);
        }
        else
        {
            animator.SetTrigger("Error");
            SoundManager.instance.playSound(audio, 0.5f);
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneId);
    }
}
