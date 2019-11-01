using UnityEngine;
using System.Collections;

public sealed class FSM_AI2 : FSM_state<FSM_AI> 
{
    public override void Enter(FSM_AI m) 
    {        
        if (m.Location != Locations.goldmine) //如果位置不等於goldmine才執行
        {
            Debug.Log("Entering the mine...");
            m.ChangeLocation(Locations.goldmine);
        }
    }

    public override void Execute(FSM_AI m)
    {
        m.AddToGoldCarried(1);
        Debug.Log("Picking ap nugget and that's..." + m.GoldCarried);
        m.IncreaseFatigue();
        if (m.PocketsFull())
            m.ChangeState(new FSM_AI3());        
    }

    public override void Exit(FSM_AI m)
    {
        Debug.Log("Leaving the mine with my pockets full...");
    }
}
