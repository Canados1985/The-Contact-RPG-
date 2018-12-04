using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxigenBar : MonoBehaviour {

    public static OxigenBar cl_OxigenBar;

    public Image oxigenBar;
    public float f_fill;

    void Start () {
        cl_OxigenBar = this;
        f_fill = 1;
    }


	void Update () {
        oxigenBar.fillAmount = Player.cl_Player.f_playerOxigen / 100;
    }
}
