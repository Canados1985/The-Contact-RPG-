using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour {


    public GameObject go_antenna;
    public Transform antennaTransform;

    float f_random;

    void Start () {
        f_random = Random.Range(0f, 10f);
    }


    private void FixedUpdate()
    {
        if (f_random >= 5) { antennaTransform.Rotate(0, -0.1f, 0, 0); }
        if (f_random < 5) { antennaTransform.Rotate(0, 0.1f, 0, 0); }

    }

    void Update () {


    }
}
