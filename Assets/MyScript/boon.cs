using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class boon : MonoBehaviour 
{
    public float speed = 40; //子彈射速   
    public int Damage = 10;
    public int BulletStyle = 1; //(1:不貫穿 2:貫穿)
    public int DestoryTime = 1; //自動毀滅時間
    public string UnAtkTag = "Player"; //被攻擊者Tag
    private Creature mCreature; //生物資訊

	// Use this for initialization
	void Start () 
    {
        mCreature = GetComponent<Creature>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime, Space.Self); //移動方向
        Destroy(this.gameObject, DestoryTime); //DestoryTime秒之後摧毀自身        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrian") //子彈碰到Tag為地形時
        {
            if (BulletStyle == 1)//子彈不貫穿
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == UnAtkTag) //碰到被攻擊者時
        {
            mCreature.Attack(other.GetComponent<Creature>(), Damage); //傷害10
            if (BulletStyle == 1)//子彈不貫穿
            {
                Destroy(this.gameObject);
            }
        }        
    }
}
