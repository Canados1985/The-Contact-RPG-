using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDestination : MonoBehaviour {


    private Transform newDestinationTransform;
    float f_tempX;
    float f_tempZ;

    float f_counter = 10f;


	void Start () {

        newDestinationTransform = GetComponent<Transform>();

        f_tempX = Random.Range(-10, 10);
        f_tempZ = Random.Range(-10, 10);

        transform.position = new Vector3(newDestinationTransform.position.x + f_tempX, newDestinationTransform.position.y, newDestinationTransform.position.z + f_tempZ);

    }


    private void FixedUpdate()
    {
        if (f_counter > 0)
        {
            f_counter -= Time.deltaTime;
        }
        if (f_counter <= 0)
        {
            f_tempX = Random.Range(-10, 10);
            f_tempZ = Random.Range(-10, 10);

            this.gameObject.transform.position = new Vector3( newDestinationTransform.position.x + f_tempX, newDestinationTransform.position.y, newDestinationTransform.position.z + f_tempZ);
            f_counter = 10;
        }
    }


    void Update () {
		


	}
}
