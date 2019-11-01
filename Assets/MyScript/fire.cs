using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour 
{
    public float firespeed = 0.1f; //攻擊間隔
    public float timer; //計時器
    public GameObject Boom; //宣告遊戲物件Boom 用來生成子彈
    

	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) //滑鼠左鍵按下時
        {
            if (timer > firespeed) //計時器大於攻擊間隔時
            {
                timer = 0; //計時器歸零
                Instantiate(Boom, transform.position, transform.rotation); //動態生成 Boom 位置在此程式碼物件位置
            }            
        }
        timer += Time.deltaTime; //計時器增加
	}
}
