using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DartboardManager : MonoBehaviour
{
    public int score;
    public AudioClip destroySound;
    MiniGameManager minigameManager;


    private void Awake()
    {
        minigameManager = GameObject.Find("Main Camera").GetComponent<MiniGameManager>();
    }
    void Start()
    {

    }

    public void DestroyDartboard()
    {
        SoundManager.instance.playSound(destroySound, 0.7f);
        StartCoroutine(WaitForDestruction());
    }

    public void DestroyOctopusLeg()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        minigameManager.cont++;
        if (minigameManager.cont >= 4)
        {
            minigameManager.cont = 0;
            minigameManager.ShowHead();
        }
    }

    IEnumerator WaitForDestruction()
    {
        
        if (score>0)
        {
            int showScore = minigameManager.mult * score;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + showScore.ToString();
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
        gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Destroy");
        gameObject.GetComponentInParent<Animator>().SetTrigger("Destroy");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        if (score > 0)
        {
            minigameManager.score = minigameManager.score + score * minigameManager.mult;
            if (minigameManager.mult < 5)
            {
                minigameManager.mult++;
            }
        }else
        {
            minigameManager.score = Math.Max(minigameManager.score + score,0);
            minigameManager.mult = 1;
        }

    }
}
