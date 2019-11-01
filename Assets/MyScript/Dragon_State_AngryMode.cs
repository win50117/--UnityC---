using UnityEngine;
using System.Collections;

public class Dragon_State_AngryMode : FSM_state<Dragon_AI>
{
    //**龍生氣狀態 50%HP**//
    float FlyCD, FlyTime; //飛行技能CD , 上飛時間
    float WaitTime, FAgainTime;//等待時間
    bool MuzCount,MuzCount2;
    public override void Enter(Dragon_AI m)
    {
        m.DragonMode = 2; //模式2 生氣狀態
        WaitTime = 0;
        FlyCD = 0;
        FlyTime = 0;
        MuzCount = false;
        MuzCount2 = false;
        Debug.Log("進入生氣MODE");
        m.SendMessage("Anim_Attack");        
        m.SendMessage("Audio_AngryMode");

        m.FireBall.GetComponent<boon>().Damage = 100;//火球傷害
        m.FireBall.GetComponent<boon>().speed = 45; //火球速度
        m.mCreature.AttackPower = 200;//進戰傷害
    }

    public override void Execute(Dragon_AI m)
    {
        FlyCD += Time.deltaTime;
        WaitTime += Time.deltaTime;
        m.FaceToPlayer();//面向玩家 

        if (FlyCD < 25 ) //飛行技能CD中
        {
            if (m.mCreature.HP / m.mCreature.Max_HP <= 0.2) //生命值低於20%時
            {
                m.ChangeState(new Dragon_State_CrazyMode());//切換狀態至CrazyMode
            }            
            if (WaitTime >= 3) //三秒後正式執行動作
            {
                Debug.Log(m.AreaFireCD);
                m.SendMessage("Anim_Idle");
                
                if (m.AreaFireCD == 0)
                {
                    if (MuzCount == false)
                    {
                        m.SendMessage("Audio_AreaFire");
                        MuzCount = true;
                    }
                    m.SendMessage("Anim_FlyInSky");
                    FAgainTime += Time.deltaTime;
                    if (m.AreaFireCount < 20)//火雨累積未滿20顆
                    {
                        if (FAgainTime >= 0.2)//每0.2秒執行一次
                        {
                            m.Invoke("Area_Fire", 1);//延遲一秒發動                        
                            FAgainTime = 0;
                        }
                    }
                    else
                    {
                        m.AreaFireCD = 10;//火雨CD
                        m.AreaFireCount = 0;//火雨計數歸零
                        MuzCount = false;//音效播放歸零
                    }
                }
                else if (m.Distance() <= 10 && m.NearAttackCD == 0)
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
                else if (m.Distance() > 10 && m.FireAttackCD == 0)
                {
                    m.SendMessage("Anim_FireInGround"); //播放動作
                    m.FireAttackCD = 3.5f;//火球CD               
                    m.Invoke("FireAttack", 1); //延遲1秒後發動 與動畫時間吻合
                }
                WaitTime = 4;
            }
        }
        else //飛行
        {
            if (MuzCount2 == false)
            {
                m.SendMessage("Audio_FlyIng");
                MuzCount2 = true;
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
        WaitTime = 0;
    }
}
