using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Puzzle2Manager : MonoBehaviour
{
    public AudioClip sound;
    public AudioClip correct;
    public AudioClip error;
    public PuzzleData puzzleData;
    public List<GameObject> gameObjects;
    public List<Sprite> solution;
    public GameObject transition;
    public GameObject music;
    public Result result;
    public int idPuzzle;
    public int idScene;
    void Start()
    {
        Cursor.visible = true;
    }

    
    public void Solve()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            puzzleData.finished = (gameObjects[i].GetComponent<Image>().sprite == solution[i]);
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
            transition.GetComponent<SceneController>().SolvePuzzle(correct,puzzleData.finished);
        }
        else
        {
            transition.GetComponent<SceneController>().SolvePuzzle(error, puzzleData.finished);
        }
        
    }

    private void LoadResult()
    {
        result.result = puzzleData.finished;
        result.idPuzle = idPuzzle;
        result.idScene = idScene;
        result.well = "¡Genial!,¿Has tenido problemas con alguna?";
        result.bad = "Que lástima... fíjate mejor en los resultados que pueden dar las expresiones...";
    }
}
