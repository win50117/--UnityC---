using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
    private Creature mCreature; //生物資訊
    private GameObject Player;
    private Transform mTransform;
    public Transform target;

    private GameObject enemy;

    RaycastHit hit; //用於獲取 線性投射的回饋物體
	// Use this for initialization
	void Start () 
    {
        Player = GameObject.FindWithTag("Player");  
        mCreature = Player.GetComponent<Creature>();
        mTransform = transform;
	}	
	// Update is called once per frame
	void Update () 
    {
        //線性投射
        if (Physics.Linecast(mTransform.position, target.position, out hit)) //(起始位置 , 終點位置) 用兩個empty gameobject
        {
            enemy = hit.collider.gameObject;//enemy等於 線性投射所接觸的碰撞體
            if (Input.GetMouseButtonDown(0)) //按下左鍵
            {
                mCreature.Attack(enemy.GetComponent<Creature>()); //攻擊
                print("jasdlkfjasl;dfkjasl;df");
            }
        }
	}    
}
