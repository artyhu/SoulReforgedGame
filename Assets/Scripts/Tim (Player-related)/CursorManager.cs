using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    [SerializeField] private Texture2D cursorTexure;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorPos = new Vector2(cursorTexure.width / 2, cursorTexure.height / 2);
        Cursor.SetCursor(cursorTexure, cursorPos, CursorMode.Auto);
    }

    void Update() {
        if (PauseMenuController.isPaused)
            {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
        }
        else {
            Vector2 cursorPos = new Vector2(cursorTexure.width / 2, cursorTexure.height / 2);
            Cursor.SetCursor(cursorTexure, cursorPos, CursorMode.Auto);
        }
        
    }
}
