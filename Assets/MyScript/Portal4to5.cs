using UnityEngine;
using System.Collections;

public class Portal4to5 : MonoBehaviour 
{
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            Application.LoadLevel("5_map_volcano");
        }
    }
}
