using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ビルボード
public class Billboard : MonoBehaviour {

	void Update () {

        //カメラポジションの取得
        Quaternion rot = Camera.main.transform.rotation;
        Vector3 pos = Camera.main.transform.position;

        //rot = new Vector3(rot.x - 90.0f, rot.y, rot.z);
       // pos = new Vector3(pos.x - 90.0f, pos.y, pos.z);

        //書き換えてその方向へ向かせる
         pos.y = transform.position.y;
        



        //指定した向きに向かせる
        //transform.localEulerAngles = pos;
        transform.LookAt(pos);
        transform.localEulerAngles = new Vector3(transform.rotation.x, rot.y, transform.rotation.z);
    }
}
