using UnityEngine;
using System.Collections;

public class EndTheBoss : MonoBehaviour 
{
    private GameObject[] enemy; //宣告遊戲物件陣列Enemy[]    
    private GameObject[] Any;

    // Update is called once per frame
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");        
        Any = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    }
    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");        
        if (enemy.Length == 0) //敵人死光時
        {                        
            for (int i = 0; i < Any.Length; i++)//清除所有GameObject物件
            {
                Destroy(Any[i], 0);
            }
            Application.LoadLevel("6_End");     
        }
    }
}
