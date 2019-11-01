using UnityEngine;
using System.Collections;

public class Player_Weapon_AI : MonoBehaviour
{
    private FSM_Machine<Player_Weapon_AI> FSM; //宣告FSM_Machine<Player_Weapon_AI>狀態機
    private Creature mCreature; //生物資訊
    private Transform myTransform;
    private Vector3 FixTran;

    //CD時間變數//
    private float change_weapon_cd = 5;
    private float SwordAttackCD = 1.5f;
    private float SwordCD_1 = 2;
    private float SwordCD_2 = 5;
    private float SwordCD_3 = 7;
    private float SwordCD_4 = 1;    

    //技能GUI物件//
    public GameObject GUI_Sword_skill; 
    public GameObject GUI_Claw_skill;
    public GameObject GUI_Hammer_skill;
    public GameObject GUI_Whip_skill;

    //技能GameObject Mod物件
    public GameObject SkillMod_1;
    public GameObject SkillMod_2;
    public GameObject SkillMod_4_Area;
    public GameObject GUIDamage; //傷害顯示物件(用於Skill_3回血)
    //public GameObject Sword_Skill_Mod4;

    //技能使用變數//
    public float Sword_Skill_Range_4 = 6.0f; //攻擊範圍
    RaycastHit hit; //Skill_3 用於獲取 線性投射的回饋物體
    public Transform Skill_3_sight; //Skill_3
    public Transform Attack_sight; //Attack
    private GameObject enemy_one;
    private GameObject[] enemy; //宣告遊戲物件陣列Enemy[]

    public void Start()
    {
        myTransform = transform; //若經常調用transform 宣告變數給他避免頻繁的找查
        
        mCreature = GetComponent<Creature>();
        FSM = new FSM_Machine<Player_Weapon_AI>();
        FSM.Configure(this, new Player_Sword()); //初始配置 狀態為Player_Sword
                
        //GUI切換//
        GUI_Claw_skill.SetActive(false);
        GUI_Hammer_skill.SetActive(false);
        GUI_Whip_skill.SetActive(false);        
    }

    public void ChangeState(FSM_state<Player_Weapon_AI> e) //狀態切換
    {
        FSM.ChangeState(e);
    }

    public void Update()
    {
        //技能使用//
        enemy = GameObject.FindGameObjectsWithTag("Enemy"); //enemy = 找尋Tag為Enemy的所有物件 
        FixTran = new Vector3(myTransform.position.x, myTransform.position.y + collider.bounds.size.y / 2, myTransform.position.z);
        Weapon_CD_Count();//全部CD計算
        FSM.Update(); //持續執行狀態機的Update() 有當前狀態和全局狀態       
    }

    public float Weapon_CD //切換武器CD get set
    {
        get
        {
            return change_weapon_cd;
        }
        set
        {
            change_weapon_cd = value;
        }  
    }
    public float Attack_CD //Sword技能1 CD get set
    {
        get
        {
            return SwordAttackCD;
        }
        set
        {
            SwordAttackCD = value;
        }
    }
    public float SwordSkillCD_1 //Sword技能1 CD get set
    {
        get
        {
            return SwordCD_1;
        }
        set
        {
            SwordCD_1 = value;
        }
    }
    public float SwordSkillCD_2 //Sword技能2 CD get set
    {
        get
        {
            return SwordCD_2;
        }
        set
        {
            SwordCD_2 = value;
        }
    }
    public float SwordSkillCD_3 //Sword技能2 CD get set
    {
        get
        {
            return SwordCD_3;
        }
        set
        {
            SwordCD_3 = value;
        }
    }
    public float SwordSkillCD_4 //Sword技能4 CD get set
    {
        get
        {
            return SwordCD_4;
        }
        set
        {
            SwordCD_4 = value;
        }
    }

    public void Weapon_CD_Count() //所有CD計算
    {
        SwordAttackCD -= Time.deltaTime;//劍普攻 CD
        SwordCD_1 -= Time.deltaTime; //劍技能1 CD
        SwordCD_2 -= Time.deltaTime; //劍技能2 CD
        SwordCD_3 -= Time.deltaTime; //劍技能3 CD
        SwordCD_4 -= Time.deltaTime; //劍技能4 CD
        change_weapon_cd -= Time.deltaTime; //切換武器 CD
        if (change_weapon_cd <= 0) //小於0時等於0
            change_weapon_cd = 0;
        if (SwordAttackCD <= 0)
            SwordAttackCD = 0;
        if (SwordCD_1 <= 0)
            SwordCD_1 = 0;
        if (SwordCD_2 <= 0)
            SwordCD_2 = 0;
        if (SwordCD_3 <= 0)
            SwordCD_3 = 0;
        if (SwordCD_4 <= 0)
            SwordCD_4 = 0 ;
    }
    public void SwordAttack() //普攻
    {
        Debug.DrawLine(FixTran, Attack_sight.position, Color.red);//自身位置到目標位置畫一條紅線      
        SendMessage("Anim_Attack"); //傳遞訊息 執行Anim_Attack()函式 動作   
        if (Physics.Linecast(FixTran, Attack_sight.position, out hit)) //(起始位置 , 終點位置) 用兩個empty gameobject
        {
            enemy_one = hit.collider.gameObject;//enemy_one為線性投射所接觸的碰撞體
            print(enemy_one.tag);
            if (enemy_one.tag == "Enemy")
            {
                mCreature.Attack(enemy_one.GetComponent<Creature>()); //攻擊  
            }
        }
    }

    public void SwordSkill1() //Sword技能1
    {
        SendMessage("Anim_Skill_1"); //傳遞訊息 執行Anim_Skill_1()函式 動作        
        Instantiate(SkillMod_1, Attack_sight.position, myTransform.rotation); //動態生成 SkillMod_1 位置在此程式碼物件位置   
    }

    public void SwordSkill2() //Sword技能2
    {
        SendMessage("Anim_Skill_2"); //傳遞訊息 執行Anim_Skill_2()函式 動作
        Instantiate(SkillMod_2, Attack_sight.position, myTransform.rotation); //動態生成 SkillMod_2 位置在此程式碼物件位置 
    }

    public void SwordSkill3() //Sword技能3
    {        
        SendMessage("Anim_Skill_3"); //傳遞訊息 執行Anim_Skill_3()函式 動作    
        if (Physics.Linecast(FixTran, Skill_3_sight.position, out hit)) //(起始位置 , 終點位置) 用兩個empty gameobject
        {
            enemy_one = hit.collider.gameObject;//enemy_one為線性投射所接觸的碰撞體
            print(enemy_one.tag);
            if (enemy_one.tag == "Enemy") 
            {
                mCreature.Attack(enemy_one.GetComponent<Creature>(), 70); //技能傷害70                
                mCreature.HP += 50; //回血50
                //取得GUIDamage下的DamageGUI程式碼 並執行CreateDamege(int Damage)修改傷害數
                //動態生成 GUIDamage 位置在此程式碼物件位置
                GUIDamage.GetComponent<DamageGUI>().CreateDamege(50, this.tag); //回血數字
                Instantiate(GUIDamage, myTransform.position, myTransform.rotation);
            }
        }
    }

    public void SwordSkill4() //Sword技能4
    {
        //SendMessage("StopMove");        
        SendMessage("Anim_Skill_4"); //傳遞訊息 執行Anim_Skill_4()函式 動作
        Instantiate(SkillMod_4_Area, myTransform.position, myTransform.rotation); //動態生成 SkillMod_1 位置在此程式碼物件位置  
        for (int i = 0; i < enemy.Length; i++) //enemy.Length陣列共有幾個元素
        {                       
            float distance = Vector3.Distance(myTransform.position, enemy[i].transform.position);
            if (distance <= Sword_Skill_Range_4)
                mCreature.Attack(enemy[i].GetComponent<Creature>(),500); //技能傷害500
        }       
    }

    public void dead()
    {
        SendMessage("Anim_Dead");        
        GetComponent<PlayerControll>().enabled = false;
        GetComponent<Player_Weapon_AI>().enabled = false;
        GetComponent<Player_Anim>().enabled = false;
        Screen.showCursor = true;
        Screen.lockCursor = false;
        animation["Dead"].layer = 2;
        this.tag = "Respawn";
    }
}
