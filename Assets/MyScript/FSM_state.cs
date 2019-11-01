public abstract class FSM_state<T> //狀態基本 虛擬類別 
{
    public abstract void Enter(T entity); //狀態進入時
    public abstract void Execute(T entity); //狀態執行中 entity:實體
    public abstract void Exit(T entity);  //狀態退出時  
}