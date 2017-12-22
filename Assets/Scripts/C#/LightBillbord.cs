using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBillbord : MonoBehaviour {

	void Update () {
        Vector3 dirVec = Camera.main.transform.position - transform.position;
        transform.up = dirVec.normalized; 
	}
}
