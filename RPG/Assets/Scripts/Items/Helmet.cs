using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour {

    public GameObject go_helmet;
    public Transform helmetTransform;
    public Transform miniMapMarker;

    float f_random;

    void Start()
    {

        go_helmet.transform.parent = null;
        helmetTransform.localScale = new Vector3(1, 1, 1);
        f_random = Random.Range(0f, 10f);
        //this.gameObject.name = "Helmet";
    }


    private void FixedUpdate()
    {
        if (f_random >= 5) { helmetTransform.Rotate(0, -5f, 0, 0); miniMapMarker.Rotate(0, 5f, 0, 0); }
        if (f_random < 5) { helmetTransform.Rotate(0, 5f, 0, 0); miniMapMarker.Rotate(0, -5f, 0, 0); }

    }
}
