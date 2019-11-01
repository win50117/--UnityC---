using UnityEngine;
using System.Collections;

public class Dragon_State_CrazyMode : FSM_state<Dragon_AI>
{
    //**龍瘋狂狀態 20%HP**//
    float FlyCD, FlyTime; //飛行技能CD , 上飛時間
    float WaitTime, FAgainTime, RayTime;//等待時間
    bool MuzCount, MuzCount2;
    
    public override void Enter(Dragon_AI m)
    {
        m.DragonMode = 3; //模式3 狂暴狀態
        WaitTime = 0;
        FlyCD = 0;
        FlyTime = 0;
        RayTime = 0;
        MuzCount = false;
        MuzCount2 = false;
        Debug.Log("進入瘋狂MODE");
        m.SendMessage("Anim_Attack");
        m.SendMessage("Audio_CrazyMode");

        m.FireBall.GetComponent<boon>().Damage = 150; //火球傷害
        m.FireBall.GetComponent<boon>().speed = 50; //火球速度
        m.mCreature.AttackPower = 250;//進戰傷害
        m.Area_FireBall.GetComponent<boon>().Damage = 150;//火雨傷害
    }

    public override void Execute(Dragon_AI m)
    {
        FlyCD += Time.deltaTime;
        WaitTime += Time.deltaTime;
        m.FaceToPlayer();//面向玩家     

        if (FlyCD < 25) //飛行技能CD中
        {
            if (m.mCreature.HP / m.mCreature.Max_HP <= 0) //生命值為0時
            {
                m.ChangeState(new Dragon_State_EscapeMode());//切換狀態至AngryMode
            }

            if (WaitTime >= 3) //兩秒後正式執行動作
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
                    if (m.AreaFireCount < 30)//火雨累積未滿30顆
                    {
                        if (FAgainTime >= 0.15)//每0.15秒執行一次
                        {
                            m.Invoke("Area_Fire", 1);//延遲一秒發動                        
                            FAgainTime = 0;
                        }
                    }
                    else
                    {
                        m.AreaFireCD = 10;//火雨CD
                        m.AreaFireCount = 0;//火雨技術歸零
                        MuzCount = false;//音效播放歸零
                    }
                }
                else if (m.RayShootCD == 0 && RayTime <= 3) //光束砲
                {
                    if (RayTime == 0)
                    {
                        m.SendMessage("Anim_FireInSky");
                        m.SendMessage("Audio_RayShoot");
                        m.Ray_Area();
                        m.Invoke("Ray_Shoot", 1);
                    }
                    RayTime += Time.deltaTime;
                    if (RayTime > 3)
                    {
                        RayTime = 0;
                        m.RayShootCD = 20; //光束砲CD
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
                    m.FireAttackCD = 2;//火球CD               
                    m.Invoke("FireAttack", 1); //延遲1秒後發動 與動畫時間吻合
                }
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

    }
}