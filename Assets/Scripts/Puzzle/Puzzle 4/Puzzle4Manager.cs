using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle4Manager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public PuzzleData puzzleData;
    bool levelComplete;
    public AudioClip sound;
    public AudioClip correct;
    public AudioClip error;
    public GameObject transition;
    public GameObject music;
    public Result result;
    public List<int> solution;
    public int currentLevel = 0;
    public int dropdown = 0;
    public List<GameObject> buttons = new List<GameObject>();
    public int idPuzzle;
    public int idScene;
    void Start()
    {
        levelComplete = false;
        currentLevel = 0;
        Cursor.visible = true;
    }

    public void ButtonReset()
    {
        levelComplete = false;
        GameObject level = levels[currentLevel].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GameObject();
        for (int i = 0; i < level.transform.childCount; i++)
        {
            levels[currentLevel].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<TMP_Dropdown>().value = 0;
        }
    }
    public void Reset()
    {
        levelComplete = false;
        if (currentLevel % 2 == 0)
        {
            dropdown = Mathf.Max(0,dropdown - 1);
        }
        else
        {
            dropdown = dropdown - 2;
        }
        GameObject level = levels[currentLevel].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GameObject();
        for (int i = 0; i < level.transform.childCount; i++)
        {
            levels[currentLevel].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<TMP_Dropdown>().value = 0;
        }
        
    }

    public void SolveLevel()
    {
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = false;
        }
        GameObject level = levels[currentLevel].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GameObject();
        if (currentLevel < 4)
        {
            if (currentLevel % 2 == 0)
            {
                levelComplete = level.transform.GetChild(0).GetComponent<TMP_Dropdown>().value == solution[dropdown];
            }
            else
            {
                levelComplete = level.transform.GetChild(0).GetComponent<TMP_Dropdown>().value == solution[dropdown] && level.transform.GetChild(1).GetComponent<TMP_Dropdown>().value == solution[dropdown + 1];
                dropdown++;
            }
            dropdown++;

            AnimateSolution(level);
        }     
    }

    public void NextLevel()
    {
        if (levelComplete)
        {
            if (currentLevel != 3)
            {
                levels[currentLevel].SetActive(false);
                levels[currentLevel + 1].SetActive(true);
            }
            else
            {
                Solve();
            }
            currentLevel++;

        }
        else
        {
            Reset();
        }
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().enabled = true;
        }
    }
    public void Solve()
    {
        puzzleData.finished = levelComplete;
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

    private void AnimateSolution(GameObject level)
    {
        List<int> dropdowns = new List<int>();
        for (int i = 0; i < level.transform.childCount; i++)
        {
            dropdowns.Add(level.transform.GetChild(i).GetComponent<TMP_Dropdown>().value);
        }

        switch (currentLevel)
        {
            case 0:
                AnimateLevel1(dropdowns);
                break;
            case 1:
                AnimateLevel21(dropdowns);
                break;
            case 2:
                AnimateLevel3(dropdowns);
                break;
            case 3:
                AnimateLevel41(dropdowns);
                break;
        }
    }

    private void AnimateLevel1(List<int> dropdowns)
    {
        switch (dropdowns[0])
        {
            case 0:
                NextLevel();
                break;
            case 1:
                StartCoroutine(AnimateLevels("DropFail", 1.3f));
            break;
            case 2:
                StartCoroutine(AnimateLevels("DropFail", 1.3f));
                break;
            case 3:
                StartCoroutine(AnimateLevels("Level1", 1.5f));
                break;
            case 4:
                StartCoroutine(AnimateLevels("", 1.3f));
                break;
        }
    }

    private void AnimateLevel21(List<int> dropdowns)
    {

            switch (dropdowns[0])
            {
                case 0:
                    NextLevel();
                    break;
                case 1:
                    StartCoroutine(AnimateFirstPartLevel("minorFail", 0.5f, dropdowns));
                    break;
                case 2:
                    StartCoroutine(AnimateFirstPartLevel("Level21", 1f, dropdowns));
                    break;
                case 3:
                    StartCoroutine(AnimateFirstPartLevel("minorFail", 0.5f, dropdowns));
                    break;
                case 4:
                    StartCoroutine(AnimateFirstPartLevel("MayorFail", 1f, dropdowns));
                    break;
            }

    }

    private void AnimateLevel22(int dropdownValue)
    {
        switch (dropdownValue)
        {
            case 0:
                StartCoroutine(AnimateLevels("Reset", 0.5f));
                break;
            case 1:
                StartCoroutine(AnimateLevels("Infinite", 3.4f));
                break;
            case 2:
                StartCoroutine(AnimateLevels("NotWheel", 0.1f));
                break;
            case 3:
                StartCoroutine(AnimateLevels("PoorIter", 1f));
                break;
            case 4:
                StartCoroutine(AnimateLevels("Level22", 1.1f));
                break;
        }
    }

    private void AnimateLevel3(List<int> dropdowns)
    {
        switch (dropdowns[0])
        {
            case 0:
                NextLevel();
                break;
            case 1:
                StartCoroutine(AnimateLevels("Correct", 1f));
                break;
            case 2:
                StartCoroutine(AnimateLevels("NotTurnOff", 2.25f));
                break;
            case 3:
                StartCoroutine(AnimateLevels("NotTurnOff", 2.25f));
                break;
            case 4:
                StartCoroutine(AnimateLevels("Infinite", 1f));
                break;
        }
    }

    private void AnimateLevel41(List<int> dropdowns)
    {
        switch (dropdowns[0])
        {
            case 0:
                NextLevel();
                break;
            case 1:
                StartCoroutine(AnimateFirstPartLevel("Fail", 0.5f, dropdowns));
                break;
            case 2:
                StartCoroutine(AnimateFirstPartLevel("Fail", 0.5f, dropdowns));
                break;
            case 3:
                StartCoroutine(AnimateFirstPartLevel("L31Correct", 1.2f, dropdowns));
                break;
            case 4:
                StartCoroutine(AnimateFirstPartLevel("Fail", 0.5f, dropdowns));
                break;
        }
    }

    private void AnimateLevel42(int dropdownValue)
    {
        switch (dropdownValue)
        {
            case 0:
                StartCoroutine(AnimateLevels("Reset", 0.5f));
                break;
            case 1:
                StartCoroutine(AnimateLevels("Infinite", 2.3f));
                break;
            case 2:
                StartCoroutine(AnimateLevels("idle2", 0.1f));
                break;
            case 3:
                StartCoroutine(AnimateLevels("idle2", 0.1f));
                break;
            case 4:
                StartCoroutine(AnimateLevels("L32Correct", 1.3f));
                break;
        }
    }
    private void LoadResult()
    {
        result.result = puzzleData.finished;
        result.idPuzle = idPuzzle;
        result.idScene = idScene;
        result.well = "¡Bien hecho!, ¡Ya estás más cerca de acceder al laboratorio!";
        result.bad = "Que lástima... mira bien el número de veces que se repite cada acción...";
    }

    IEnumerator AnimateLevels(string trigger, float duration)
    {
        if (trigger != "" && duration > 0)
        {
            levels[currentLevel].transform.GetChild(1).GetComponent<Animator>().SetTrigger(trigger);
            yield return new WaitForSeconds(duration);
        }
        NextLevel();
    }

    IEnumerator AnimateFirstPartLevel(string trigger, float duration, List<int> dropdowns)
    {
        if (trigger != "" && duration > 0)
        {
            levels[currentLevel].transform.GetChild(1).GetComponent<Animator>().SetTrigger(trigger);
            yield return new WaitForSeconds(duration);
            if (currentLevel == 1)
            {
                AnimateLevel22(dropdowns[1]);
            }
            else if (currentLevel == 3)
            {
                AnimateLevel42(dropdowns[1]);
            }
        }

    }

}
