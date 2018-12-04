using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ServantFollowPlayer : MonoBehaviour {


    public Transform playerTransform;

    private Rigidbody rb_Servant;
    private Animator animatorServant;

    private NavMeshAgent agent;
    private float f_distance;


	void Start () {

        agent = GetComponent<NavMeshAgent>();
        rb_Servant = GetComponent<Rigidbody>();
        animatorServant = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
 
        agent.SetDestination(playerTransform.position);
        f_distance = Vector3.Distance(playerTransform.position, transform.position);
        if (f_distance > 10)
        {
            animatorServant.SetBool("IsRunning", true);
            agent.speed = 3;
        }
        if (f_distance > 3f && f_distance < 10f)
        {
            agent.speed = 1.63f;
            animatorServant.SetBool("IsWalking", true);
            animatorServant.SetBool("IsRunning", false);

        }
        if (f_distance < 3.1f)
        {
            agent.speed = 1.63f;
            animatorServant.SetBool("IsRunning", false);
            animatorServant.SetBool("IsWalking", false);

        }
        //Debug.Log(f_distance);
	}
}
