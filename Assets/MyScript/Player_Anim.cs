using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player_Audio))]
public class Player_Anim : MonoBehaviour 
{  
    void Start()
    {
        //animation.wrapMode = WrapMode.Loop;        
        //動作階層 數字越大的會覆蓋小的
        animation["Idle"].layer = 0;
        animation["Run"].layer = 0;
        animation["Attack"].layer = 3;
        animation["Jump"].layer = 1;
        animation["OnHit"].layer = 1;
        animation["Skill_1"].layer = 3;
        animation["Skill_2"].layer = 3;
        animation["Skill_3"].layer = 3;
        animation["Skill_4"].layer = 3;
        animation["Dead"].layer = 2; 
    }
    // Update is called once per frame
    void Update()
    {        
        //如果輸入設定中的垂直鍵(WS鍵或上下鍵)數值大於絕對值0.2時(角色移動時)
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.5)
        {

            animation.CrossFade("Run", 0.2f);//漸變播放跑步動畫     
            if (Input.GetAxis("Vertical") < -0.2)//如果垂直鍵(WS鍵或上下鍵)的數值小於0.2(後退)
            {
                animation.CrossFade("Back_Walk", 0.2f);
            }
        }
        //如果輸入設定中的水平鍵(AD鍵或左右鍵)數值大於絕對值0.2時(角色移動時)
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2)
        {
            if (Input.GetKey(KeyCode.A))
            {
                animation.CrossFade("Left_Walk", 0.2f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animation.CrossFade("Right_Walk", 0.2f);
            } 
        }
        //如果輸入設定中的水平鍵(AD鍵或左右鍵)或是垂直鍵(WS鍵或上下鍵)數值沒有大於絕對值0.2時(角色停止時)
        else
        {
            animation.CrossFade("Idle");//漸變播放待機動畫
        }        
    }

    public void Anim_Attack()
    {
        SendMessage("Audio_Attack"); //傳遞訊息 執行普攻音效
        animation.CrossFade("Attack", 0.2f);//漸變播放攻擊動畫 
    }
    public void Anim_Jump()
    {
        //漸變播放跳躍動畫 --> 設定跳躍動畫的速度 
        animation.CrossFade("Jump",0.2f);
        animation["Jump"].speed = 1.5f; 
    }
    public void Anim_Skill_1()
    {
        SendMessage("Audio_Skill_1");
        animation.CrossFade("Skill_1");
        animation["Skill_1"].speed = 2.5f; 
    }
    public void Anim_Skill_2()
    {
        SendMessage("Audio_Skill_2");
        animation.CrossFade("Skill_2");
        animation["Skill_2"].speed = 1.2f; 
    }
    public void Anim_Skill_3()
    {
        SendMessage("Audio_Skill_3");
        animation.CrossFade("Skill_3");
        animation["Skill_3"].speed = 1.5f; 
    }
    public void Anim_Skill_4()
    {
        SendMessage("Audio_Skill_4");
        animation.CrossFade("Skill_4");
        animation["Skill_4"].speed = 0.5f; 
    }
    public void Anim_Dead()
    {
        animation.CrossFade("Dead");        
    }
}


