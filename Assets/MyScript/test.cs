using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{
    public GameObject SkillMod_2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) //滑鼠左鍵按下時
        {
            SwordSkill1();
        }
	
	}
    public void SwordSkill1() //Sword技能1
    {
        //SendMessage("Anim_Skill_2"); //傳遞訊息 執行Anim_Skill_2()函式 動作

        Instantiate(SkillMod_2, transform.position, transform.rotation); //動態生成 Boom 位置在此程式碼物件位置         

        //transform.rotation = Quaternion.Euler(0, transform.rotation.y + 45, 0);
        Quaternion tests;
        tests = transform.rotation;
        tests.y += 0.25f;
        print(tests.y); 
        //tests = Quaternion.Euler(0, transform.rotation.z + 45, 0);
        Instantiate(SkillMod_2, transform.position, tests);
        //tests = Quaternion.Euler(0, transform.rotation.x - 45, 0);
        Instantiate(SkillMod_2, transform.position, tests);
        // Instantiate(SkillMod_2, transform.position, transform.rotation);
    }
}
