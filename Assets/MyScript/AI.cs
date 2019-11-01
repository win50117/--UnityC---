using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(AudioSource))]

public class AI : MonoBehaviour 
{
    public enum EnemyState
    {      
        Idle, //待命  
        Patrol, //巡邏
        Attack, //攻擊  
        Dead, //死亡        
        Homing, //歸位
    } 

    public float MoveSpeed = 4; //移動速度    
    public float RotateSpeed = 3; //旋轉速度
    public float AttackSpeed = 1.0f; //攻擊速度(冷卻)
    public float AttackRange = 3.0f; //攻擊範圍
    public float SightRange = 10.0f; //視野距離
    public int Attack_type = 1; //攻擊模式 1進戰  2遠攻  3遠攻定點不巡邏
    
    private float MoveTime; //隨機移動CD
    private float PatrolTime; //移動持續時間
    private float rand_x, rand_y, rand_z; //亂數值 (x,y:方向 z:距離) 
    private Transform myTransform;

    public EnemyState State = EnemyState.Idle; //狀態
    private float mAttackCooldown; //攻擊冷卻時間    
    private Vector3 HomePosition; //原點座標

    private CharacterController mController; //控制器
    private Creature mCreature; //生物資訊    
    private Transform TargetTransform; //目標變型資訊 (位置) 
    public AudioClip Audio_Attack;//攻擊音效
 
    //用於遠程攻擊//
    public GameObject Boom; //宣告遊戲物件Boom 用來生成子彈
    private Transform AttackFix; //修正遠程攻擊方向
    public GameObject FixObject; //攻擊方向修正物件 建立EmptyObject讓他注視目標物

    void Awake()
    {
      //  GameObject player = GameObject.FindGameObjectWithTag("Player"); //宣告遊戲物件player = 找尋Tag為Player的物件
      //  TargetTransform = player.transform; //目標座標 等於玩家座標

        mCreature = GetComponent<Creature>(); //生物資訊
        mController = GetComponent<CharacterController>(); //角色控制器
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
        mController.slopeLimit = 60.0f; //可走斜坡幾度  
        HomePosition = myTransform.position; //原點座標紀錄  
        //隨機數x,y,z//
        rand_x = Random.Range(-1f, 1f);
        rand_y = Random.Range(-1f, 1f);
        rand_z = Random.Range(1f, 3f);
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //宣告遊戲物件player = 找尋Tag為Player的物件
        TargetTransform = player.transform; //目標座標 等於玩家座標
        //攻擊CD//
        mAttackCooldown -= Time.deltaTime;
        if (mAttackCooldown <= 0)
            mAttackCooldown = 0;
        
        //狀態機//
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
        //獲取和玩家之間的距離
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);
        if (distance <= SightRange)
        {
            //若在警覺範圍內就主動攻擊
            State = EnemyState.Attack;
        }
        animation.CrossFade("Idle", 0.2f); //播放"Idle"動作

        if (Attack_type != 3)//攻擊模式3以外 (遠攻定點不巡邏以外)
        {
            MoveTime += Time.deltaTime;
            if (MoveTime > rand_z)
            {
                MoveTime = 0.0f;
                State = EnemyState.Patrol;
            }
        }
    }

    private void patrol() //巡邏
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
            Vector3 walk = new Vector3(myTransform.position.x + rand_x, myTransform.position.y, myTransform.position.z + rand_y);//移動方向            

            Quaternion lookRotation = Quaternion.LookRotation(walk - myTransform.position);//注視旋轉    
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);//Slerp:球型插值            
            
            animation.CrossFade("Run", 0.2f); //播放Run動畫

            Vector3 moveDirection = new Vector3(0, 0, rand_z); //向前移動
            moveDirection = myTransform.TransformDirection(moveDirection);//將自身坐標變換到世界坐標方向
            mController.Move(moveDirection * Time.deltaTime); //人物控制器移動             
                      

            if (distance <= SightRange)
            {
                //若在警覺範圍內就主動攻擊
                State = EnemyState.Attack;
            }
        }
    }

    private void attack()
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);//獲取和玩家之間的距離   
        
        if (distance <= AttackRange)
        {            
            //在攻擊範圍內：面朝玩家            
            Vector3 watch = new Vector3(TargetTransform.position.x, myTransform.position.y, TargetTransform.position.z);
            Quaternion lookRotation = Quaternion.LookRotation(watch - myTransform.position);//注視旋轉                
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);//Slerp:球型插值

            if (mAttackCooldown == 0) //攻擊已冷卻：攻擊
            {
                audio.PlayOneShot(Audio_Attack, 1.0f); //播放攻擊音效
                if (Attack_type == 1) //為進戰攻擊
                {
                    mCreature.Attack(TargetTransform.GetComponent<Creature>());//攻擊(目標)
                    mAttackCooldown = AttackSpeed; //攻擊CD
                    animation.CrossFade("Attack", 0.2f); //播放攻擊動畫
                }
                else if (Attack_type == 2 || Attack_type == 3) //遠程攻擊 攻擊模式2或3
                {
                    AttackFix = FixObject.transform; //修正rotation用                     
                    animation.CrossFade("Attack", 0.2f); //播放攻擊動畫                    
                    Instantiate(Boom, FixObject.transform.position, AttackFix.rotation); //動態生成 Boom 位置在此程式碼物件位置
                    mAttackCooldown = AttackSpeed; //攻擊CD
                }
            }
        }                   

        else if (distance < SightRange && distance > AttackRange)
        {            
            runTo(TargetTransform.position);//在視野範圍內：衝向玩家
        }

        else if (distance > SightRange)
        {           
            State = EnemyState.Homing;//超出距離：歸位
        }
    }
  
    private void runTo(Vector3 iPosition) //向目標座標移動
    {        
        animation.CrossFade("Run", 0.2f);
        Debug.DrawLine(myTransform.position, TargetTransform.position, Color.red);//自身位置到目標位置畫一條紅線         
        iPosition.y = myTransform.position.y;//避免上下軸旋轉
        Quaternion lookRotation = Quaternion.LookRotation(iPosition - myTransform.position);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);        
        mController.SimpleMove(lookRotation * Vector3.forward * MoveSpeed);
    }
    
    private void dead()
    {
        State = EnemyState.Dead; //切換為Dead狀態     
        //this.tag
        
        if (mCreature.HP <=0)
        {
            animation.CrossFade("Dead", 0.2f); //播放"Dead"動作
            Destroy(this.gameObject, 2); //兩秒後摧毀此物件
        }             
    }

    private void homing()
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);//獲取和玩家之間的距離  
        runTo(HomePosition);//回到起始座標

        if (distance < SightRange && distance > AttackRange)//在視野範圍內
        {
            State = EnemyState.Attack; //將狀態切換為攻擊狀態
        }
        if (Vector3.Distance(myTransform.position, HomePosition) <= 0.5) //若回到原點 狀態切回待命
        {            
            State = EnemyState.Idle;
        }
    }
}
