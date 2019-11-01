using UnityEngine;
using System.Collections;

public class Dragon_State_EscapeMode : FSM_state<Dragon_AI>
{
    //** 龍逃脫狀態 **//
    float FlyCD, FlyTime; //飛行技能CD , 上飛時間
    bool MuzCount;
    public override void Enter(Dragon_AI m)
    {
        MuzCount = false;
        FlyCD = 0;
        FlyTime = 0;
        m.SendMessage("Anim_Attack");
        m.animation["Attack"].speed = 0.5f;
        m.SendMessage("Audio_Roar");
    }

    public override void Execute(Dragon_AI m)
    {
        FlyCD += Time.deltaTime;
         
        if (FlyCD > 2 && FlyCD <= 21) //飛行
        {
            if (MuzCount == false)
            {
                m.SendMessage("Audio_FlyIng");
                MuzCount = true;
            }
            FlyTime += Time.deltaTime;
            if (FlyTime <= 5)
            {
                m.SendMessage("Anim_FlyInSky");
                m.Invoke("FlyUP", 1); //延遲1秒後發動 和起飛動畫錯開 才能連貫  
                m.transform.Rotate(0,0.5f,0);
            }
            else
            {
                m.transform.Translate(0, 0, 0.1f);                              
            }
        }
        else if (FlyCD > 21)
        {
            m.Escape();
        }
    }

    public override void Exit(Dragon_AI m)
    {

    }
}
