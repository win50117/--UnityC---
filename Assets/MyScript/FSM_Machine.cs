public class FSM_Machine<T>
{    
    private T Owner;
    private FSM_state<T> CurrentState; //宣告當前狀態  型態為FSM_state<T>抽象類別
    private FSM_state<T> PreviousState;//宣告前一個狀態
    private FSM_state<T> GlobalState;  //宣告全局狀態

    public void Awake() //喚醒 在start()前執行
    {
        CurrentState = null;
        PreviousState = null;
        GlobalState = null;
    }

    public void Configure(T owner, FSM_state<T> InitialState) //Initial State:初始狀態   Configure:配置
    {
        Owner = owner;
        ChangeState(InitialState); //切換狀態為初始狀態
    }

    public void Update() //每一幀都會執行
    {
        if (GlobalState != null) GlobalState.Execute(Owner);  //全局狀態不等於null時 就執行全局狀態的Execute事件
        if (CurrentState != null) CurrentState.Execute(Owner);//當前狀態不等於null時 就執行當前狀態的Execute事件
    }

    public void ChangeState(FSM_state<T> NewState) //切換狀態 代入型態FSM_state<T> 名叫NewState
    {
        PreviousState = CurrentState; //前一個狀態等於當前狀態 紀錄狀態
        if (CurrentState != null) //如果當前狀態不等於null 就執行當前狀態的Exit事件 Owner為目標實體
            CurrentState.Exit(Owner);
        CurrentState = NewState;  //當前狀態變更為新狀態
        if (CurrentState != null) //變更後若不是null 就執行當前狀態的Enter事件
            CurrentState.Enter(Owner);
    }

    public void RevertToPreviousState() //回復至上一個狀態
    {
        if (PreviousState != null) //若上一個狀態不等於null 就切換至上一個狀態
            ChangeState(PreviousState); 
    }
};