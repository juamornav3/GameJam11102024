using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle System/New Puzzle")]
public class PuzzleData : PersistentScriptableObject
{
    public int id;
    public bool finished;
}
