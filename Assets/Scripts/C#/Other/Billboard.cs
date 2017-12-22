using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ビルボード
public class Billboard : MonoBehaviour {

    void Update()
    {

        //カメラポジションの取得
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
