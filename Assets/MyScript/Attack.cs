using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
    private Creature mCreature; //�ͪ���T
    private GameObject Player;
    private Transform mTransform;
    public Transform target;

    private GameObject enemy;

    RaycastHit hit; //�Ω���� �u�ʧ�g���^�X����
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
        //�u�ʧ�g
        if (Physics.Linecast(mTransform.position, target.position, out hit)) //(�_�l��m , ���I��m) �Ψ��empty gameobject
        {
            enemy = hit.collider.gameObject;//enemy���� �u�ʧ�g�ұ�Ĳ���I����
            if (Input.GetMouseButtonDown(0)) //���U����
            {
                mCreature.Attack(enemy.GetComponent<Creature>()); //����
                print("jasdlkfjasl;dfkjasl;df");
            }
        }
	}    
}
