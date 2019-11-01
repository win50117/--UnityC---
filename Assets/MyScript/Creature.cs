using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour 
{
    private Creature mTarget;//目標資訊
    public int AttackPower; //攻擊力
    public int Armor; //防禦力
    public float Max_HP = 1000;
    public float HP = 1000;
    public GameObject GUIDamage; //傷害顯示物件

    public Creature Target
    {
        get
        {
            return mTarget;
        }
        set
        {
            mTarget = value;
        }
    }
    public void Attack(Creature iTarget) //攻擊目標 代入型態為Creature  iTarget為被攻擊者
    {              
        iTarget.UnderAttack(this); //被攻擊者執行"被攻擊程式碼"  this為攻擊者        
    }

    public void UnderAttack(Creature iSource) //被攻擊 傷害計算 iSource為攻擊者
    {        
        int damage = iSource.AttackPower - this.Armor; //目標攻擊力 - 自身防禦力
        if (damage <= 0)
            damage = 1;

        //取得GUIDamage下的DamageGUI程式碼 並執行CreateDamege(int Damage)修改傷害數
        //動態生成 GUIDamage 位置在此程式碼物件位置
        GUIDamage.GetComponent<DamageGUI>().CreateDamege(-damage, this.tag);
        Instantiate(GUIDamage, transform.position, transform.rotation);

        animation.CrossFade("OnHit", 0.2f); //執行被攻擊動作       
        HP -= damage;
        Debug.Log(string.Format("{0}->{1} damage={2}", iSource, this, damage));
        
        if (HP <= 0) 
        {
            HP = 0;
            SendMessage("dead"); //呼叫dead函式並執行
        }
    }

    public void Attack(Creature iTarget, int SkillDamage) //技能攻擊
    {
        iTarget.UnderAttack(this, SkillDamage); //this為攻擊者
    }

    public void UnderAttack(Creature iSource, int SkillDamage) //被技能攻擊
    {
        //取得GUIDamage下的DamageGUI程式碼 並執行CreateDamege(int Damage)修改傷害數
        //動態生成 GUIDamage 位置在此程式碼物件位置
        GUIDamage.GetComponent<DamageGUI>().CreateDamege(-SkillDamage, this.tag); 
        Instantiate(GUIDamage, transform.position, transform.rotation); 
        
        animation.CrossFade("OnHit", 0.2f); //執行被攻擊動作
        HP -= SkillDamage;
        Debug.Log(string.Format("{0}->{1} SkillDamage={2}", iSource, this, SkillDamage));       
        if (HP <= 0)
        {
            HP = 0;
            SendMessage("dead"); //呼叫dead函式並執行
        }
    }
}

