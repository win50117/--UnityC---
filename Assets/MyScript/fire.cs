using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour 
{
    public float firespeed = 0.1f; //�������j
    public float timer; //�p�ɾ�
    public GameObject Boom; //�ŧi�C������Boom �Ψӥͦ��l�u
    

	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) //�ƹ�������U��
        {
            if (timer > firespeed) //�p�ɾ��j��������j��
            {
                timer = 0; //�p�ɾ��k�s
                Instantiate(Boom, transform.position, transform.rotation); //�ʺA�ͦ� Boom ��m�b���{���X�����m
            }            
        }
        timer += Time.deltaTime; //�p�ɾ��W�[
	}
}
