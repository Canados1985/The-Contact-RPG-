using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePropeller : MonoBehaviour {


    public GameObject go_propeller;
    public Transform propellerTransform;


    float f_random;

    void Start()
    {
        f_random = Random.Range(0f, 10f);
    }

    private void FixedUpdate()
    {
        if (f_random >= 5) { propellerTransform.Rotate(0, -1000 * 2, 0, 0); }
        if (f_random < 5) { propellerTransform.Rotate(0, 1000 * 2, 0, 0); }
    }
}
