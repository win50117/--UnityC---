using UnityEngine;
using System.Collections;

public class OpenPortal : MonoBehaviour 
{
    private GameObject[] enemy; //宣告遊戲物件陣列Enemy[]
    public GameObject Open_portal;

	// Update is called once per frame
	void Update () 
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy"); //enemy = 找尋Tag為Enemy的所有物件 

        if (enemy.Length == 0) //敵人死光時
        {
            Open_portal.SetActive(true); //開啟傳送門           
        }
	}
}
