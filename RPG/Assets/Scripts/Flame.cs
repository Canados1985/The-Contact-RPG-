using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {

    public GameObject go_flame;

	void Start () {
        go_flame.transform.parent = null;
        go_flame.transform.rotation = new Quaternion(0,0,0,0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
