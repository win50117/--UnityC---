using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour 
{
    public GUISkin mySkin;
    public Texture BackGround;
    public MovieTexture StartMov;
    public Vector2 Button1Pos = new Vector2(0.32f, 0.7f);
    public Vector2 Button2Pos = new Vector2(0.68f, 0.7f);
    public Vector2 Button1Size = new Vector2(150, 70);
    public Vector2 Button2Size = new Vector2(150, 70);
    private bool Preload = false;

    void Start()
    {
        StartMov.Play();
    }

    void OnGUI()
    {
        GUI.skin = mySkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), StartMov, ScaleMode.StretchToFill);//繪製電影紋理
        
        if (Input.GetKey(KeyCode.Escape)) //按ESC跳過動畫
        {
            StartMov.Stop();
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackGround); //背景圖片
        }
        if (!StartMov.isPlaying) //如果開頭動畫停止播放
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackGround); //背景圖片
            /**建立進入遊戲按鈕 按下時進入預載模式**/
            if (GUI.Button(new Rect(Screen.width * Button1Pos.x - (Button1Size.x / 2), Screen.height * Button1Pos.y - (Button1Size.y / 2),
               Button1Size.x, Button1Size.y), "進入遊戲"))
            {
                Preload = true;
                Application.LoadLevel("1_map_home");
            }

            /**建立離開遊戲按鈕**/
            if (GUI.Button(new Rect(Screen.width * Button2Pos.x - (Button2Size.x / 2), Screen.height * Button2Pos.y - (Button2Size.y / 2),
               Button2Size.x, Button2Size.y), "離開遊戲"))
            {
                Application.Quit();
            }

            /**如果預載狀態為"是" 則在介面上顯示文字**/
            if (Preload == true)
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f + 150, 300, 80), "遊戲載入中 ...");
            }
        }
    }
}
