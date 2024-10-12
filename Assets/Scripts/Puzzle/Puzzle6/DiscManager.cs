using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscManager : MonoBehaviour
{
    public PuzzleData puzzleData;
    public List<GameObject> discs;
    public AudioClip sound;
    public AudioClip reset;
    public AudioClip correct;
    public AudioClip error;
    public AudioClip rotation;
    public GameObject transition;
    public GameObject music;
    public GameObject code;
    public Result result;
    public GameObject inputField;
    public int idPuzzle;
    public int idScene;
    void Start()
    {
        Cursor.visible = true;
        inputField.GetComponent<TMP_InputField>().text = "";
        foreach(GameObject disc in discs)
        {
            disc.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }


    public void SetRotationDisc(int discPosition)
    {
        if (discPosition >= 0 && discPosition < discs.Count)
        {
            SoundManager.instance.playSound(rotation, 0.7f);
            Quaternion rotacionActual = discs[discPosition].transform.rotation;
            Quaternion nuevaRotacion = Quaternion.Euler(0f, 0f, 90f) * rotacionActual;
            discs[discPosition].transform.rotation = nuevaRotacion;
        }
    }

    public void Solve()
    {
        puzzleData.finished = inputField.GetComponent<TMP_InputField>().text == "254";
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
        result.idPuzle = idPuzzle;
        result.idScene = idScene;
        result.well = "¡Genial!,¿Has tenido problemas con alguno?";
        result.bad = "Que lástima... fíjate mejor en las rotaciones que hacen los discos en el código...";
    }

    public void ResetPuzzle()
    {
        SoundManager.instance.playSound(reset, 0.3f);
        Start();
    }
    
    public void ChangeView()
    {
        code.gameObject.SetActive(!code.gameObject.activeSelf);
    }
}
