using UnityEngine;
using System.Collections;

public class Dragon_State_NormalMode : FSM_state<Dragon_AI>
{  
    //** 龍一般狀態 **//
    float FlyCD,FlyTime; //飛行技能CD , 上飛時間
    bool MuzCount;
    public override void Enter(Dragon_AI m)
    {
        m.DragonMode = 1; //模式1 一般狀態
        m.SendMessage("Anim_Attack");
        m.animation["Attack"].speed = 0.5f;
        m.SendMessage("Audio_Roar");
        MuzCount = false;
        FlyCD = 0;
        FlyTime = 0;

        m.FireBall.GetComponent<boon>().Damage = 50; //火球傷害
        m.FireBall.GetComponent<boon>().speed = 40; //火球速度
        m.mCreature.AttackPower = 150;//進戰傷害
    }

    public override void Execute(Dragon_AI m)
    {
        FlyCD += Time.deltaTime;
        m.FaceToPlayer();//面向玩家 
        
        if (FlyCD < 20) //飛行技能CD中
        {
            if (m.mCreature.HP / m.mCreature.Max_HP <= 0.5) //生命值低於50%時
            {
                m.ChangeState(new Dragon_State_AngryMode());//切換狀態至AngryMode
            }

            m.SendMessage("Anim_Idle");
            if (m.Distance() <= 10 && m.NearAttackCD == 0) //近距攻擊
            {
                m.SendMessage("Anim_Attack"); //播放動作
                m.SendMessage("Audio_Roar"); //音效
                m.NearAttack(); //攻擊
                m.NearAttackCD = 5; //攻擊CD
            }
            else if (m.NearAttackCD == 0)//CD到沒有近距離攻擊 再一次CD
            {
                m.NearAttackCD = 5; //攻擊CD
            }
            if (m.Distance() > 10 && m.FireAttackCD == 0) //火球攻擊
            {
                m.SendMessage("Anim_FireInGround"); //播放動作  
                m.FireAttackCD = 5;//火球CD     
                m.Invoke("FireAttack", 1); //延遲1秒後發動 與動畫時間吻合  
            }
        }
        else //飛行
        {
            if (MuzCount == false)
            {
                m.SendMessage("Audio_FlyIng");
                MuzCount = true;
            }
            FlyTime += Time.deltaTime;
            if (FlyTime <= 4)
            {
                m.SendMessage("Anim_FlyInSky");
                m.Invoke("FlyUP", 1); //延遲1秒後發動 和起飛動畫錯開 才能連貫            
            }
            else
            {
                m.ChangeState(new Dragon_State_FlyMode()); //切換狀態至飛行Mode
            }
        }
    }

    public override void Exit(Dragon_AI m)
    {

    }
}
