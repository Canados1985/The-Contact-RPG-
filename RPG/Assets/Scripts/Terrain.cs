using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {


    public GameObject go_flame;
    

    void Start () {

       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "rocket")
        {
            Instantiate(go_flame, collision.transform);
            
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
