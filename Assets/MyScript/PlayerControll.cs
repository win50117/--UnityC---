using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControll : MonoBehaviour 
{
    public float speed = 6.0f; //移動速度
    public float jumpSpeed = 8.0f;//跳躍速度
    public float gravity = 20.0f; //重力值
    private Transform myTransform;//自身transform
    private Vector3 moveDirection = Vector3.zero;//三維向量 初始數值0
    public Transform target;//Transform(變形資訊)物件 代入準星 (攝影機的對面 主角在中間)
    public Vector3 sight; //準星位置的三維向量 (讓注視時不上下旋轉)   
    private CharacterController player;
    
    // Use this for initialization
	void Start () 
    {
        player = GetComponent<CharacterController>();
        player.slopeLimit = 60.0f; //可走斜坡幾度  
        myTransform = transform; //若經常調用transform 宣告變數(暫存)給他避免頻繁的找查	
	}   
	// Update is called once per frame
    void Update()
    {
        if (player.isGrounded) //在地板上
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal")*4/5, 0, Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") <= -0.2)
            {
                moveDirection.z = Input.GetAxis("Vertical") / 2;//後退移動速度減半
            }
            moveDirection = myTransform.TransformDirection(moveDirection); //將自身坐標變換到世界坐標方向
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                SendMessage("Anim_Jump"); //傳遞訊息 執行Anim_Jump()函式 動作
                moveDirection.y = jumpSpeed;
            }
        }
        sight.x = target.position.x;   //準星的x位置隨著攝影機改變
        sight.y = myTransform.position.y;//準星的y位置等於主角本身 (避免人物上下旋轉)
        sight.z = target.position.z;   //準星的z位置隨著攝影機改變
        myTransform.LookAt(sight); //持續注視著準星的位置

        moveDirection.y -= gravity * Time.deltaTime; //y值(上下垂直)持續減少重力值
        player.Move(moveDirection * Time.deltaTime); //人物控制器移動  
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
