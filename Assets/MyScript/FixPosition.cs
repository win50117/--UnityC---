using UnityEngine;
using System.Collections;

public class FixPosition : MonoBehaviour 
{
    private GameObject player;
    public int x,y,z;

	// Use this for initialization
    void Awake ()
    {        
        player = GameObject.FindGameObjectWithTag("Player");
    }
	void Start () 
    {
        player.transform.position = new Vector3(x,y,z);
	}
}
