using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public int cursorSize = 64;
    public Texture2D gameCursor, actionCursor;
    Texture2D activeCursor;

   
    void Start()
    {
        Cursor.visible = false;
        ChangeCursor("gameCursor");
    }

    public void ChangeCursor(string cursorType)
    {
        if (cursorType=="gameCursor")
        {
            activeCursor = gameCursor;
        }
        if (cursorType == "actionCursor")
        {
            activeCursor = actionCursor;
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorSize/2, Screen.height - (Input.mousePosition.y + cursorSize/2), cursorSize,cursorSize),activeCursor);
    }

}
