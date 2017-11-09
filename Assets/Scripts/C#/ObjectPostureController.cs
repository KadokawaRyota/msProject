using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPostureController : MonoBehaviour
{

    private Vector3 surfaceNormal;

    void Start()
    {
        Vector3 dirVec;
        dirVec = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;

        surfaceNormal = transform.position - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;

        dirVec = Vector3.ProjectOnPlane(dirVec, surfaceNormal);
        transform.rotation = Quaternion.LookRotation(dirVec, surfaceNormal);


    }

    void Update()
    {

    }
}
