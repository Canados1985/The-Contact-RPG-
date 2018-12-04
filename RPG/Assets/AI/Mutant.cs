using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mutant : MonoBehaviour
{

    public GameObject go_Mutant;
    public GameObject miniMapMarker;
    private CapsuleCollider mutantCollider;
    public Transform mutantTransform;
    public Transform playerTransform;
    public Animator animatorMutant;
    public NavMeshAgent agent;

    public ParticleSystem shootEffectMutantLauncher;
    public ParticleSystem bloodEffectMutant;
    public ParticleSystem bloodEffectMutantAfterDeath;

    public GameObject rocketInst; // Rocket Inst
    public Transform rocketsContainerTransform; // Rockets SpawnPoint inst
    public Transform mutantGunTransform; // Mutant Gun transform
    private List<GameObject> rockets; //Rockets Array

    public GameObject go_generator; 

    private GameObject rocketsPool; // Container for Rockets List
    private Vector3 v3_bulletsInst; //V3 bullets insts

    Vector3 tempVector3;

    public float f_mutantHealth = 500f;

    public float f_counter = 10;
    public float f_counterReload = 200;
    float f_counterJumpAttack = 2.5f;
    private float f_tempX;
    private float f_tempZ;
    private float f_distance;
    public float f_distanceWithPlayer;

    public bool b_IsMovingNewPosition = false;
    public bool b_IsChasingPlayer = false;
    public bool b_IsAttacking = false;
    public bool b_Is_Death = false;
    bool soundIsPlayed = false;

    public bool b_attacked_by_player = false;

    public bool b_Is_Shooting = false;
    public float f_delayAfterShooting = 3f;
    public float f_rockets = 3;

    float f_randomDeath;

    void Start()
    {
        go_Mutant.transform.parent = null;
        f_randomDeath = Random.Range(0, 10);

        go_Mutant.name = "Mutant";
        this.gameObject.tag = "Enemy";
        agent = GetComponent<NavMeshAgent>();
        animatorMutant = GetComponent<Animator>();
        mutantCollider = GetComponent<CapsuleCollider>();
        playerTransform = GameObject.Find("Player").transform;

        miniMapMarker.SetActive(true);
        f_counter = 10;
        shootEffectMutantLauncher.Stop(true);
        bloodEffectMutant.Stop(true);
        bloodEffectMutantAfterDeath.Stop(true); 

        rockets = new List<GameObject>();
        rocketsPool = GameObject.Find("MutantRocketsContainer");
        for (int i = 0; i < 20; i++)
        {
            GameObject temp = Instantiate(rocketInst);
            temp.SetActive(false);
            rockets.Add(temp); // Adding to the list
            rockets[i].transform.parent = rocketsPool.transform;
            rockets[i].transform.position = rocketsPool.transform.position;
            rockets[i].transform.rotation = rocketsPool.transform.rotation;
            rockets[i].name = "rocket";
        }


        f_tempX = Random.Range(-150f, 150f);
        f_tempZ = Random.Range(-150f, 150f);


        tempVector3.x = mutantTransform.position.x + f_tempX;
        tempVector3.y = mutantTransform.position.y;
        tempVector3.z = mutantTransform.position.z + f_tempZ;

        MoveToNewPosition();

    }

    public GameObject GetRocket()
    {
        for (int i = 0; i < 20; i++)
        {
            if (!rockets[i].activeInHierarchy)
            {
                rockets[i].SetActive(true);
                rockets[i].transform.position = mutantGunTransform.transform.position;
                rockets[i].transform.rotation = mutantGunTransform.transform.rotation;
                return rockets[i];
            }
        }

        GameObject temp = Instantiate(rocketInst);
        rockets.Add(temp);
        temp.SetActive(true);
        return temp;
    }

    void mutantDeathSound()
    {
        
        if (!soundIsPlayed)
        {
            FindObjectOfType<AudioManager>().Play("mutantDeath");
            soundIsPlayed = true;

            //We can Instantiate any prefab here----->
            Instantiate(go_generator, this.transform);
        }
    }


    // Taking new position on map 
    void MoveToNewPosition()
    {
        if (b_IsAttacking == false && b_IsChasingPlayer == false && b_IsMovingNewPosition == false && !b_Is_Death && !b_Is_Shooting)
        {
            agent.SetDestination(tempVector3);
            agent.speed = 3f;
            f_counter = 10;
            animatorMutant.SetBool("Is_Walking", true);
            b_IsMovingNewPosition = true;
        }

    }

    //
    public void ActionOnTrigger()
    {
        b_attacked_by_player = false;
        b_Is_Shooting = true;
        shootEffectMutantLauncher.Play(true);
        agent.speed = 0f;
        animatorMutant.SetBool("Is_Shooting", true);
        animatorMutant.SetBool("Is_Running", false);
        animatorMutant.SetBool("Is_Walking", false);
        GetRocket();
        FindObjectOfType<AudioManager>().Play("launcherSound");
    }

    void Update()
    {
        f_distance = Vector3.Distance(tempVector3, transform.position);
        f_distanceWithPlayer = Vector3.Distance(playerTransform.position, transform.position);

        //Mutant stop chasing player if player is dead
        if (Player.cl_Player.b_IsDeath == true)
        {
            animatorMutant.SetBool("Is_Running", false);
            animatorMutant.SetBool("Is_Walking", true);
            animatorMutant.SetBool("Is_Shooting", false);
            f_delayAfterShooting = 3f;
            b_Is_Shooting = false;
            b_attacked_by_player = false;

            f_tempX = Random.Range(-150f, 150f);
            f_tempZ = Random.Range(-150f, 150f);


            tempVector3.x = mutantTransform.position.x + f_tempX;
            tempVector3.y = mutantTransform.position.y;
            tempVector3.z = mutantTransform.position.z + f_tempZ;

            MoveToNewPosition();

        }


        //Death check for Mutant
        if (this.f_mutantHealth <= 0)
        {
            //mutantHealthBar.cl_mutantHealthBar.gameObject.SetActive(false);
            this.gameObject.tag = "Untagged";
            miniMapMarker.SetActive(false);
            mutantDeathSound();
            agent.speed = 0f;
            b_Is_Death = true;
            b_attacked_by_player = false;
            b_IsMovingNewPosition = false;
            mutantCollider.enabled = true;
            agent.enabled = true;
            go_Mutant.transform.Translate(new Vector3(0, 0.5f, 0));
            bloodEffectMutantAfterDeath.Play(true);
            if (f_randomDeath <= 5)
            {
                animatorMutant.SetBool("Is_Death", true);
                animatorMutant.SetBool("Is_Shooting", false);
                animatorMutant.SetBool("Is_Running", false);
                animatorMutant.SetBool("Is_Walking", false);
            }
            if (f_randomDeath > 5)
            {
                animatorMutant.SetBool("Is_Death2", true);
                animatorMutant.SetBool("Is_Shooting", false);
                animatorMutant.SetBool("Is_Running", false);
                animatorMutant.SetBool("Is_Walking", false);
            }

        }

        //Start chasing player if player attacks mutant
        if (b_attacked_by_player && !b_Is_Death && !Player.cl_Player.b_IsDeath)
        {
            agent.SetDestination(playerTransform.position);
            b_IsMovingNewPosition = false;
            agent.speed = 6f;
            animatorMutant.SetBool("Is_Running", true);
            animatorMutant.SetBool("Is_Walking", false);
        }

        //Delay after shooting
        if (b_Is_Shooting && f_delayAfterShooting > 0 && !Player.cl_Player.b_IsDeath)
        {
            f_delayAfterShooting -= Time.deltaTime;
        }
        if (f_delayAfterShooting <= 0)
        {
            shootEffectMutantLauncher.Stop(true);

            b_Is_Shooting = false;
            b_attacked_by_player = true;
            animatorMutant.SetBool("Is_Shooting", false);
            animatorMutant.SetBool("Is_Running", true);
            agent.speed = 6f;
            f_delayAfterShooting = 3f;

        }

        //Countdown before taking next position
        if (f_counter > 0 && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            f_counter -= Time.deltaTime;
        }
        if (f_counter <= 0 && !b_IsMovingNewPosition && !b_IsChasingPlayer && b_IsAttacking == false && !b_Is_Death && !b_Is_Shooting)
        {

            f_tempX = Random.Range(-150f, 150f);
            f_tempZ = Random.Range(-150f, 150f);


            tempVector3.x = mutantTransform.position.x + f_tempX;
            tempVector3.y = mutantTransform.position.y;
            tempVector3.z = mutantTransform.position.z + f_tempZ;

            MoveToNewPosition();
        }
        //Coundown for reload Mutant
        if (f_counterReload > 0)
        {
            f_counterReload -= Time.deltaTime;
        }
        if (f_counterReload <= 0)
        {
            f_tempX = Random.Range(-550f, 550f);
            f_tempZ = Random.Range(-550f, 550f);


            tempVector3.x = mutantTransform.position.x + f_tempX;
            tempVector3.y = mutantTransform.position.y;
            tempVector3.z = mutantTransform.position.z + f_tempZ;

            MoveToNewPosition();
        }

        //Mutant stops if it reached current point 
        if (this.f_distance < 7f && b_IsMovingNewPosition && !b_IsChasingPlayer && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath || this.f_distance < 8f && b_IsMovingNewPosition && !b_IsChasingPlayer && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath || this.f_distance < 9f && b_IsMovingNewPosition && !b_IsChasingPlayer && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            animatorMutant.SetBool("Is_Walking", false);
            b_IsMovingNewPosition = false;
            agent.speed = 0f;
            f_counter = 10;

        }

        //Checking distance to Player and if it less than 20 start to chasing player
        if (this.f_distanceWithPlayer < 100f && b_IsAttacking == false && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            b_IsChasingPlayer = true;
            b_IsMovingNewPosition = false;
            agent.SetDestination(playerTransform.position);
            animatorMutant.SetBool("Is_Running", true);
            agent.speed = 6f;


        }
        //Here we can say mutant to attack Player;
        if (this.f_distanceWithPlayer < 6f && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            agent.speed = 0f;
            b_IsAttacking = true;

            animatorMutant.SetBool("Is_Running", false);
            animatorMutant.SetBool("Is_SwipeAttack", true);


        }
        if (this.f_distanceWithPlayer > 6f && b_IsAttacking == true && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            animatorMutant.SetBool("Is_SwipeAttack", false);
            agent.speed = 6f;

        }

        //Stop chasing player based on distance
        if (f_distanceWithPlayer > 120f && b_IsChasingPlayer == true && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {

            animatorMutant.SetBool("Is_Running", false);
            animatorMutant.SetBool("Is_Walking", false);
            animatorMutant.SetBool("Is_SwipeAttack", false);

            b_IsChasingPlayer = false;
            b_IsAttacking = false;
            f_counterJumpAttack = 3f;

        }

        if (b_IsAttacking == true && !b_Is_Death && !b_Is_Shooting && !Player.cl_Player.b_IsDeath)
        {
            f_counterJumpAttack -= Time.deltaTime;
            Debug.Log(f_counterJumpAttack);
            if (f_counterJumpAttack <= 0)
            {
                animatorMutant.SetBool("Is_Running", true);
                animatorMutant.SetBool("Is_Walking", false);
                animatorMutant.SetBool("Is_SwipeAttack", false);
                f_counterJumpAttack = 2.5f;
                agent.speed = 6f;
                b_IsAttacking = false;
                agent.SetDestination(playerTransform.position);
            }
        }



    }
}
