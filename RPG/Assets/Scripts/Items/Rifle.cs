using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour {

    public GameObject go_rifle;
    public Transform rifleTransform;
    public Transform miniMapMarker;
    Transform playerTransform;

    float f_random;

    void Start()
    {

        go_rifle.transform.parent = null;
        rifleTransform.localScale = new Vector3(1, 1, 1);
        f_random = Random.Range(0f, 10f);
        this.gameObject.name = "Rifle";
        playerTransform = GameObject.Find("Player").transform;
    }


    private void FixedUpdate()
    {
        if (f_random >= 5) { rifleTransform.Rotate(0, -5f, 0, 0); miniMapMarker.Rotate(0, 5f, 0, 0); }
        if (f_random < 5) { rifleTransform.Rotate(0, 5f, 0, 0); miniMapMarker.Rotate(0, -5f, 0, 0); }
        miniMapMarker.rotation = playerTransform.rotation;
    }
}
