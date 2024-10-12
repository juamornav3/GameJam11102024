using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle3Manager : MonoBehaviour
{
    public List<TMP_Dropdown> dropdowns;
    public PuzzleData puzzleData;
    public AudioClip sound;
    public AudioClip correct;
    public AudioClip error;
    public GameObject transition;
    public GameObject music;
    public Result result;
    public List<int> solution;
    public List<int> sceneId;
    public List<string> advices;

    void Start()
    {
        Cursor.visible = true;
        for (int i = 0; i < dropdowns.Count; i++)
        {
            dropdowns[i].value = 0;
        }

    }

    public void Solve()
    {
        for (int i = 0; i < dropdowns.Count; i++)
        {
            puzzleData.finished = dropdowns[i].value == solution[i];
            if (!puzzleData.finished)
            {
                break;
            }
        }
        SoundManager.instance.playSound(sound, 0.6f);
        music.GetComponent<AudioSource>().enabled = false;
        LoadResult();
        result.result = puzzleData.finished;
        if (puzzleData.finished)
        {
            transition.GetComponent<SceneController>().SolvePuzzle(correct, puzzleData.finished);
        }
        else
        {
            transition.GetComponent<SceneController>().SolvePuzzle(error, puzzleData.finished);
        }
    }

    private void LoadResult()
    {
        result.result = puzzleData.finished;
        result.idPuzle = sceneId[1];
        result.idScene = sceneId[0];
        result.well = advices[0];
        result.bad = advices[1];
    }

    public void Restart()
    {
        Start();
    }

    


}
