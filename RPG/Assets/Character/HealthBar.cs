using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public static HealthBar cl_HealthBar;

    public Image healthBar;
    public float f_fill;

	void Start () {
        cl_HealthBar = this;
        f_fill = 1;
	}
	
	
	void Update () {
        healthBar.fillAmount = Player.cl_Player.f_playerHealth / 100 ;
	}
}
