using UnityEngine;
using System.Collections;

public class Player_Weapon_AI : MonoBehaviour
{
    private FSM_Machine<Player_Weapon_AI> FSM; //�ŧiFSM_Machine<Player_Weapon_AI>���A��
    private Creature mCreature; //�ͪ���T
    private Transform myTransform;
    private Vector3 FixTran;

    //CD�ɶ��ܼ�//
    private float change_weapon_cd = 5;
    private float SwordAttackCD = 1.5f;
    private float SwordCD_1 = 2;
    private float SwordCD_2 = 5;
    private float SwordCD_3 = 7;
    private float SwordCD_4 = 1;    

    //�ޯ�GUI����//
    public GameObject GUI_Sword_skill; 
    public GameObject GUI_Claw_skill;
    public GameObject GUI_Hammer_skill;
    public GameObject GUI_Whip_skill;

    //�ޯ�GameObject Mod����
    public GameObject SkillMod_1;
    public GameObject SkillMod_2;
    public GameObject SkillMod_4_Area;
    public GameObject GUIDamage; //�ˮ`��ܪ���(�Ω�Skill_3�^��)
    //public GameObject Sword_Skill_Mod4;

    //�ޯ�ϥ��ܼ�//
    public float Sword_Skill_Range_4 = 6.0f; //�����d��
    RaycastHit hit; //Skill_3 �Ω���� �u�ʧ�g���^�X����
    public Transform Skill_3_sight; //Skill_3
    public Transform Attack_sight; //Attack
    private GameObject enemy_one;
    private GameObject[] enemy; //�ŧi�C������}�CEnemy[]

    public void Start()
    {
        myTransform = transform; //�Y�g�`�ե�transform �ŧi�ܼƵ��L�קK�W�c����d
        
        mCreature = GetComponent<Creature>();
        FSM = new FSM_Machine<Player_Weapon_AI>();
        FSM.Configure(this, new Player_Sword()); //��l�t�m ���A��Player_Sword
                
        //GUI����//
        GUI_Claw_skill.SetActive(false);
        GUI_Hammer_skill.SetActive(false);
        GUI_Whip_skill.SetActive(false);        
    }

    public void ChangeState(FSM_state<Player_Weapon_AI> e) //���A����
    {
        FSM.ChangeState(e);
    }

    public void Update()
    {
        //�ޯ�ϥ�//
        enemy = GameObject.FindGameObjectsWithTag("Enemy"); //enemy = ��MTag��Enemy���Ҧ����� 
        FixTran = new Vector3(myTransform.position.x, myTransform.position.y + collider.bounds.size.y / 2, myTransform.position.z);
        Weapon_CD_Count();//����CD�p��
        FSM.Update(); //������檬�A����Update() ����e���A�M�������A       
    }

    public float Weapon_CD //�����Z��CD get set
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
    public float Attack_CD //Sword�ޯ�1 CD get set
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
    public float SwordSkillCD_1 //Sword�ޯ�1 CD get set
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
    public float SwordSkillCD_2 //Sword�ޯ�2 CD get set
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
    public float SwordSkillCD_3 //Sword�ޯ�2 CD get set
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
    public float SwordSkillCD_4 //Sword�ޯ�4 CD get set
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

    public void Weapon_CD_Count() //�Ҧ�CD�p��
    {
        SwordAttackCD -= Time.deltaTime;//�C���� CD
        SwordCD_1 -= Time.deltaTime; //�C�ޯ�1 CD
        SwordCD_2 -= Time.deltaTime; //�C�ޯ�2 CD
        SwordCD_3 -= Time.deltaTime; //�C�ޯ�3 CD
        SwordCD_4 -= Time.deltaTime; //�C�ޯ�4 CD
        change_weapon_cd -= Time.deltaTime; //�����Z�� CD
        if (change_weapon_cd <= 0) //�p��0�ɵ���0
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
    public void SwordAttack() //����
    {
        Debug.DrawLine(FixTran, Attack_sight.position, Color.red);//�ۨ���m��ؼЦ�m�e�@�����u      
        SendMessage("Anim_Attack"); //�ǻ��T�� ����Anim_Attack()�禡 �ʧ@   
        if (Physics.Linecast(FixTran, Attack_sight.position, out hit)) //(�_�l��m , ���I��m) �Ψ��empty gameobject
        {
            enemy_one = hit.collider.gameObject;//enemy_one���u�ʧ�g�ұ�Ĳ���I����
            print(enemy_one.tag);
            if (enemy_one.tag == "Enemy")
            {
                mCreature.Attack(enemy_one.GetComponent<Creature>()); //����  
            }
        }
    }

    public void SwordSkill1() //Sword�ޯ�1
    {
        SendMessage("Anim_Skill_1"); //�ǻ��T�� ����Anim_Skill_1()�禡 �ʧ@        
        Instantiate(SkillMod_1, Attack_sight.position, myTransform.rotation); //�ʺA�ͦ� SkillMod_1 ��m�b���{���X�����m   
    }

    public void SwordSkill2() //Sword�ޯ�2
    {
        SendMessage("Anim_Skill_2"); //�ǻ��T�� ����Anim_Skill_2()�禡 �ʧ@
        Instantiate(SkillMod_2, Attack_sight.position, myTransform.rotation); //�ʺA�ͦ� SkillMod_2 ��m�b���{���X�����m 
    }

    public void SwordSkill3() //Sword�ޯ�3
    {        
        SendMessage("Anim_Skill_3"); //�ǻ��T�� ����Anim_Skill_3()�禡 �ʧ@    
        if (Physics.Linecast(FixTran, Skill_3_sight.position, out hit)) //(�_�l��m , ���I��m) �Ψ��empty gameobject
        {
            enemy_one = hit.collider.gameObject;//enemy_one���u�ʧ�g�ұ�Ĳ���I����
            print(enemy_one.tag);
            if (enemy_one.tag == "Enemy") 
            {
                mCreature.Attack(enemy_one.GetComponent<Creature>(), 70); //�ޯ�ˮ`70                
                mCreature.HP += 50; //�^��50
                //���oGUIDamage�U��DamageGUI�{���X �ð���CreateDamege(int Damage)�ק�ˮ`��
                //�ʺA�ͦ� GUIDamage ��m�b���{���X�����m
                GUIDamage.GetComponent<DamageGUI>().CreateDamege(50, this.tag); //�^��Ʀr
                Instantiate(GUIDamage, myTransform.position, myTransform.rotation);
            }
        }
    }

    public void SwordSkill4() //Sword�ޯ�4
    {
        //SendMessage("StopMove");        
        SendMessage("Anim_Skill_4"); //�ǻ��T�� ����Anim_Skill_4()�禡 �ʧ@
        Instantiate(SkillMod_4_Area, myTransform.position, myTransform.rotation); //�ʺA�ͦ� SkillMod_1 ��m�b���{���X�����m  
        for (int i = 0; i < enemy.Length; i++) //enemy.Length�}�C�@���X�Ӥ���
        {                       
            float distance = Vector3.Distance(myTransform.position, enemy[i].transform.position);
            if (distance <= Sword_Skill_Range_4)
                mCreature.Attack(enemy[i].GetComponent<Creature>(),500); //�ޯ�ˮ`500
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
