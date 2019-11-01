using UnityEngine;
using System.Collections;

public enum Locations { goldmine, bar, bank, home }; 

public class FSM_AI : MonoBehaviour 
{
      private FSM_Machine<FSM_AI> FSM; //�ŧiFSM_Machine<FSM_AI>

      public Locations  Location = Locations.goldmine;
      public int        GoldCarried = 0;
      public int        MoneyInBank  = 0;
      public int        Thirst = 0;
      public int        Fatigue = 0;

      public void Awake() 
      {
          Debug.Log("Miner awakes...");
          FSM = new FSM_Machine<FSM_AI>();
          FSM.Configure(this, new FSM_AI2()); //��l�t�m ���A��AI2
      }

      public void ChangeState(FSM_state<FSM_AI> e)
      {
          FSM.ChangeState(e);
      }

      public void Update() 
      {
          Thirst++;
          FSM.Update(); //������檬�A����Update() ����e���A�M�������A
      }

      public void ChangeLocation(Locations l) 
      {
          Location = l;
      }

      public void AddToGoldCarried(int amount) 
      {
          GoldCarried += amount;
      }

      public void AddToMoneyInBank(int amount ) 
      {
          MoneyInBank += amount;
          GoldCarried = 0;
      }

      public bool RichEnough() 
      {
          return false;
      }

      public bool PocketsFull() 
      {
          bool full = GoldCarried ==  2 ? true : false;
          return full;
      }

      public bool Thirsty() 
      {
          bool thirsty = Thirst == 10 ? true : false;
          return thirsty;
      }

      public void IncreaseFatigue() 
      {
          Fatigue++;
      }
}