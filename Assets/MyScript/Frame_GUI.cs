using UnityEngine;
using System.Collections;

public class Frame_GUI : MonoBehaviour 
{   
    public Vector2 baseScale = new Vector2(1920.0f, 1080.0f);//基礎解析度    
    public Texture2D HP_Bar_BG;//圓形血條後貼圖
    public Texture2D HP_Bar_FG;//圓形血條前貼圖  
    public Creature imformation; //生物資訊script       
    private Vector2 size; //縮放比例
    private string HP_Text; //血量字串
    private GUIStyle myStyle = new GUIStyle(); //GUI字型樣式   
    public int RespawnTime = 3;//復活次數

	// Use this for initialization      
    void Awake() //比Start還早執行 
    {
        /*將所有GUITextur全部一起照比例縮放*/
        GUITexture[] guis = FindObjectsOfType(typeof(GUITexture)) as GUITexture[]; //找尋所有GUITexture型態 並放入陣列內        
        size.x = Screen.width / baseScale.x; //寬的縮放比例
        size.y = Screen.height / baseScale.y;//高的縮放比例
        for (int i = 0; i < guis.Length; i++)//將每個GUITexture依照比例變更位置和大小
        {
            guis[i].pixelInset = new Rect(guis[i].pixelInset.x * size.x, guis[i].pixelInset.y * size.y, 
                                          guis[i].pixelInset.width * size.x, guis[i].pixelInset.height * size.y);
        }        
	}	

	// Update is called once per frame
	void Update ()
    {        
        HP_Text = imformation.HP.ToString();
        if (imformation.HP > imformation.Max_HP)
        {
            imformation.HP = imformation.Max_HP;
        }
	}
    void OnGUI()
    {
        myStyle.alignment = TextAnchor.UpperCenter;//文字置中
        myStyle.normal.textColor = Color.white; //字型顏色
        myStyle.fontSize = Screen.width / 100 +5; //字型大小
        
        /*繪製圓形血量*/
        GUI.BeginGroup(new Rect((Screen.width / 2) - (51 * size.x), Screen.height - 130 * size.y, 97 * size.x, 98 * size.y));//GUI群組起始位置及大小計算依比例
        if (HP_Bar_FG)
            GUI.DrawTexture(new Rect(0, 0, 97 * size.x, 98 * size.y ), HP_Bar_FG);  //確認有無底貼圖 有就繪製血量前貼圖
        GUI.BeginGroup(new Rect(0f, 0f, 97*size.x, 98 * size.y * (1f - imformation.HP / imformation.Max_HP)));//群組內的群組 改變大小來改變血量
        if (HP_Bar_BG)
            GUI.DrawTexture(new Rect(0, 0, 97 * size.x, 98 * size.y ), HP_Bar_BG);   //確認有無前貼圖 有就繪製血量後貼圖
        GUI.EndGroup(); //結束GUI群組 和BeginGroup成對
        GUI.EndGroup(); //結束第二個GUI群組  

        GUI.Label(new Rect(Screen.width / 2 - 3 * size.x, Screen.height - 96 * size.y, 0, 0), HP_Text, myStyle);//血量文字

        
        /**測試**/
        if (GUI.Button(new Rect(10, 80, 150, 100), "扣HP"))
        {
            imformation.HP -= 100; //扣血測試
        }

        if (imformation.HP == 0 && RespawnTime > 0) //生命值歸零時
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) -50, 150, 100), "復活次數:" + RespawnTime))
            {
                RespawnTime--;
                imformation.HP = imformation.Max_HP;                              
                imformation.GetComponent<PlayerControll>().enabled = true;
                imformation.GetComponent<Player_Weapon_AI>().enabled = true;
                imformation.GetComponent<Player_Anim>().enabled = true;
                imformation.animation["Dead"].layer = 0;
                imformation.tag = "Player";
            }
        }
        else if (imformation.HP == 0 && RespawnTime == 0)
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 50, 150, 100), "遊戲結束!"))
            {
                Application.Quit();
            }
        }
    }  
}
