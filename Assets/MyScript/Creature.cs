using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour 
{
    private Creature mTarget;//�ؼи�T
    public int AttackPower; //�����O
    public int Armor; //���m�O
    public float Max_HP = 1000;
    public float HP = 1000;
    public GameObject GUIDamage; //�ˮ`��ܪ���

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
    public void Attack(Creature iTarget) //�����ؼ� �N�J���A��Creature  iTarget���Q������
    {              
        iTarget.UnderAttack(this); //�Q�����̰���"�Q�����{���X"  this��������        
    }

    public void UnderAttack(Creature iSource) //�Q���� �ˮ`�p�� iSource��������
    {        
        int damage = iSource.AttackPower - this.Armor; //�ؼЧ����O - �ۨ����m�O
        if (damage <= 0)
            damage = 1;

        //���oGUIDamage�U��DamageGUI�{���X �ð���CreateDamege(int Damage)�ק�ˮ`��
        //�ʺA�ͦ� GUIDamage ��m�b���{���X�����m
        GUIDamage.GetComponent<DamageGUI>().CreateDamege(-damage, this.tag);
        Instantiate(GUIDamage, transform.position, transform.rotation);

        animation.CrossFade("OnHit", 0.2f); //����Q�����ʧ@       
        HP -= damage;
        Debug.Log(string.Format("{0}->{1} damage={2}", iSource, this, damage));
        
        if (HP <= 0) 
        {
            HP = 0;
            SendMessage("dead"); //�I�sdead�禡�ð���
        }
    }

    public void Attack(Creature iTarget, int SkillDamage) //�ޯ����
    {
        iTarget.UnderAttack(this, SkillDamage); //this��������
    }

    public void UnderAttack(Creature iSource, int SkillDamage) //�Q�ޯ����
    {
        //���oGUIDamage�U��DamageGUI�{���X �ð���CreateDamege(int Damage)�ק�ˮ`��
        //�ʺA�ͦ� GUIDamage ��m�b���{���X�����m
        GUIDamage.GetComponent<DamageGUI>().CreateDamege(-SkillDamage, this.tag); 
        Instantiate(GUIDamage, transform.position, transform.rotation); 
        
        animation.CrossFade("OnHit", 0.2f); //����Q�����ʧ@
        HP -= SkillDamage;
        Debug.Log(string.Format("{0}->{1} SkillDamage={2}", iSource, this, SkillDamage));       
        if (HP <= 0)
        {
            HP = 0;
            SendMessage("dead"); //�I�sdead�禡�ð���
        }
    }
}

