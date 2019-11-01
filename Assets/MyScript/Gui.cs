using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour 
{
    public GUITexture HP_bar; //�ͩR���Ϥ�
    public GUIText HP_text;   //�ͩR�ƭȤ�r
    public Creature imformation; //�ͪ���Tscript    

	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () 
    {                
        HP_bar.pixelInset = new Rect() //Rect�x��
        {
            x = HP_bar.pixelInset.x, //x��m������m
            y = HP_bar.pixelInset.y, //y��m������m
            width = 196 * imformation.HP / imformation.Max_HP, //�e�׵��� �Ϥ��e196 * �Ѿl��q�X% (�ثe��q/�̤j��q) 
            height = HP_bar.pixelInset.height //�������j�p
        };
        HP_text.text = string.Format("{0:0}/{1:0}", imformation.HP, imformation.Max_HP);//{0:d}/{1:d} 0��Ĥ@�ӰѼ� d���� 
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 80, 150, 100), "��HP"))
        {
            imformation.HP-=10; //�������
        }
    }
}
