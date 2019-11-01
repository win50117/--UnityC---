using UnityEngine;
using System.Collections;

//把技能模組當成AI 利用creature的攻擊方法造成傷害
public class SwordSkill_Mod4 : MonoBehaviour 
{
    private Creature mCreature;      
    private Transform TargetTransform; //目標變型資訊 (位置)   
    private GameObject[] enemy;
    public float Range = 10.0f; //攻擊範圍

    void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy"); //宣告遊戲物件Enemy = 找尋Tag為Enemy的物件 test           
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
        Destroy(this.gameObject, 2); //2秒之後摧毀自身	
	}
}
