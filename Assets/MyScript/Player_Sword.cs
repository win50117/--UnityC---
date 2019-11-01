using UnityEngine;
using System.Collections;

public sealed class Player_Sword : FSM_state<Player_Weapon_AI> //繼承自state
{
    public override void Enter(Player_Weapon_AI m)
    {
        m.Weapon_CD = 2;
        m.GUI_Sword_skill.SetActive(true);
        m.GUI_Claw_skill.SetActive(false);
        m.GUI_Hammer_skill.SetActive(false);
        m.GUI_Whip_skill.SetActive(false);
    }

    public override void Execute(Player_Weapon_AI m)
    {       
        //Debug.Log("現在正使用劍!!");        
        if (m.Weapon_CD == 0) //武器切換CD為0時才可以切換
        {
            if (Input.GetKey(KeyCode.F2))
                m.ChangeState(new Player_Claw());
            if (Input.GetKey(KeyCode.F3))
                m.ChangeState(new Player_Hammer());
            if (Input.GetKey(KeyCode.F4))
                m.ChangeState(new Player_Whip());
        }
        if (Input.GetMouseButtonDown(0) && m.Attack_CD == 0)
        {
            m.Attack_CD = 1;//普攻CD
            m.SwordAttack(); //發動普攻
        }
        if (Input.GetKey(KeyCode.Alpha1) && m.SwordSkillCD_1 == 0)
        {
            m.SwordSkillCD_1 = 2;//第1技能CD時間
            m.SwordSkill1(); //發動技能1
        }
        if (Input.GetKey(KeyCode.Alpha2) && m.SwordSkillCD_2 == 0)
        {
            m.SwordSkillCD_2 = 5;//第2技能CD時間
            m.SwordSkill2(); //發動技能2
        }
        if (Input.GetKey(KeyCode.Alpha3) && m.SwordSkillCD_3 == 0)
        {
            m.SwordSkillCD_3 = 7;//第3技能CD時間
            m.SwordSkill3(); //發動技能3
        }
        if(Input.GetKey(KeyCode.Alpha4) && m.SwordSkillCD_4 == 0)
        {
            m.SwordSkillCD_4 = 1;//第4技能CD時間           
            m.SwordSkill4(); //發動技能4            
        }
    }

    public override void Exit(Player_Weapon_AI m)
    {
        Debug.Log("切換武器");
    }
}
