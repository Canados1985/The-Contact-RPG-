using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMutant : MonoBehaviour {


    public GameObject go_Mutant;

    public float f_countDown;

    private List<GameObject> Mutants; //Mutants list

    public bool b_MutantIsBorn = false;

    void Start () {

        f_countDown = Random.Range(50, 80);

    }
	


	void Update () {

        if (f_countDown > 0)
        {
            b_MutantIsBorn = false;
            f_countDown -= Time.deltaTime;
        }
        if(f_countDown <= 0 && !b_MutantIsBorn)
        {
            b_MutantIsBorn = true;
            Instantiate(go_Mutant, this.transform);
            f_countDown = Random.Range(50, 80);
        }
	}
}
