using UnityEngine;
using System.Collections;

public class RaycastMouse : MonoBehaviour 
{
    RPGCharacterController controllerIstance;

    //HUD elements
    public GameObject mutantInfoHUD;
    public GameObject helmetLevel3HUD;
    public GameObject helmetLevel2HUD;

    public GameObject impulseRifleHUD;

    public Texture2D iconArrow;
	public Vector2 arrowRegPoint;
	public Texture2D iconAttack;
	public Vector2 attackRegPoint;
	public Texture2D iconTalk;
	public Vector2 talkRegPoint;
	public Texture2D iconInteract;
	public Vector2 interactRegPoint;
    public Texture2D iconLoot;
    public Vector2 lootRegPoint;
    public Texture2D iconPickUp;
    public Vector2 pickupRegPoint;
    private Vector2 mouseReg;
	private Vector2 mouseCoord;
	private Texture mouseTex;

    mutantHealthBar tempMutantHealthBar;

    private void Awake()
    {
        tempMutantHealthBar = GameObject.Find("Canvas/HUD/EnemyInfoMutant").GetComponent<mutantHealthBar>();
    }


    void OnDisable()
	{
		Cursor.visible = true;	
	}
	
    void Update () 
	{
		Cursor.visible = false;


	}
	
	void OnGUI()
	{
		//determine what we hit.
    	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

        controllerIstance = GameObject.Find("Player").GetComponent<RPGCharacterController>();
        if (Physics.Raycast(ray, out hit) && !controllerIstance.b_isAiming) 
		{
            
            switch (hit.collider.tag)
			{
				case "Enemy":
					mouseTex = iconAttack;
					mouseReg = attackRegPoint;

                    //I need to find out problem here
                    if (hit.collider.name == "Mutant")
                    {

                        this.tempMutantHealthBar.f_fill = hit.collider.gameObject.GetComponent<Mutant>().f_mutantHealth;

                        //mutantInfoHUD.SetActive(true);
                        //mutantInfoHUD.transform.position = Input.mousePosition;

                        Debug.Log(tempMutantHealthBar.f_fill);
                    }

                   
                    break;

				case "NPC":
					mouseTex = iconTalk;
					mouseReg = talkRegPoint;
                    
                    break;

				case "Interact":
					mouseTex = iconInteract;
					mouseReg = interactRegPoint;
				break;

                case "Loot":
                    mouseTex = iconLoot;
                    mouseReg = lootRegPoint;

                    break;

                case "PickUp":
                    mouseTex = iconPickUp;
                    mouseReg = pickupRegPoint;

                    if (hit.collider.name == "Helmet_lvl2")
                    {
                            helmetLevel2HUD.SetActive(true);
                            helmetLevel2HUD.transform.position = Input.mousePosition;
                    }
                    if (hit.collider.name == "Helmet_lvl3")
                    {
                        helmetLevel3HUD.SetActive(true);
                        helmetLevel3HUD.transform.position = Input.mousePosition;
                    }
                    if (hit.collider.name == "Rifle")
                    {
                        impulseRifleHUD.SetActive(true);
                        impulseRifleHUD.transform.position = Input.mousePosition;
                    }

                    break;

                default:
					mouseTex = iconArrow;
					mouseReg = arrowRegPoint;
                    
                    mutantInfoHUD.SetActive(false);
                    

                    helmetLevel2HUD.SetActive(false); 
                    helmetLevel3HUD.SetActive(false);
                    impulseRifleHUD.SetActive(false);

                    break;
			}
		}
		else
		{
			mouseTex = iconArrow;
			mouseReg = arrowRegPoint;
                        
        }
		
		//update texture object.
		mouseCoord = Input.mousePosition;
		GUI.DrawTexture(new Rect(mouseCoord.x-mouseReg.x, Screen.height-mouseCoord.y - mouseReg.y, mouseTex.width, mouseTex.height), mouseTex, ScaleMode.StretchToFill, true, 10.0f);
	}
} 
