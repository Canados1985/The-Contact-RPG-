using UnityEngine;


public class RPGCharacterController : MonoBehaviour
{

    public Rigidbody rb_player;
    public string moveStatus = "idle";
    public bool walkByDefault = true;
    public float gravity = 20.0f;


    public Camera camera; // RPG camera
    public Interactable focus;
    public bool b_IsFocused = false;
    public GameObject go_gunAimRaicast; // canvas img
    public GameObject playerGun; // empty object using as start position for bullets

    //Movement speeds
    public float jumpSpeed = 10.0f;
    public float runSpeed = 10.0f;
    public float walkSpeed = 4.0f;
    public float turnSpeed = 250.0f;
    public float moveBackwardsMultiplier = 0.75f;

    //Internal vars to work with
    private float speedMultiplier = 0.0f;
    private bool grounded = false;
    public Vector3 moveDirection = Vector3.zero;
    private bool isWalking = false;
    private bool jumping = false;
    public bool b_isAiming = false;
    bool b_playerAttackingNoFocus = false;
    private bool mouseSideButton = false;
    private CharacterController controller;
    public Animator animController;

    public float f_counterShooting = 0.5f; // for shooting
    public ParticleSystem shootingEffect;


    mutantHealthBar tempMutantHealthBar;
    
    void Awake()
    {
        //Get CharacterController
        tempMutantHealthBar = GameObject.Find("Canvas/HUD/EnemyInfoMutant").GetComponent<mutantHealthBar>();
        controller = GetComponent<CharacterController>();
        animController = GetComponent<Animator>();
        shootingEffect.Stop(true);
       
    }



    void InstantiateBullet()
    {
        shootingEffect.Play(true);
        GameObject temp = Player.cl_Player.GetBullet();
        Player.cl_Player.f_ammo = Player.cl_Player.f_ammo - 1;

        //Shooting Raycast to enemy
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5000))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.gameObject.name == "Mutant")
            {
                

                if (hit.transform.GetComponent<Mutant>().f_mutantHealth > 0)
                {
                    b_playerAttackingNoFocus = true;

                    tempMutantHealthBar.enemyInfo.SetActive(true);
                    tempMutantHealthBar.f_fill = hit.transform.GetComponent<Mutant>().f_mutantHealth / 500;

                    hit.transform.GetComponent<Mutant>().bloodEffectMutant.Play(true);
                    hit.transform.GetComponent<Mutant>().b_attacked_by_player = true;
                    hit.transform.GetComponent<Mutant>().f_mutantHealth = hit.transform.GetComponent<Mutant>().f_mutantHealth - 10;
                }
                if (hit.transform.GetComponent<Mutant>().f_mutantHealth <= 0)
                {
                    b_playerAttackingNoFocus = false;
                    tempMutantHealthBar.enemyInfo.SetActive(false);
                }
            }
        }
    }


    void Update ()
	{
        //Debug.Log(moveDirection.z);
        go_gunAimRaicast.SetActive(false);
        b_isAiming = false;
        
        if (Player.cl_Player.b_IsDeath == true)
        {
            animController.SetBool("IsDeath", true);
            animController.SetBool("IsJumping", false);
            animController.SetBool("IsWalkingBackward", false);
            animController.SetBool("IsWalking", false);
            animController.SetBool("IsRunning", false);
            animController.SetBool("IsWalkingLeft", false);
            animController.SetBool("IsWalkingRight", false);
            animController.SetBool("IsRunningBackward", false);
            animController.SetBool("IsRunningLeft", false);
            animController.SetBool("IsRunningRight", false);
            animController.SetBool("IsAiming", false);
            moveDirection = Vector3.zero;
        }

        // Character shooting -->
        if (Input.GetKey(KeyCode.LeftControl) && !Player.cl_Player.b_IsDeath && Player.cl_Player.impulseRifle.activeSelf == true)
        {
            go_gunAimRaicast.SetActive(true);
            animController.SetBool("IsAiming", true);
            b_isAiming = true;
            moveDirection = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            playerGun.transform.rotation = camera.transform.rotation;


            if (f_counterShooting > 0)
            {
                f_counterShooting -= Time.deltaTime;
                shootingEffect.Stop(true);
            }
            if (f_counterShooting <= 0)
            {
                f_counterShooting = 0.5f;
                if (Input.GetKey(KeyCode.Mouse0) && Player.cl_Player.f_ammo > 0)
                {   

                    if (f_counterShooting == 0.5f)
                    {

                        
                        FindObjectOfType<AudioManager>().Play("characterShot");
                        InstantiateBullet();
                        Invoke("InstantiateBullet", 0.1f);
                        Invoke("InstantiateBullet", 0.1f);

                    }

                }
                
            }


        }
        //Set idle animation
        moveStatus = "idle";
		isWalking = walkByDefault;
			
		// Hold "Run" to run
		if(Input.GetAxis("Run") != 0 && !b_isAiming && !Player.cl_Player.b_IsDeath)
		{
			isWalking = !walkByDefault;
		}
		
		// Only allow movement and jumps while grounded
		if(grounded && !b_isAiming && !Player.cl_Player.b_IsDeath) 
		{

            //Debug.Log(moveDirection.x);

            animController.SetBool("IsJumping", false);
            animController.SetBool("IsWalkingBackward", false);
            animController.SetBool("IsWalking", false);
            animController.SetBool("IsRunning", false);
            animController.SetBool("IsWalkingLeft", false);
            animController.SetBool("IsWalkingRight", false);
            animController.SetBool("IsRunningBackward", false);
            animController.SetBool("IsRunningLeft", false);
            animController.SetBool("IsRunningRight", false);
            animController.SetBool("IsAiming", false);



            //Changing states for moving forward
            if (Input.GetKey(KeyCode.W) && !b_isAiming && !Player.cl_Player.b_IsDeath)
            {
                RemoveFocus();
                animController.SetBool("IsWalking", true);

                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsWalkingLeft", true);
                }
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsWalkingRight", true);
                }

                if (moveDirection.magnitude > 5)
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsRunning", true);

                    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                    {
                        animController.SetBool("IsRunning", false);
                        animController.SetBool("IsRunningLeft", true);
                    }
                    if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                    {
                        animController.SetBool("IsRunning", false);
                        animController.SetBool("IsRunningRight", true);
                    }

                }

            }
            //Changing states for moving backward
            if (Input.GetKey(KeyCode.S) && !b_isAiming && !Player.cl_Player.b_IsDeath)
            {
                RemoveFocus();
                animController.SetBool("IsWalkingBackward", true);


                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                {
                    animController.SetBool("IsWalkingBackward", false);
                    animController.SetBool("IsWalkingLeft", true);
                }
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                {
                    animController.SetBool("IsWalkingBackward", false);
                    animController.SetBool("IsWalkingRight", true);
                }

                if (moveDirection.magnitude > 3.5f)
                {
                    animController.SetBool("IsWalkingBackward", false);
                    animController.SetBool("IsRunningBackward", true);

                    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                    {
                        animController.SetBool("IsRunningBackward", false);
                        animController.SetBool("IsRunninggLeft", true);
                    }
                    if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                    {
                        animController.SetBool("IsRunningBackward", false);
                        animController.SetBool("IsRunningRight", true);
                    }
                }

            }
            if (Input.GetKey(KeyCode.Q) && !b_isAiming && !Player.cl_Player.b_IsDeath)
            {
                RemoveFocus();
                animController.SetBool("IsWalkingLeft", true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animController.SetBool("IsRunningLeft", true);
                }


            }
            if (Input.GetKey(KeyCode.E) && !b_isAiming && !Player.cl_Player.b_IsDeath)
            {
                RemoveFocus();
                animController.SetBool("IsWalkingRight", true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animController.SetBool("IsRunningRight", true);
                }

            }


            //if the player is steering with the right mouse button, A/D strafe instead of turn.
            if (Input.GetMouseButton(1) && !b_isAiming && !Player.cl_Player.b_IsDeath)
			{
				moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
                
			}
			else
			{
				moveDirection = new Vector3(0,0,Input.GetAxis("Vertical"));
                
            }

			//Auto-move button pressed
			if(Input.GetButtonDown("Toggle Move"))
			{
			    mouseSideButton = !mouseSideButton;
			}
    		
			//player moved or otherwise interrupted the auto-move.
			if(mouseSideButton && (Input.GetAxis("Vertical") != 0 || Input.GetButton("Jump")) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
			{
				mouseSideButton = false;
               
            }
			
			//L+R MouseButton Movement
			if ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) && !b_isAiming && !Player.cl_Player.b_IsDeath || mouseSideButton && !b_isAiming && !Player.cl_Player.b_IsDeath)
			{
                animController.SetBool("IsWalking", true);

                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsWalkingLeft", true);
                }
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsWalkingRight", true);
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsRunning", true);

                    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
                    {
                        animController.SetBool("IsRunning", false);
                        animController.SetBool("IsRunningLeft", true);
                    }
                    if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
                    {
                        animController.SetBool("IsRunning", false);
                        animController.SetBool("IsRunningRight", true);
                    }

                }


                moveDirection.z = 1;

			}
			
			//If not strafing with right mouse and horizontal, check for strafe keys
			if(!(Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0) && !b_isAiming && !Player.cl_Player.b_IsDeath)
			{
				moveDirection.x -= Input.GetAxis("Strafing");
			}
			    
		  	//if moving forward/backward and sideways at the same time, compensate for distance
			if(((Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0) || Input.GetAxis("Strafing") != 0) && Input.GetAxis("Vertical") != 0) 
			{
				moveDirection *= 0.707f;
                
            }
		  					
			//apply the move backwards multiplier if not moving forwards only.
			if((Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0) || Input.GetAxis("Strafing") != 0 || Input.GetAxis("Vertical") < 0)
			{
				speedMultiplier = moveBackwardsMultiplier;
                if(Input.GetKey(KeyCode.A) && !b_isAiming && !Player.cl_Player.b_IsDeath)
                {
                    animController.SetBool("IsWalkingLeft", true);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animController.SetBool("IsRunningLeft", true);
                    }
                    
                }
                if (Input.GetKey(KeyCode.D) && !b_isAiming && !Player.cl_Player.b_IsDeath)
                {
                    animController.SetBool("IsWalkingRight", true);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animController.SetBool("IsRunningRight", true);
                    }
                }

                
            }
			else
			{
				speedMultiplier = 1f;
                
            }
			
			//Use run or walkspeed
			moveDirection *= isWalking ? walkSpeed * speedMultiplier : runSpeed * speedMultiplier;
				  	
			// Jump!
			if(Input.GetButtonDown("Jump") && !b_isAiming && !Player.cl_Player.b_IsDeath)
			{
				jumping = true;
                //rb_player.AddForce(this.transform.up * jumpSpeed * 500);
                //moveDirection.y = jumpSpeed;
                animController.SetBool("IsJumping", true);
			}

			//Determine our moveStatus state (for animations).    		
			if((moveDirection.x == 0 && !Player.cl_Player.b_IsDeath) && (moveDirection.z == 0))
			{
				moveStatus = "idle";
			}
			

            /*
			if(moveDirection.z > 0)
			{
				moveStatus = isWalking ? "walking" : "running";
			}
			
			if(moveDirection.z < 0)
			{
				moveStatus = isWalking ? "walkingBack" : "runningBack";
			}
			
			if(moveDirection.x > 0)
			{
				moveStatus = isWalking ? "walkingRight" : "runningRight";
			}
			
			if(moveDirection.x < 0)
			{
				moveStatus = isWalking ? "walkingLeft" : "runningLeft";
			}
			*/
			
			//transform direction
			moveDirection = transform.TransformDirection(moveDirection);		
		
		}
		
		//Character must face the same direction as the Camera when the right mouse button is down.
		if(Input.GetMouseButton(1) && !b_isAiming && !Player.cl_Player.b_IsDeath) 
		{
			transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
		}
		else 
		{
            if (!b_isAiming && !Player.cl_Player.b_IsDeath)
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            }

		}
		
		//Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		//Move Charactercontroller and check if grounded
		grounded = ((controller.Move(moveDirection * Time.deltaTime)) & CollisionFlags.Below) != 0;
		
		//Reset jumping after landing
		jumping = grounded ? false : jumping;
		
		//movestatus jump/swimup (for animations)      
		if(jumping)
		{
			moveStatus = "jump";
		}

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5000))
            {
                RemoveFocus();



                if (tempMutantHealthBar.enemyInfo.activeSelf == true && !b_playerAttackingNoFocus)
                {
                    tempMutantHealthBar.enemyInfo.SetActive(false);
                }
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100000))
            {
                Interactable interactable =  hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        
        if(focus != null)
        {

               if (focus.gameObject.tag == "Enemy" && Player.cl_Player.impulseRifle.activeSelf == true)
               {
                    FaceTarget();
                    go_gunAimRaicast.SetActive(true);
                    tempMutantHealthBar.enemyInfo.SetActive(true);
                    tempMutantHealthBar.f_fill = focus.GetComponent<Mutant>().f_mutantHealth / 500;

                    if (focus.GetComponent<Mutant>().f_mutantHealth <= 0)
                    {
                    tempMutantHealthBar.enemyInfo.SetActive(false);
                    }
                    animController.SetBool("IsJumping", false);
                    animController.SetBool("IsWalkingBackward", false);
                    animController.SetBool("IsWalking", false);
                    animController.SetBool("IsRunning", false);
                    animController.SetBool("IsWalkingLeft", false);
                    animController.SetBool("IsWalkingRight", false);
                    animController.SetBool("IsRunningBackward", false);
                    animController.SetBool("IsRunningLeft", false);
                    animController.SetBool("IsRunningRight", false);
                    animController.SetBool("IsAiming", true);

                    if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1) && Input.GetKey(KeyCode.A))
                    {
                        FaceTarget();
                        animController.SetBool("IsAiming", false);
                        animController.SetBool("IsWalkingLeft", true);
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            animController.SetBool("IsRunningLeft", true);
                        }

                    }
                    if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.E) || Input.GetMouseButton(1) && Input.GetKey(KeyCode.D))
                    {
                        FaceTarget();
                        animController.SetBool("IsAiming", false);
                        animController.SetBool("IsWalkingRight", true);
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            animController.SetBool("IsRunningRight", true);
                        }

                    }


                if (f_counterShooting > 0)
                {
                    f_counterShooting -= Time.deltaTime;
                    shootingEffect.Stop(true);
                }
                if (f_counterShooting <= 0)
                {
                    f_counterShooting = 0.5f;
                    if (Input.GetKey(KeyCode.Space) && Player.cl_Player.f_ammo > 0)
                    {
                        if (f_counterShooting == 0.5f)
                        {

                            shootingEffect.Play(true);
                            FindObjectOfType<AudioManager>().Play("characterShot");
                            InstantiateBullet();
                            Invoke("InstantiateBullet", 0.1f);
                            Invoke("InstantiateBullet", 0.2f);

                        }

                    }

                }

            }





        }
        
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
            focus.OnDefocused();

            b_IsFocused = true;
            focus = newFocus;

           
        }

        newFocus.OnFocused(transform);

    }

    void RemoveFocus()
    {
        if (focus != null)
        focus.OnDefocused();

        focus = null;
        b_IsFocused = false;
        
    }

    //Rotation Player when target is Enemy
    void FaceTarget()
    {
        Vector3 direction = (focus.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, transform.rotation, Time.deltaTime * 5f);

        //if we want to use raycast from player gun ---->
       /* Vector3 direction2 = (focus.transform.position - go_gunAimRaicast.transform.position).normalized;
        Quaternion lookRotation2 = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(go_gunAimRaicast.transform.rotation, lookRotation, Time.deltaTime * 5f);*/
    }
}
