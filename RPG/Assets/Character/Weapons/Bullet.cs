using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject go_bullet;
    public Rigidbody rb_bullet;

    public Transform bulletTransform;
    public Transform playerTransform;
    public Transform bulletContainerTransform;

    public float f_distanceWithPlayer;

    private float f_moveSpeed = 5f;
    public float f_counterBulletLife = 3;


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        bulletContainerTransform = GameObject.Find("PlayerBulletContainer").GetComponent<Transform>();

    }

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            //other.gameObject.GetComponent<Mutant>().f_mutantHealth = other.gameObject.GetComponent<Mutant>().f_mutantHealth - 50;
            //other.gameObject.GetComponent<Mutant>().b_attacked_by_player = true;

            go_bullet.transform.parent = bulletContainerTransform;
            go_bullet.transform.position = bulletContainerTransform.position;
            go_bullet.transform.rotation = bulletContainerTransform.rotation;
            f_counterBulletLife = 3;
            rb_bullet.velocity = Vector3.zero;
            go_bullet.SetActive(false);
            
            

        }

    }


    private void FixedUpdate()
    {

        


        if (f_counterBulletLife > 0)
        {
            f_counterBulletLife -= Time.deltaTime;
        }
        if (f_counterBulletLife <= 0)
        {
            go_bullet.transform.parent = bulletContainerTransform;
            go_bullet.transform.position = bulletContainerTransform.position;
            go_bullet.transform.rotation = bulletContainerTransform.rotation;
            f_counterBulletLife = 3;
            rb_bullet.velocity = Vector3.zero;
            go_bullet.SetActive(false);

        }
    }

    void Update () {

        rb_bullet.AddForce(bulletTransform.forward * f_moveSpeed);

    }
}
