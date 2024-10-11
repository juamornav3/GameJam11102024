using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;

    public TextMeshProUGUI logLabel;

    private void Awake()
    {
        current = this; 
    }

    public static void Write(string message)
    {
        current.logLabel.text = message;
    }
}
