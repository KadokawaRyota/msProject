using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPostureController : MonoBehaviour
{

    private Vector3 surfaceNormal;
    public bool standX;
    public bool standY = true;
    public bool standZ;
    public bool reverse = false;

    void Start()
    {
        Vector3 dirVecY, dirVecZ;
        surfaceNormal = transform.position - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;

        if ( standX )
        {
            standY = false;
            standZ = false;
        }
        else if( standY )
        {
            standX = false;
            standZ = false;
        }
        else if( standZ )
        {
            standX = false;
            standY = false;
        }

        // Y軸上向き
        if (standY)
        {
            dirVecZ = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;
            dirVecZ = Vector3.ProjectOnPlane(dirVecZ, surfaceNormal);
            transform.rotation = Quaternion.LookRotation(dirVecZ, surfaceNormal);
        }

        // Z軸上向き
        if (standX || standZ)
        {
            /*if (reverse)
            {
                dirVecY = transform.up;
                dirVecY = Vector3.ProjectOnPlane(dirVecY, surfaceNormal);
                transform.rotation = Quaternion.LookRotation(surfaceNormal, dirVecY);
            }
            else
            {*/
                dirVecY = transform.up;
                dirVecY = Vector3.ProjectOnPlane(dirVecY, surfaceNormal);
                transform.rotation = Quaternion.LookRotation(surfaceNormal, dirVecY);
            //}
        }

        // X軸上向き
        if (standX)
        {
            transform.Rotate(new Vector3(0, 1, 0), -90);
        }

        
    }

}
