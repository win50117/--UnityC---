using UnityEngine;
using System.Collections;

public class DamageGUI : MonoBehaviour 
{
    private Transform myTransform;
    private Camera camera;
	void Start () 
    {
        camera = Camera.main;        
        myTransform = transform;
        myTransform.LookAt(myTransform.position + camera.transform.forward);
	}	
	// Update is called once per frame
	void Update () 
    {
        myTransform.LookAt(myTransform.position + camera.transform.forward);
        Destroy(this.gameObject, 2);
        myTransform.Translate(0, 0.08f, 0);
	}
    public void CreateDamege(int Damage,string tag)
    {
        if (Damage > 0)
        {
            GetComponent<TextMesh>().color = Color.green;
        }
        else if (tag == "Enemy")
        {
            GetComponent<TextMesh>().color = Color.white;
        }
        else if (tag == "Player")
        {
            GetComponent<TextMesh>().color = Color.red;
        }
        GetComponent<TextMesh>().text = Damage.ToString();
    }
}
