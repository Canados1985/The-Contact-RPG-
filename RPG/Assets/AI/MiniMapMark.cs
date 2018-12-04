using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMark : MonoBehaviour {


    public Transform miniMapTransform;
    public Transform mutantTransform;

    Quaternion tempQuaternion;

	void Start () {

        tempQuaternion.x = mutantTransform.rotation.x - mutantTransform.rotation.x;
        tempQuaternion.y = mutantTransform.rotation.y - mutantTransform.rotation.y;
        tempQuaternion.z = mutantTransform.rotation.z - mutantTransform.rotation.y;

    }
	

	void Update () {

        miniMapTransform.rotation = new Quaternion(0, 0, 0, 0);

    }
}
