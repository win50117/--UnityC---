using UnityEngine;
using System.Collections;

public class Player_Whip : FSM_state<Player_Weapon_AI> //繼承自state
{
    public override void Enter(Player_Weapon_AI m)
    {
        m.Weapon_CD = 2;
        m.GUI_Sword_skill.SetActive(false);
        m.GUI_Claw_skill.SetActive(false);
        m.GUI_Hammer_skill.SetActive(false);
        m.GUI_Whip_skill.SetActive(true);
    }

    public override void Execute(Player_Weapon_AI m)
    {
        //Debug.Log("現在正使用鞭子!!");
        m.Weapon_CD_Count();
        if (m.Weapon_CD == 0) //武器切換CD為0時才可以切換
        {
            if (Input.GetKey(KeyCode.F1))
                m.ChangeState(new Player_Sword());
            if (Input.GetKey(KeyCode.F2))
                m.ChangeState(new Player_Claw());
            if (Input.GetKey(KeyCode.F3))
                m.ChangeState(new Player_Hammer());
        }        
    }

    public override void Exit(Player_Weapon_AI m)
    {
        Debug.Log("切換武器");
    }
}
