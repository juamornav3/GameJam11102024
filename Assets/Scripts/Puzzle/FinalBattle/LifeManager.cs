using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    
    public List<Conversation> conversations = new List<Conversation>();
    public GameObject agentLife;
    public GameObject iaLife;
    public GameObject transition;
    public PuzzleData battle;
    public Result result;
    public GameObject background;
    public GameObject redLight;
    public AudioClip correct;
    public AudioClip error;
    public AudioClip iaHit;
    public AudioClip agentHit;
    public AudioClip iaDead;
    SoundManager soundManager;
    int agentCount = 5;
    int iaCount = 9;
    int streak = 0;


    private void Awake()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    void Start()
    {
        foreach (var conversation in conversations)
        {
            conversation.finished = false;
        }

        for (int i=0; i < iaLife.transform.childCount; i++)
        {
            iaLife.transform.GetChild(iaCount).gameObject.GetComponent<Animator>().enabled = false;
        }
    }
    void Update()
    {
        if (conversations[0].finished)
        {
            conversations[0].finished = false;
            StartCoroutine(DestroyLifeSlot(iaCount));
            iaCount = Mathf.Max(-1,iaCount-1);
            if (iaCount < 3 && !redLight.activeSelf)
            {
                redLight.SetActive(true);
            }
            if (iaCount < 0)
            {
                iaCount = 0;
                battle.finished = true;
                FinishBattle();
            }
            streak++;
            if (streak >= 2)
            {
                streak = 0;
                agentCount = Mathf.Min(agentCount + 1, 5);
                agentLife.transform.GetChild(agentCount).gameObject.SetActive(true);
            }

        }else if (conversations[1].finished)
        {
            conversations[1].finished = false;
            agentLife.transform.GetChild(agentCount).gameObject.SetActive(false);
            agentCount = Mathf.Max(-1, agentCount - 1);
            streak = 0;
            gameObject.GetComponent<Animator>().SetTrigger("Damage");
            soundManager.playSound(agentHit, 1f);
            if (agentCount < 0)
            {
                agentCount = 0;
                battle.finished=false;
                FinishBattle();
            }
        }   
    }

    private void FinishBattle()
    {
        LoadResult();
        soundManager.playSound(iaDead, 1f);
        transition.GetComponent<SceneController>().ChangeScene();
        background.GetComponent<AudioSource>().enabled = false;

    }

    private void LoadResult()
    {
        Cursor.visible = true;
        result.result = battle.finished;
        result.idPuzle = 29;
        result.idScene = 34;
        result.well = "¡Bien hecho!, ¡Has acabado con los planes de Guido!";
        result.bad = "Que lástima... recuerda intentar responder la incorrecta para desentrenar a GUIA...";
    }

    IEnumerator DestroyLifeSlot(int i)
    {
        iaLife.transform.GetChild(i).gameObject.GetComponent<Animator>().enabled = true;
        soundManager.playSound(iaHit, 1f);
        yield return new WaitForSeconds(0.5f);
        iaLife.transform.GetChild(i).gameObject.SetActive(false);
    }
}
