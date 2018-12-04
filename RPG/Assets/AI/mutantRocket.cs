using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutantRocket : MonoBehaviour {

    public GameObject go_mutantRocket;
    public Rigidbody rb_mutantRocket;


    public Transform mutantRocketTransform;
    public Transform rocketsContainerTransform; // Rockets SpawnPoint inst
    public Transform mutantLauncherTransform;

    public GameObject go_rocketModel;
    public GameObject go_Explosion;
    public Transform exlosionTransform;
    public Transform mutantTransform;

    public ParticleSystem explosionEffect;
    public ParticleSystem rocketTrail;

    public float f_distanceWithMutant;
    public float f_counterRocketLife = 3;
    private float f_moveSpeed = 1f;

    public bool b_hitGround = false;

    float f_random;

    void Start () {

        f_random = Random.Range(0f, 10f);
        explosionEffect.Stop(true);
        rocketsContainerTransform = GameObject.Find("MutantRocketsContainer").transform;
        rocketTrail.Play(true);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" && !b_hitGround)
        {
            b_hitGround = true;
            FindObjectOfType<AudioManager>().Play("explosion");
            Instantiate(go_Explosion, mutantRocketTransform.transform);
            //exlosionTransform.position = other.transform.position;
            //go_mutantRocket.transform.position = rocketsContainerTransform.position;
            //go_mutantRocket.transform.rotation = rocketsContainerTransform.rotation;
            rb_mutantRocket.angularVelocity = Vector3.zero;
            rb_mutantRocket.velocity = Vector3.zero;
            rocketTrail.Play(false);
            rocketTrail.Stop(true);
            f_counterRocketLife = 3;
            go_rocketModel.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        if (!b_hitGround)
        {
            rb_mutantRocket.AddForce(mutantRocketTransform.forward * f_moveSpeed);
        }
        

        f_distanceWithMutant = Vector3.Distance(mutantTransform.position, mutantRocketTransform.position);

        if (f_counterRocketLife > 0)
        {
            f_counterRocketLife -= Time.deltaTime;
        }
        if (f_counterRocketLife <= 0)
        {
            go_mutantRocket.transform.position = rocketsContainerTransform.position;
            go_mutantRocket.transform.rotation = rocketsContainerTransform.rotation;
            rb_mutantRocket.angularVelocity = Vector3.zero;
            rb_mutantRocket.velocity = Vector3.zero;
            b_hitGround = false;
            go_rocketModel.SetActive(true);
            f_counterRocketLife = 3;
        }


}



    void Update () {



    }
}
