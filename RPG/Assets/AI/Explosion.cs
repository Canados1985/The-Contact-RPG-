using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


    public GameObject go_explosion;
    public ParticleSystem explosionEffect;
    public float f_countLife = 0.8f;

    private Transform playerTransform;

    float f_distanceWithPlayer;

    bool b_hitsPlayer = false;

	void Start () {

        playerTransform = GameObject.Find("Player").transform;

        explosionEffect.Play(true);
        f_countLife = 0.8f;
    }
	
	void Update () {


        f_distanceWithPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (f_distanceWithPlayer < 30 && !b_hitsPlayer)
        {
            b_hitsPlayer = true;
            Player.cl_Player.f_playerHealth = Player.cl_Player.f_playerHealth - 25;

        }

        if (f_countLife > 0)
        {

            f_countLife -= Time.deltaTime;
        }
        if (f_countLife <= 0)
        {
            b_hitsPlayer = false;
            Destroy(this.gameObject);
            
        }

	}
}
