using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(Dragon_Audio))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Dragon_Anim))]


public class Dragon_AI : MonoBehaviour 
{
    //一般宣告變數//
    public float RotateSpeed = 3;
    private FSM_Machine<Dragon_AI> FSM; //宣告FSM_Machine<Dragon_AI>狀態機
    public Creature mCreature; //生物資訊    
    private Transform myTransform; 
    private Transform TargetTransform; //玩家Transform
    private Vector3 HomePosition; //原點座標
    private float rand_x, rand_y; //亂數值 
    private int Dragon_Mode;

    //CD時間變數//
    private float NearAttack_CD = 2;
    private float FireAttack_CD = 4;
    private float Area_Fire_CD = 10;
    private float Ray_Shoot_CD = 5;

    //技能GameObject Mod物件//
    private Transform AttackFix; //修正遠程攻擊方向
    public GameObject FireBall; //火球    
    public GameObject FixObject; //攻擊方向修正物件 建立EmptyObject讓他注視目標物
    public GameObject Magic_Area_0;//魔法陣0
    public GameObject Magic_Area_1;//魔法陣1
    public GameObject Magic_Area_4; //魔法陣4
    public GameObject Area_FireBall; //範圍火雨
    public GameObject Ray_Ball;//光球
    public float Area_FireBall_Count; //火雨顆數計數
    public GameObject LocatePoint_1, LocatePoint_2, LocatePoint_3, LocatePoint_4;//定位點
    public GameObject FixPoint_1, FixPoint_2, FixPoint_3, FixPoint_4;//Rotation修正
    private Transform FixRay_1, FixRay_2, FixRay_3, FixRay_4;
    

	// Use this for initialization
	void Start () 
    {
        myTransform = transform; //若經常調用transform 宣告變數給他避免頻繁的找查
        mCreature = GetComponent<Creature>();
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //宣告遊戲物件player = 找尋Tag為Player的物件
        TargetTransform = player.transform; //目標座標 等於玩家座標
        HomePosition = myTransform.position;//記錄原點座標

        FSM = new FSM_Machine<Dragon_AI>();
        FSM.Configure(this, new Dragon_State_NormalMode()); //初始配置 狀態為Player_Sword	
	}

    public void ChangeState(FSM_state<Dragon_AI> e) //狀態切換
    {
        FSM.ChangeState(e);
    }

    public void RevertToPreviousState()
    {
        FSM.RevertToPreviousState();
    }
	
	// Update is called once per frame
	void Update () 
    {
        CD_Count(); //所有CD計算
        FSM.Update(); //持續執行狀態機的Update() 有當前狀態和全局狀態  	
	}

    public float NearAttackCD //進戰CD get/set
    {
        get
        { 
            return NearAttack_CD; 
        }
        set
        {
            NearAttack_CD = value;
        }
    }
    public float FireAttackCD //一般火球CD get/set
    {
        get
        {
            return FireAttack_CD;
        }
        set
        {
            FireAttack_CD = value;
        }
    }
    public float AreaFireCD
    {
        get
        {
            return Area_Fire_CD;
        }
        set
        {
            Area_Fire_CD = value;
        }
    }
    public float AreaFireCount
    {
        get
        {
            return Area_FireBall_Count;
        }
        set
        {
            Area_FireBall_Count = value;
        }
    }
    public float RayShootCD
    {
        get
        {
            return Ray_Shoot_CD;
        }
        set
        {
            Ray_Shoot_CD = value;
        }
    }
    public int DragonMode
    {
        get
        {
            return Dragon_Mode;
        }
        set
        {
            Dragon_Mode = value;
        }
    }

    public void CD_Count() //所有CD計算
    {
        NearAttack_CD -= Time.deltaTime;//進戰CD
        FireAttack_CD -= Time.deltaTime;//火球CD      
        Area_Fire_CD -= Time.deltaTime;//範圍火雨CD
        Ray_Shoot_CD -= Time.deltaTime;//
        if (NearAttack_CD <= 0)
            NearAttack_CD = 0;
        if (FireAttack_CD <= 0)
            FireAttack_CD = 0;
        if (Area_Fire_CD <= 0)
            Area_Fire_CD = 0;
        if (Ray_Shoot_CD <= 0)
            Ray_Shoot_CD = 0;  
    }

    public void FaceToPlayer() //面向玩家
    {
        Vector3 Tposition;
        Tposition = TargetTransform.transform.position;
        Tposition.y = myTransform.position.y;//避免上下軸旋轉
        Quaternion lookRotation = Quaternion.LookRotation(Tposition - myTransform.position);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, RotateSpeed * Time.deltaTime);    
    }

    public float Distance()//計算距離
    {
        float distance = Vector3.Distance(myTransform.position, TargetTransform.position);//雙方距離
        return distance;//回傳值
    }

    public void NearAttack() //進戰攻擊
    {
        SendMessage("Audio_NearAttack"); //傳遞訊息 執行普攻音效
        mCreature.Attack(TargetTransform.GetComponent<Creature>());//攻擊(目標)
    }

    public void FireAttack() //一般火球
    {                 
        AttackFix = FixObject.transform; //修正rotation用         
        Instantiate(FireBall, FixObject.transform.position, AttackFix.rotation); //動態生成 Boom 位置在此程式碼物件位置        
    }

    public void FlyUP()
    {
        myTransform.Translate(0, 5.5f * Time.deltaTime, 0);
    }

    public void FlyDown()
    {
        myTransform.Translate(0,-5.5f * Time.deltaTime, 0);        
    }

    public void FixPosition()//下飛回原點 座標誤差修正
    {
        myTransform.position = HomePosition;
    }

    public void Area_Fire()
    {
        Area_FireBall_Count++;//火球顆數+1
        rand_x = Random.Range(TargetTransform.position.x - 10, TargetTransform.position.x + 10);
        rand_y = Random.Range(TargetTransform.position.z - 10, TargetTransform.position.z + 10);
        
        Instantiate(Magic_Area_4, new Vector3(rand_x, 0.1f, rand_y), TargetTransform.rotation);
        Instantiate(Area_FireBall, new Vector3(rand_x, 30f, rand_y), Quaternion.Euler(90, 0, 0));        
    }

    public void Ray_Area()
    {     
        Instantiate(Magic_Area_0, LocatePoint_1.transform.position, LocatePoint_1.transform.rotation);
        Instantiate(Magic_Area_0, LocatePoint_2.transform.position, LocatePoint_2.transform.rotation);
        Instantiate(Magic_Area_1, LocatePoint_3.transform.position, LocatePoint_3.transform.rotation);
        Instantiate(Magic_Area_1, LocatePoint_4.transform.position, LocatePoint_4.transform.rotation);
    }

    public void Ray_Shoot()
    {
        FixRay_1 = FixPoint_1.transform; //修正rotation用   
        FixRay_2 = FixPoint_2.transform; 
        FixRay_3 = FixPoint_3.transform; 
        FixRay_4 = FixPoint_4.transform;

        Instantiate(Ray_Ball, LocatePoint_1.transform.position, FixRay_1.rotation);
        Instantiate(Ray_Ball, LocatePoint_2.transform.position, FixRay_2.rotation);
        Instantiate(Ray_Ball, LocatePoint_3.transform.position, FixRay_3.rotation);
        Instantiate(Ray_Ball, LocatePoint_4.transform.position, FixRay_4.rotation); 
    }

    public void dead()
    {
        print("我要逃跑啦!");
    }

    public void Escape()
    {
        Destroy(this.gameObject, 0);
    }
}
