using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class CriptoPuzle : MonoBehaviour
{
    public List<TMP_InputField> inputs;
    public List<string> solution;
    public List<string> abc;
    public PuzzleData puzzleData;
    public AudioClip sound;
    public AudioClip correct;
    public AudioClip error;
    public GameObject transition;
    public GameObject music;
    public Result result;
    public List<int> sceneId;
    public List<string> advices;
    public TextMeshProUGUI textPuzzle;
    public string initialText;

    // Diccionario para almacenar los reemplazos realizados
    private string textoModificado;
    private Dictionary<char, List<int>> posicionesLetraA = new Dictionary<char, List<int>>();

    // Función para inicializar el texto original
    private string textoOriginal = "";
   


    void Start()
    {
        
        Cursor.visible = true;
        textPuzzle.text = initialText;
        textoOriginal = initialText;
        InicializarDiccionarioPosiciones();

        textoModificado = textPuzzle.text;
        for (int i = 0; i < inputs.Count; i++)
        {
            inputs[i].text = "";
        }

    }


    public void InicializarDiccionarioPosiciones()
    {
        posicionesLetraA = new Dictionary<char, List<int>>();
        foreach (string l in abc)
        {
            char letra = l[0];
            posicionesLetraA.Add(letra, new List<int>());
            
        }

        for (int i = 0; i < textoOriginal.Length; i++)
        {
            char letra = textoOriginal[i];

            if (posicionesLetraA.ContainsKey(letra))
            {
                posicionesLetraA[letra].Add(i);
            }
            
        }
    }

    public void ChangeText(int idInput)
    {
        char letraOriginal = abc[idInput][0];
        string nuevoTexto = inputs[idInput].text;

        if (!string.IsNullOrEmpty(nuevoTexto) && char.IsLetter(nuevoTexto[0]))
        {
            List<int> posiciones = posicionesLetraA.ContainsKey(letraOriginal) ? posicionesLetraA[letraOriginal] : new List<int>();
            foreach (int posicion in posiciones)
            {
                if (posicion >= 0 && posicion < textoModificado.Length)
                {
                    textoModificado = textoModificado.Remove(posicion, 1).Insert(posicion, nuevoTexto);
                }
            }
            textPuzzle.text = textoModificado;
        }
    }
    public void Solve()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            puzzleData.finished = inputs[i].text == solution[i];
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
