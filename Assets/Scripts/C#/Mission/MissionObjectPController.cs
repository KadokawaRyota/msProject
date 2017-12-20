using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectPController : MonoBehaviour {

    private Vector3 surfaceNormal;

    void Start () {
		
	}
	
	void Update () {

        Vector3 dirVecZ;
        surfaceNormal = transform.position - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;

        dirVecZ = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;
        dirVecZ = Vector3.ProjectOnPlane(dirVecZ, surfaceNormal);
        transform.rotation = Quaternion.LookRotation(dirVecZ, surfaceNormal);

    }
}
