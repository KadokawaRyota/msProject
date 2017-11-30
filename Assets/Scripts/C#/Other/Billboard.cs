using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ビルボード
public class Billboard : MonoBehaviour {

	void Update () {

        //カメラポジションの取得
        Vector3 pos = Camera.main.transform.forward;

        //書き換えてその方向へ向かせる
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }
}
