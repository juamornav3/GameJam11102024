using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle System/New Hint")]
public class HintData : PersistentScriptableObject
{
    public int price;
    public string hint;
    public bool sold;
}
