using UnityEngine;
using System.Collections;

public sealed class FSM_AI3 : FSM_state<FSM_AI>
{    
    public override void Enter(FSM_AI m)
    {
        if (m.Location != Locations.bank)
        {
            Debug.Log("Entering the bank...");
            m.ChangeLocation(Locations.bank);
        }
    }

    public override void Execute(FSM_AI m)
    {
        Debug.Log("Feeding The System with MY gold... " + m.MoneyInBank);
        m.AddToMoneyInBank(m.GoldCarried);
        m.ChangeState(new FSM_AI2());        
    }

    public override void Exit(FSM_AI m)
    {
        Debug.Log("Leaving the bank...");
    }
}
