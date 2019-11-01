using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour 
{
    public GUITexture HP_bar; //ネ㏑兵瓜
    public GUIText HP_text;   //ネ㏑计ゅ
    public Creature imformation; //ネ戈癟script    

	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () 
    {                
        HP_bar.pixelInset = new Rect() //Rect痻
        {
            x = HP_bar.pixelInset.x, //x竚单竚
            y = HP_bar.pixelInset.y, //y竚单竚
            width = 196 * imformation.HP / imformation.Max_HP, //糴单 瓜糴196 * 逞緇﹀秖碭% (ヘ玡﹀秖/程﹀秖) 
            height = HP_bar.pixelInset.height //蔼单
        };
        HP_text.text = string.Format("{0:0}/{1:0}", imformation.HP, imformation.Max_HP);//{0:d}/{1:d} 0材把计 d俱计 
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 80, 150, 100), "ΙHP"))
        {
            imformation.HP-=10; //Ι﹀代刚
        }
    }
}
