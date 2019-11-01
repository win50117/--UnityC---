using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(AudioSource))]

public class AI : MonoBehaviour 
{
    public enum EnemyState
    {      
        Idle, //�ݩR  
        Patrol, //����
        Attack, //����  
        Dead, //���`        
        Homing, //�k��
    } 

    public float MoveSpeed = 4; //���ʳt��    
    public float RotateSpeed = 3; //����t��
    public float AttackSpeed = 1.0f; //�����t��(�N�o)
    public float AttackRange = 3.0f; //�����d��
    public float SightRange = 10.0f; //�����Z��
    public int Attack_type = 1; //�����Ҧ� 1�i��  2����  3����w�I������
    
    private float MoveTime; //�H������CD
    private float PatrolTime; //���ʫ���ɶ�
    private float rand_x, rand_y, rand_z; //�üƭ� (x,y:��V z:�Z��) 
    private Transform myTransform;

    public EnemyState State = EnemyState.Idle; //���A
    private float mAttackCooldown; //�����N�o�ɶ�    
    private Vector3 HomePosition; //���I�y��

    private CharacterController mController; //���
    private Creature mCreature; //�ͪ���T    
    private Transform TargetTransform; //�ؼ��ܫ���T (��m) 
    public AudioClip Audio_Attack;//��������
 
    //�Ω󻷵{����//
    public GameObject Boom; //�ŧi�C������Boom �Ψӥͦ��l�u
    private Transform AttackFix; //�ץ����{������V
    public GameObject FixObject; //������V�ץ����� �إ�EmptyObject���L�`���ؼЪ�

    void Awake()
    {
      //  GameObject player = GameObject.FindGameObjectWithTag("Player"); //�ŧi�C������player = ��MTag��Player������
      //  TargetTransform = player.transform; //�ؼЮy�� ���󪱮a�y��

        mCreature = GetComponent<Creature>(); //�ͪ���T
        mController = GetComponent<CharacterController>(); //���ⱱ�
    }
    void Start()
    {
        animation["Idle"].layer = 0;
        animation["Run"].layer = 0;
        animation["OnHit"].layer = 1;
        animation["Dead"].layer = 1;
        animation["Attack"].layer = 1;
        animation["OnHit"].speed = 3f;
        myTransform = transform;
        mController.slopeLimit = 60.0f; //�i���שY�X��  
        HomePosition = myTransform.position; //���I�y�Ь���  
        //�H����x,y,z//
        rand_x = Random.Range(-1f, 1f);
        rand_y = Random.Range(-1f, 1f);
        rand_z = Random.Range(1f, 3f);
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�ŧi�C������player = ��MTag��Player������
        TargetTransform = player.transform; //�ؼЮy�� ���󪱮a�y��
        //����CD//
        mAttackCooldown -= Time.deltaTime;
        if (mAttackCooldown <= 0)
            mAttackCooldown = 0;
        
        //���A��//
        switch (State)
        {            
            case EnemyState.Idle:
                idle();
                break;
            case EnemyState.Patrol:
                patrol();
                break;
            case EnemyState.Attack:
                attack();
                break;
            case EnemyState.Dead:
                dead();
                break;
            case EnemyState.Homing:
                homing();
                break;
            default:
                break;
        }    
    }

    private void idle()
    {        
        //����M���a�������Z��
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);
        if (distance <= SightRange)
        {
            //�Y�bĵı�d�򤺴N�D�ʧ���
            State = EnemyState.Attack;
        }
        animation.CrossFade("Idle", 0.2f); //����"Idle"�ʧ@

        if (Attack_type != 3)//�����Ҧ�3�H�~ (����w�I�����ޥH�~)
        {
            MoveTime += Time.deltaTime;
            if (MoveTime > rand_z)
            {
                MoveTime = 0.0f;
                State = EnemyState.Patrol;
            }
        }
    }

    private void patrol() //����
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);

        PatrolTime += Time.deltaTime;
        if (PatrolTime >= 1)
        {
            PatrolTime = 0.0f;
            rand_x = Random.Range(-1f, 1f);
            rand_y = Random.Range(-1f, 1f);
            rand_z = Random.Range(2f, 4f);
            State = EnemyState.Idle;            
        }
        else
        {
            Vector3 walk = new Vector3(myTransform.position.x + rand_x, myTransform.position.y, myTransform.position.z + rand_y);//���ʤ�V            

            Quaternion lookRotation = Quaternion.LookRotation(walk - myTransform.position);//�`������    
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);//Slerp:�y������            
            
            animation.CrossFade("Run", 0.2f); //����Run�ʵe

            Vector3 moveDirection = new Vector3(0, 0, rand_z); //�V�e����
            moveDirection = myTransform.TransformDirection(moveDirection);//�N�ۨ������ܴ���@�ɧ��Ф�V
            mController.Move(moveDirection * Time.deltaTime); //�H���������             
                      

            if (distance <= SightRange)
            {
                //�Y�bĵı�d�򤺴N�D�ʧ���
                State = EnemyState.Attack;
            }
        }
    }

    private void attack()
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);//����M���a�������Z��   
        
        if (distance <= AttackRange)
        {            
            //�b�����d�򤺡G���ª��a            
            Vector3 watch = new Vector3(TargetTransform.position.x, myTransform.position.y, TargetTransform.position.z);
            Quaternion lookRotation = Quaternion.LookRotation(watch - myTransform.position);//�`������                
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);//Slerp:�y������

            if (mAttackCooldown == 0) //�����w�N�o�G����
            {
                audio.PlayOneShot(Audio_Attack, 1.0f); //�����������
                if (Attack_type == 1) //���i�ԧ���
                {
                    mCreature.Attack(TargetTransform.GetComponent<Creature>());//����(�ؼ�)
                    mAttackCooldown = AttackSpeed; //����CD
                    animation.CrossFade("Attack", 0.2f); //��������ʵe
                }
                else if (Attack_type == 2 || Attack_type == 3) //���{���� �����Ҧ�2��3
                {
                    AttackFix = FixObject.transform; //�ץ�rotation��                     
                    animation.CrossFade("Attack", 0.2f); //��������ʵe                    
                    Instantiate(Boom, FixObject.transform.position, AttackFix.rotation); //�ʺA�ͦ� Boom ��m�b���{���X�����m
                    mAttackCooldown = AttackSpeed; //����CD
                }
            }
        }                   

        else if (distance < SightRange && distance > AttackRange)
        {            
            runTo(TargetTransform.position);//�b�����d�򤺡G�ĦV���a
        }

        else if (distance > SightRange)
        {           
            State = EnemyState.Homing;//�W�X�Z���G�k��
        }
    }
  
    private void runTo(Vector3 iPosition) //�V�ؼЮy�в���
    {        
        animation.CrossFade("Run", 0.2f);
        Debug.DrawLine(myTransform.position, TargetTransform.position, Color.red);//�ۨ���m��ؼЦ�m�e�@�����u         
        iPosition.y = myTransform.position.y;//�קK�W�U�b����
        Quaternion lookRotation = Quaternion.LookRotation(iPosition - myTransform.position);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);        
        mController.SimpleMove(lookRotation * Vector3.forward * MoveSpeed);
    }
    
    private void dead()
    {
        State = EnemyState.Dead; //������Dead���A     
        //this.tag
        
        if (mCreature.HP <=0)
        {
            animation.CrossFade("Dead", 0.2f); //����"Dead"�ʧ@
            Destroy(this.gameObject, 2); //����R��������
        }             
    }

    private void homing()
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);//����M���a�������Z��  
        runTo(HomePosition);//�^��_�l�y��

        if (distance < SightRange && distance > AttackRange)//�b�����d��
        {
            State = EnemyState.Attack; //�N���A�������������A
        }
        if (Vector3.Distance(myTransform.position, HomePosition) <= 0.5) //�Y�^����I ���A���^�ݩR
        {            
            State = EnemyState.Idle;
        }
    }
}
