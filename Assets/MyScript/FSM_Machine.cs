public class FSM_Machine<T>
{    
    private T Owner;
    private FSM_state<T> CurrentState; //�ŧi��e���A  ���A��FSM_state<T>��H���O
    private FSM_state<T> PreviousState;//�ŧi�e�@�Ӫ��A
    private FSM_state<T> GlobalState;  //�ŧi�������A

    public void Awake() //��� �bstart()�e����
    {
        CurrentState = null;
        PreviousState = null;
        GlobalState = null;
    }

    public void Configure(T owner, FSM_state<T> InitialState) //Initial State:��l���A   Configure:�t�m
    {
        Owner = owner;
        ChangeState(InitialState); //�������A����l���A
    }

    public void Update() //�C�@�V���|����
    {
        if (GlobalState != null) GlobalState.Execute(Owner);  //�������A������null�� �N����������A��Execute�ƥ�
        if (CurrentState != null) CurrentState.Execute(Owner);//��e���A������null�� �N�����e���A��Execute�ƥ�
    }

    public void ChangeState(FSM_state<T> NewState) //�������A �N�J���AFSM_state<T> �W�sNewState
    {
        PreviousState = CurrentState; //�e�@�Ӫ��A�����e���A �������A
        if (CurrentState != null) //�p�G��e���A������null �N�����e���A��Exit�ƥ� Owner���ؼй���
            CurrentState.Exit(Owner);
        CurrentState = NewState;  //��e���A�ܧ󬰷s���A
        if (CurrentState != null) //�ܧ��Y���Onull �N�����e���A��Enter�ƥ�
            CurrentState.Enter(Owner);
    }

    public void RevertToPreviousState() //�^�_�ܤW�@�Ӫ��A
    {
        if (PreviousState != null) //�Y�W�@�Ӫ��A������null �N�����ܤW�@�Ӫ��A
            ChangeState(PreviousState); 
    }
};