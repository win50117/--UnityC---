using UnityEngine;
using System.Collections;

public class Frame_GUI : MonoBehaviour 
{   
    public Vector2 baseScale = new Vector2(1920.0f, 1080.0f);//��¦�ѪR��    
    public Texture2D HP_Bar_BG;//��Φ����K��
    public Texture2D HP_Bar_FG;//��Φ���e�K��  
    public Creature imformation; //�ͪ���Tscript       
    private Vector2 size; //�Y����
    private string HP_Text; //��q�r��
    private GUIStyle myStyle = new GUIStyle(); //GUI�r���˦�   
    public int RespawnTime = 3;//�_������

	// Use this for initialization      
    void Awake() //��Start�٦����� 
    {
        /*�N�Ҧ�GUITextur�����@�_�Ӥ���Y��*/
        GUITexture[] guis = FindObjectsOfType(typeof(GUITexture)) as GUITexture[]; //��M�Ҧ�GUITexture���A �é�J�}�C��        
        size.x = Screen.width / baseScale.x; //�e���Y����
        size.y = Screen.height / baseScale.y;//�����Y����
        for (int i = 0; i < guis.Length; i++)//�N�C��GUITexture�̷Ӥ���ܧ��m�M�j�p
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
        myStyle.alignment = TextAnchor.UpperCenter;//��r�m��
        myStyle.normal.textColor = Color.white; //�r���C��
        myStyle.fontSize = Screen.width / 100 +5; //�r���j�p
        
        /*ø�s��Φ�q*/
        GUI.BeginGroup(new Rect((Screen.width / 2) - (51 * size.x), Screen.height - 130 * size.y, 97 * size.x, 98 * size.y));//GUI�s�հ_�l��m�Τj�p�p��̤��
        if (HP_Bar_FG)
            GUI.DrawTexture(new Rect(0, 0, 97 * size.x, 98 * size.y ), HP_Bar_FG);  //�T�{���L���K�� ���Nø�s��q�e�K��
        GUI.BeginGroup(new Rect(0f, 0f, 97*size.x, 98 * size.y * (1f - imformation.HP / imformation.Max_HP)));//�s�դ����s�� ���ܤj�p�ӧ��ܦ�q
        if (HP_Bar_BG)
            GUI.DrawTexture(new Rect(0, 0, 97 * size.x, 98 * size.y ), HP_Bar_BG);   //�T�{���L�e�K�� ���Nø�s��q��K��
        GUI.EndGroup(); //����GUI�s�� �MBeginGroup����
        GUI.EndGroup(); //�����ĤG��GUI�s��  

        GUI.Label(new Rect(Screen.width / 2 - 3 * size.x, Screen.height - 96 * size.y, 0, 0), HP_Text, myStyle);//��q��r

        
        /**����**/
        if (GUI.Button(new Rect(10, 80, 150, 100), "��HP"))
        {
            imformation.HP -= 100; //�������
        }

        if (imformation.HP == 0 && RespawnTime > 0) //�ͩR���k�s��
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) -50, 150, 100), "�_������:" + RespawnTime))
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
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 50, 150, 100), "�C������!"))
            {
                Application.Quit();
            }
        }
    }  
}
