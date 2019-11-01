using UnityEngine;
using System.Collections;

public class Dragon_Anim : MonoBehaviour //
{
	void Start () 
    {
        animation["Idle"].layer = 0;
        animation["Attack"].layer = 1;
        animation["Fly"].layer = 3;
        animation["FlyInSky"].layer = 1;
        animation["FireInSky"].layer = 2;
        animation["FireInGround"].layer = 1;
	}

    public void Anim_Attack()
    {
        animation.CrossFade("Attack", 0.2f);//漸變播放攻擊動畫 
    }

    public void Anim_Idle()
    {
        animation.CrossFade("Idle",0.4f);
    }

    public void Anim_Fly()
    {
        animation.CrossFade("Fly",0.4f);
    }

    public void Anim_FlyInSky()
    {
        animation.CrossFade("FlyInSky",0.4f);
    }

    public void Anim_FireInSky()
    {        
        animation.CrossFade("FireInSky", 0.2f);
    }

    public void Anim_FireInGround()
    {
        SendMessage("Audio_FireBall"); //傳遞訊息 執行吐火音效
        animation.CrossFade("FireInGround", 0.2f);
        //animation["FireInGroun"].speed = 2.0f; 
    }
}
