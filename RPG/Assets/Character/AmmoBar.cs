using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour {

    public static AmmoBar cl_AmmoBar;

    public Image ammoBar;
    public float f_fill;

    void Start()
    {
        cl_AmmoBar = this;

    }


    void Update()
    {
        ammoBar.fillAmount = Player.cl_Player.f_ammo / 150;
    }
}
