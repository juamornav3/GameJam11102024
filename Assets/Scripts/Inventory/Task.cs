using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objetive", menuName = "Objetive System/New Objetive")]
public class Task : PersistentScriptableObject
{
    public int id;
    public string description;
    public bool isActive;
    public bool isCompleted;   
}
