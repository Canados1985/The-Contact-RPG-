using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {


    public GameObject go_drone;
    public Transform droneTransform;
    public Rigidbody rb_drone;
    public Light droneLight;
    private AudioSource droneSound;

    public bool b_up = true;
    public bool b_down = false;

    float f_random;

    public float f_countMoving = 0.5f;

    void Start()
    {
        f_random = Random.Range(0f, 10f);
        droneSound = GetComponent<AudioSource>();
        droneSound.Play();
    }


    private void FixedUpdate()
    {
        if (f_countMoving > 0)
        {
            f_countMoving -= Time.deltaTime;
        }
        if (f_countMoving <=0 && b_up && !b_down)
        {
            b_up = false;
            b_down = true;
            f_countMoving = 0.5f;
            //droneTransform.Translate(0, -0.5f, 0);
            rb_drone.AddForce(droneTransform.up * -1.1f * 10);
            droneLight.enabled = true;

        }
        if (f_countMoving <= 0 && b_down && !b_up)
        {
            b_down = false;
            b_up = true;
            f_countMoving = 0.5f;
            //droneTransform.Translate(0, 0.5f, 0);
            rb_drone.AddForce(droneTransform.up * 1 * 10);
            droneLight.enabled = false;
        }


        if (f_random >= 5) { droneTransform.Rotate(0, -0.05f, 0, 0); }
        if (f_random < 5) { droneTransform.Rotate(0, 0.05f, 0, 0); }


    }
}
