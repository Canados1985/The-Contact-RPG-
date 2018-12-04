using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour {

    public GameObject go_Beacon;
    public Transform beaconTransform;

    float f_random;

    private void Start()
    {
        f_random = Random.Range(0f,10f);
    }

    private void FixedUpdate()
    {

        if (f_random >= 5) { beaconTransform.Rotate(0, -0.1f, 0, 0); }
        if (f_random < 5) { beaconTransform.Rotate(0, 0.1f, 0, 0); }
    }
}
