public abstract class FSM_state<T> //���A�� �������O 
{
    public abstract void Enter(T entity); //���A�i�J��
    public abstract void Execute(T entity); //���A���椤 entity:����
    public abstract void Exit(T entity);  //���A�h�X��  
}