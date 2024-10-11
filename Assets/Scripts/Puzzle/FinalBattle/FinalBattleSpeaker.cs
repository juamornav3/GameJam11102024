using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBattleSpeaker : MonoBehaviour
{
    public GameObject background;
    void Start()
    {
        if (!gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished)
        {
            StartCoroutine(WaitToConver());
        }
        else
        {
            background.GetComponent<AudioSource>().enabled = true;
        }
    }
    IEnumerator WaitToConver()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        yield return new WaitUntil(() => gameObject.GetComponent<DialogueSpeaker>().conversations[0].finished);
        background.GetComponent<AudioSource>().enabled = true;
    }
}
