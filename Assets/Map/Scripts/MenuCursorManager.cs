using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursorManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Set the cursor to the default
    }

}


