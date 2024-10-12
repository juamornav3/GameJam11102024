using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public List<GameObject> dartboards;
    public List<AudioClip> audioclips;
    public GameObject spawnManager;
    public GameObject octopusTentacles;
    public GameObject octopusHead;
    public GameObject dog;
    public GameObject scorePanel;
    public GameObject menu;
    public GameObject music;
    public int score;
    public int mult;
    public int cont;
    public int scene;
    public int goalScore;
    SceneController transition;
    public PuzzleData puzzleData;
    public GameObject timer;

    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
    }
    void Start()
    {
        Cursor.visible = false;
        music.SetActive(false);
        score = 0;
        cont = 0;
        mult = 1;
        scorePanel.SetActive(false);
        spawnManager.SetActive(false);
        menu.SetActive(true);
    }

    private void Update()
    {
        scorePanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString();
        scorePanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = mult.ToString();
    }

    public void ShowHead()
    {
        SoundManager.instance.playSound(audioclips[1],1f);
        octopusHead.SetActive(true);
    }

    public void StartMiniGame()
    {
        SoundManager.instance.playSound(audioclips[0],0.6f);
        timer.gameObject.GetComponent<Timer>().SetDuration(86f);
        music.SetActive(true);
        menu.SetActive(false);
        scorePanel.SetActive(true);
        spawnManager.SetActive(true);
        StartCoroutine(MiniGame());
    }

    public void Restart()
    {
        menu.SetActive(false);
        transition.ChangeScene(scene);
    }

    public void Exit()
    {
        menu.SetActive(false);

        transition.ChangeScene(9);
    }
     public void FinishMinigame()
    {
        puzzleData.finished = score >= goalScore;
        SoundManager.instance.playSound(audioclips[0], 0.6f);

        if (puzzleData.finished ) {
            menu.SetActive(true);
            menu.transform.GetChild(0).gameObject.SetActive(true);
            menu.transform.GetChild(1).gameObject.SetActive(false);
            menu.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(DogAppear());
        }
    }
    IEnumerator MiniGame()
    {
        yield return new WaitForSeconds(60f);
        spawnManager.SetActive(false);
        yield return new WaitForSeconds(11f);
        SoundManager.instance.playSound(audioclips[0], 0.6f);
        yield return new WaitForSeconds(3f);
        octopusTentacles.SetActive(true);
        yield return new WaitForSeconds(7f);
        for (int i = 0; i<octopusTentacles.transform.childCount; i++)
        {
            if (octopusTentacles.transform.GetChild(i) != null)
            {
                octopusTentacles.transform.GetChild(i).GetComponent<Animator>().SetTrigger("Destroy");
            }
        }
        yield return new WaitForSeconds(5f);
        if (octopusHead != null)
        {
            octopusHead.GetComponent<Animator>().SetTrigger("Destroy");
        }

        FinishMinigame();

    }

    IEnumerator DogAppear()
    {
        dog.SetActive(true);
        yield return new WaitForSeconds(2f);
        menu.SetActive(true);
        menu.transform.GetChild(0).gameObject.SetActive(false);
        menu.transform.GetChild(1).gameObject.SetActive(true);
        menu.transform.GetChild(2).gameObject.SetActive(false);
    }

}
