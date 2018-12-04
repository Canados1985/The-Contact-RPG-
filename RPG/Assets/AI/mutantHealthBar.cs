using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mutantHealthBar : MonoBehaviour {


    RPGCharacterController tempController;

    public GameObject enemyInfo;

    public Vector3 startPos;

    public Image mutantHealthBarImg;
    public float f_fill;

    private void Awake()
    {
        tempController = GameObject.Find("Player").GetComponent<RPGCharacterController>();
    }

    void Start()
    {
        startPos = this.transform.position;
        f_fill = 1;
        enemyInfo.SetActive(false);

    }


    void Update()
    {
       // Mutant tempMutant;
       // tempMutant = GameObject.Find("Mutant").GetComponent<Mutant>();

        mutantHealthBarImg.fillAmount = f_fill;

        if (tempController.focus == null)
        {
            //enemyInfo.SetActive(false);
        }

        


    }
}
