using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeFrame : MonoBehaviour {

    ObjectController pullObjectScript;
    private GameObject ropeObject;

	void Start () {
		
	}
	
	void Update () {

        ropeObject = transform.parent.gameObject;
        if (ropeObject == null)
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }

    }
}
