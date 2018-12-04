using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {


    public GameObject go_UFO;
    public Transform ufoTransform;

    float f_random;

    void Start()
    {
        f_random = Random.Range(0f, 10f);
    }

    private void FixedUpdate()
    {
        if (f_random >= 5) { ufoTransform.Rotate(0, -0.55f, 0, 0); }
        if (f_random < 5) { ufoTransform.Rotate(0, 0.75f, 0, 0); }
    }
}
