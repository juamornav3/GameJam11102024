using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Conversation", menuName ="Dialogue System/Conversation")]
public class Conversation : PersistentScriptableObject
{
    [System.Serializable]
    public struct Line
    {
        public Character character;

        public AudioClip sound;

        [TextArea(1,3)]
        public string dialogue;
    }

    public bool unlocked;
    public bool finished;
    public bool reuse;
  
    public Line[] dialogues;

    public Question question;
   

}
