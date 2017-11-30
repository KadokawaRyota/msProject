using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ビルボード
public class Billboard : MonoBehaviour {

	void FixedUpdate () {

        //カメラポジションの取得
       // Quaternion rot = Camera.main.transform.rotation;
        Vector3 pos = Camera.main.transform.position;
        Vector3 a = (pos - transform.position).normalized;
        transform.up = a * Time.deltaTime;
    }
}
