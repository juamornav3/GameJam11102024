using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Result", menuName = "Puzzle System/New Result")]
public class Result : ScriptableObject
{
    public string well;
    public string bad;
    public bool result;
    public int idPuzle;
    public int idScene;

}
