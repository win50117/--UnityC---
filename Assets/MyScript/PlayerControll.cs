using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControll : MonoBehaviour 
{
    public float speed = 6.0f; //���ʳt��
    public float jumpSpeed = 8.0f;//���D�t��
    public float gravity = 20.0f; //���O��
    private Transform myTransform;//�ۨ�transform
    private Vector3 moveDirection = Vector3.zero;//�T���V�q ��l�ƭ�0
    public Transform target;//Transform(�ܧθ�T)���� �N�J�ǬP (��v�����ﭱ �D���b����)
    public Vector3 sight; //�ǬP��m���T���V�q (���`���ɤ��W�U����)   
    private CharacterController player;
    
    // Use this for initialization
	void Start () 
    {
        player = GetComponent<CharacterController>();
        player.slopeLimit = 60.0f; //�i���שY�X��  
        myTransform = transform; //�Y�g�`�ե�transform �ŧi�ܼ�(�Ȧs)���L�קK�W�c����d	
	}   
	// Update is called once per frame
    void Update()
    {
        if (player.isGrounded) //�b�a�O�W
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal")*4/5, 0, Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") <= -0.2)
            {
                moveDirection.z = Input.GetAxis("Vertical") / 2;//��h���ʳt�״�b
            }
            moveDirection = myTransform.TransformDirection(moveDirection); //�N�ۨ������ܴ���@�ɧ��Ф�V
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                SendMessage("Anim_Jump"); //�ǻ��T�� ����Anim_Jump()�禡 �ʧ@
                moveDirection.y = jumpSpeed;
            }
        }
        sight.x = target.position.x;   //�ǬP��x��m�H����v������
        sight.y = myTransform.position.y;//�ǬP��y��m����D������ (�קK�H���W�U����)
        sight.z = target.position.z;   //�ǬP��z��m�H����v������
        myTransform.LookAt(sight); //����`���۷ǬP����m

        moveDirection.y -= gravity * Time.deltaTime; //y��(�W�U����)�����֭��O��
        player.Move(moveDirection * Time.deltaTime); //�H���������  
    }

    public void StopMove() //RunSpeed
    {
        GetComponent<PlayerControll>().enabled = false;
        GetComponent<Player_Weapon_AI>().enabled = false;
        GetComponent<Player_Anim>().enabled = false;
        Invoke("StartMove", 2);
    }
    public void StartMove() //RunSpeed
    {
        GetComponent<PlayerControll>().enabled = true;
        GetComponent<Player_Weapon_AI>().enabled = true;
        GetComponent<Player_Anim>().enabled = true;
    }
}
