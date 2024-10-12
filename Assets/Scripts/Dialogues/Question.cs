using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Choices {
    [TextArea(1, 3)]
    public string choice;
    public Conversation result;
}

[CreateAssetMenu(fileName ="Question", menuName ="Dialogue System/New Question")]
public class Question : ScriptableObject
{
    [TextArea(1, 3)]
    public string question;
 
    public Choices[] choices;
}
