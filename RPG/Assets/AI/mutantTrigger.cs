using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutantTrigger : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        Mutant tempMutant;
        tempMutant = gameObject.transform.parent.GetComponent<Mutant>();

        if (other.gameObject.name == "Player" && tempMutant.f_rockets > 0 && !tempMutant.b_Is_Death && tempMutant.f_distanceWithPlayer > 20 && !Player.cl_Player.b_IsDeath)
        {
            tempMutant.ActionOnTrigger();
        }
    }

}
