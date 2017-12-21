using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope_FrameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject GetFreamMesh()
    {
        foreach (Transform child in transform)
        {
            //child is your child transform
            if (child.name == "frame_mesh")
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
