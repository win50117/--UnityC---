using UnityEngine;
using System.Collections;

public class Portal3to4 : MonoBehaviour 
{
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            Application.LoadLevel("4_map_canyon");
        }
    }
}
