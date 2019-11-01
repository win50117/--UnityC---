using UnityEngine;
using System.Collections;

public sealed class Player_Sword : FSM_state<Player_Weapon_AI> //�~�Ӧ�state
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
        //Debug.Log("�{�b���ϥμC!!");        
        if (m.Weapon_CD == 0) //�Z������CD��0�ɤ~�i�H����
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
            m.Attack_CD = 1;//����CD
            m.SwordAttack(); //�o�ʴ���
        }
        if (Input.GetKey(KeyCode.Alpha1) && m.SwordSkillCD_1 == 0)
        {
            m.SwordSkillCD_1 = 2;//��1�ޯ�CD�ɶ�
            m.SwordSkill1(); //�o�ʧޯ�1
        }
        if (Input.GetKey(KeyCode.Alpha2) && m.SwordSkillCD_2 == 0)
        {
            m.SwordSkillCD_2 = 5;//��2�ޯ�CD�ɶ�
            m.SwordSkill2(); //�o�ʧޯ�2
        }
        if (Input.GetKey(KeyCode.Alpha3) && m.SwordSkillCD_3 == 0)
        {
            m.SwordSkillCD_3 = 7;//��3�ޯ�CD�ɶ�
            m.SwordSkill3(); //�o�ʧޯ�3
        }
        if(Input.GetKey(KeyCode.Alpha4) && m.SwordSkillCD_4 == 0)
        {
            m.SwordSkillCD_4 = 1;//��4�ޯ�CD�ɶ�           
            m.SwordSkill4(); //�o�ʧޯ�4            
        }
    }

    public override void Exit(Player_Weapon_AI m)
    {
        Debug.Log("�����Z��");
    }
}
