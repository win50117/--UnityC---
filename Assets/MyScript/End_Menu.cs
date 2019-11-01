using UnityEngine;
using System.Collections;

public class End_Menu : MonoBehaviour 
{
    public MovieTexture EndMov;
    public GUISkin mySkin;
    public Vector2 Button1Pos = new Vector2(0.5f, 0.7f);
    public Vector2 Button1Size = new Vector2(150, 70);

	// Use this for initialization
	void Start () 
    {
        EndMov.Play();	
	}
	
	// Update is called once per frame
    void OnGUI()
    {
        GUI.skin = mySkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), EndMov, ScaleMode.StretchToFill);//繪製電影紋理

        if (!EndMov.isPlaying) //如果開頭動畫停止播放
        {
            /**建立離開遊戲按鈕**/
            if (GUI.Button(new Rect(Screen.width * Button1Pos.x - (Button1Size.x / 2), Screen.height * Button1Pos.y - (Button1Size.y / 2),
               Button1Size.x, Button1Size.y), "The End"))
            {
                Application.Quit();
            }
        }
	
	}
}
