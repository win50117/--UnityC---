using UnityEngine;
using System.Collections;

//��ޯ�Ҳշ�AI �Q��creature��������k�y���ˮ`
public class SwordSkill_Mod4 : MonoBehaviour 
{
    private Creature mCreature;      
    private Transform TargetTransform; //�ؼ��ܫ���T (��m)   
    private GameObject[] enemy;
    public float Range = 10.0f; //�����d��

    void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy"); //�ŧi�C������Enemy = ��MTag��Enemy������ test           
        mCreature = GetComponent<Creature>();
    } 
	void Start () 
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemy[i].transform.position);
            if (distance <= Range)
                mCreature.Attack(enemy[i].GetComponent<Creature>());        
        }        
	}   
	
	void Update () 
    {        
        Destroy(this.gameObject, 2); //2����R���ۨ�	
	}
}
