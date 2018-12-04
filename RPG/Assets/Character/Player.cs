using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : MonoBehaviour {

    public static Player cl_Player;
    public GameObject go_Player;

    // Player's equipments meshes on skeleton
    public GameObject helmet_level3;
    public GameObject helmet_level2;


    public GameObject impulseRifle;
    public GameObject rifleCanvasInfo;
    

    public GameObject bulletInst; // Rifle bullet Inst
    public Transform bulletsPoolTransform; // Bullets SpawnPoint inst
    public Transform playerGunTransform; // Player Riffle transform
    private List<GameObject> bullets; //Bullets Array

    private GameObject bulletsPool; // Container for Bullets List
    private Vector3 v3_bulletsInst; //V3 bullets insts

    public Text ammoText;

    public float f_playerHealth = 100f;
    public float f_playerOxigen = 100f;
    public float f_ammo = 150;

    public bool b_IsDeath = false;

	void Start () {

        cl_Player = this;
        
        bullets = new List<GameObject>();
        bulletsPool = GameObject.Find("PlayerBulletContainer");
        for (int i = 0; i < 100; i++)
        {
            GameObject temp = Instantiate(bulletInst);
            temp.SetActive(false);
            bullets.Add(temp);
            bullets[i].transform.parent = bulletsPool.transform;
            bullets[i].transform.position = bulletsPool.transform.position;
            bullets[i].transform.rotation = bulletsPool.transform.rotation;
            bullets[i].name = "bullet";
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < 100; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.parent = null;
                bullets[i].transform.position = playerGunTransform.transform.position;
                bullets[i].transform.rotation = playerGunTransform.transform.rotation;
                return bullets[i];

            }
        }

        GameObject temp = Instantiate(bulletInst);
        bullets.Add(temp);
        temp.SetActive(true);

        return temp;
    }


    private void FixedUpdate()
    {
        f_playerOxigen = f_playerOxigen - 0.005f;
    }


    void Update () {



        ammoText.text = f_ammo.ToString();


        if (f_playerOxigen <= 0)
        {

            f_playerHealth = f_playerHealth - 0.005f;
        }
        if (f_playerHealth <=0)
        {
            b_IsDeath = true;
        }

        if (impulseRifle.activeSelf == true)
        {
            rifleCanvasInfo.SetActive(true);
        }
        else
        {
            rifleCanvasInfo.SetActive(false);

        }
    }
}
