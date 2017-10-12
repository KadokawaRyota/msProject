using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 球面上の重力操作
public class Acceleration : MonoBehaviour {

    private Vector3 acceleration;
	
	void Start () {
        // 重力加速度の初期値
        acceleration = new Vector3(0, -9.81f, 0);
    }
	
	void Update () {

        // 重力方向の更新
        Vector3 gravityVec = GetComponent<Transform>().position - Vector3.zero;
        gravityVec = gravityVec.normalized;
        acceleration = gravityVec * -9.81f * 10;
	}

    void FixedUpdate() {

        // 加速度の更新
        GetComponent<Rigidbody>().AddForce(acceleration, ForceMode.Acceleration);
    }

}
