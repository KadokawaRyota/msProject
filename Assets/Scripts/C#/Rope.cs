using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public GameObject targetObj;                                   // プレイヤー情報


    void Start () {
		
	}
	
	void Update () {

        transform.RotateAround(targetObj.transform.position, targetObj.transform.up, moveSpeed * Time.deltaTime * 200f);

    }
}
