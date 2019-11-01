using UnityEngine;
using System.Collections;

public class Portal2to3 : MonoBehaviour 
{
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            Application.LoadLevel("3_map_altar");
        }
    }
}
