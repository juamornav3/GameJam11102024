using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationPersistance", menuName = "Dialogue System/ConversationPersistance")]

public class ConversationPersistanceManager : PersistentScriptableObject
{
    public int id;
    public bool finished;
}
