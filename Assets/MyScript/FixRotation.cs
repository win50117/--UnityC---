using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour 
{
    private Transform myTransform;
    private Transform TargetTransform; //目標變型資訊 (位置)
    private Vector3 FixTrans;
    private float size_y;
    

	// Use this for initialization
	void Start () 
    {
        myTransform = transform;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        TargetTransform = player.transform; //目標座標 等於玩家座標
        size_y = player.collider.bounds.size.y;            
	}
	
	// Update is called once per frame
	void Update () 
    {
        FixTrans = new Vector3(TargetTransform.position.x, TargetTransform.position.y+(size_y/2),TargetTransform.position.z);
        myTransform.LookAt(FixTrans);	
	}
}
