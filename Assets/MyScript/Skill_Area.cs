using UnityEngine;
using System.Collections;

public class Skill_Area : MonoBehaviour
{
	// Use this for initialization
    private Transform myTransform;
    public float AreaRange = 1;
    public float DestroyTime = 3;
	void Start () 
    {
        myTransform = transform;	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (myTransform.localScale.x < AreaRange)
        {
            myTransform.localScale += new Vector3(0.8f * Time.deltaTime, 0, 0.8f * Time.deltaTime);
        }
        myTransform.Rotate(0, 180 * Time.deltaTime, 0);
        Destroy(this.gameObject, DestroyTime); //1秒之後摧毀自身	
	}
}
