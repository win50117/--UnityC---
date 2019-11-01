using UnityEngine;
using System.Collections;

public class HideMouse : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Screen.showCursor = false;
        Screen.lockCursor = true;
	}
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
        }        
        if (Input.GetMouseButtonUp(0)) //按下左鍵彈起後
        {
            Screen.showCursor = false;
            Screen.lockCursor = true; 
        }
    }
}
