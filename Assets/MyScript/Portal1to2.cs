using UnityEngine;
using System.Collections;

public class Portal1to2 : MonoBehaviour 
{
    void OnTriggerEnter(Collider player)
    {        
        if (player.tag == "Player")
        {
            Application.LoadLevel("2_map_forest");            
        }
    }
}
