using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject go_generator;
    public Transform generatorTransform;
    public Transform miniMapMarker;

    float f_random;

    void Start()
    {

        go_generator.transform.parent = null;
        generatorTransform.localScale = new Vector3(1, 1, 1);
        f_random = Random.Range(0f, 10f);
        this.gameObject.name = "Generator";
    }


    private void FixedUpdate()
    {
        if (f_random >= 5) { generatorTransform.Rotate(0, -5f, 0, 0); miniMapMarker.Rotate(0, 5f, 0, 0); }
        if (f_random < 5) { generatorTransform.Rotate(0, 5f, 0, 0); miniMapMarker.Rotate(0, -5f, 0, 0); }

        
    }
}
