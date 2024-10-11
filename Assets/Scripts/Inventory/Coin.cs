using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "Coin System/New Coin")]
public class Coin : PersistentScriptableObject
{
    public int id;
    public bool isRecolected;
}
