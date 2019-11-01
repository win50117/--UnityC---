using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class boon : MonoBehaviour 
{
    public float speed = 40; //�l�u�g�t   
    public int Damage = 10;
    public int BulletStyle = 1; //(1:���e�� 2:�e��)
    public int DestoryTime = 1; //�۰ʷ����ɶ�
    public string UnAtkTag = "Player"; //�Q������Tag
    private Creature mCreature; //�ͪ���T

	// Use this for initialization
	void Start () 
    {
        mCreature = GetComponent<Creature>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime, Space.Self); //���ʤ�V
        Destroy(this.gameObject, DestoryTime); //DestoryTime����R���ۨ�        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrian") //�l�u�I��Tag���a�ή�
        {
            if (BulletStyle == 1)//�l�u���e��
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == UnAtkTag) //�I��Q�����̮�
        {
            mCreature.Attack(other.GetComponent<Creature>(), Damage); //�ˮ`10
            if (BulletStyle == 1)//�l�u���e��
            {
                Destroy(this.gameObject);
            }
        }        
    }
}
