using UnityEngine;
using System.Collections;

public class Dragon_State_FlyMode : FSM_state<Dragon_AI>
{
    float FlyCD, FlyTime;
    bool MuzCount;
    public override void Enter(Dragon_AI m)
    {
        MuzCount = false;
        FlyCD = 0;
        FlyTime = 0;

        if (m.DragonMode == 1)//一般狀態
        {
            m.FireBall.GetComponent<boon>().Damage = 100;          
        }
        else if (m.DragonMode == 2)//生氣狀態
        {
            m.FireBall.GetComponent<boon>().Damage = 150;            
        }
        else if (m.DragonMode == 3)//瘋狂狀態
        {
            m.FireBall.GetComponent<boon>().Damage = 200;            
        }
    }

    public override void Execute(Dragon_AI m)
    {
        FlyCD += Time.deltaTime;
        m.FaceToPlayer();//面向玩家  
        m.SendMessage("Anim_FlyInSky");
        
        if (FlyCD < 10) //飛行技能CD中
        {
            if (m.Distance() > 10 && m.FireAttackCD == 0)
            {
                m.SendMessage("Anim_FireInSky"); //播放動作
                m.SendMessage("Audio_Roar");                           
                m.Invoke("FireAttack", 1); //延遲1秒後發動 與動畫時間吻合
                if (m.DragonMode == 1)//一般狀態
                {
                    m.FireAttackCD = 3;//火球CD   
                }
                else if (m.DragonMode == 2)//生氣狀態
                {
                    m.FireAttackCD = 2;//火球CD    
                }
                else if (m.DragonMode == 3)//瘋狂狀態
                {
                    m.FireAttackCD = 1;//火球CD    
                }
            }
        }
        else //下飛行回地板
        {
            if (MuzCount == false)
            {
                m.SendMessage("Audio_FlyIng");
                MuzCount = true;
            }
            FlyTime += Time.deltaTime;
            if (FlyTime <= 3.9)
            {
                m.SendMessage("Anim_FlyInSky");
                m.FlyDown();             
            }
            else
            {
                m.FixPosition(); //座標修正
                m.RevertToPreviousState(); //回上一個狀態
            }
        }
    }

    public override void Exit(Dragon_AI m)
    {

    }
}
